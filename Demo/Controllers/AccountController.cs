using AutoMapper;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Data.Models;
using Services.Identity;
using Infrastructure.Identity;
using Infrastructure;
using Data;
using Data.Models.Enums;
using AppUtility.Helper;
using System.Data;
using System.Security.Claims;
using System.Text;
using System.IdentityModel.Tokens.Jwt;

namespace Demo.Controllers
{
    [ApiController]
    [Route("/api/")]
    public class AccountController : ControllerBase
    {
        #region Variables
        private readonly JWTConfig _jwtConfig;
        private readonly ApplicationUserManager _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private IUserService _users;
        private readonly ILogger<AccountController> _logger;
        private readonly ITokenService _tokenService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        #endregion

        public AccountController(IHttpContextAccessor httpContextAccessor, IOptions<JWTConfig> jwtConfig,
            ApplicationUserManager userManager, RoleManager<ApplicationRole> roleManager,
            SignInManager<ApplicationUser> signInManager, IUserService users, ITokenService tokenService,
            ILogger<AccountController> logger
            )
        {
            _httpContextAccessor = httpContextAccessor;
            _jwtConfig = jwtConfig.Value;
            _logger = logger;
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _users = users;
            _tokenService = tokenService;
        }
        [HttpPost(nameof(Register))]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            Response response = new Response()
            {
                StatusCode = ResponseStatus.warning,
                ResponseText = "Registration Failed"
            };
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    UserId = Guid.NewGuid().ToString(),
                    UserName = model.Email.Trim(),
                    Email = model.Email.Trim(),
                    Role = UserRoles.Admin.ToString(),
                    Name = model.Name,
                    PhoneNumber = model.PhoneNumber
                };
                var res = await _userManager.CreateAsync(user, model.Password);
                if (res.Succeeded)
                {
                    user = _userManager.FindByEmailAsync(user.Email).Result;
                    await _userManager.AddToRoleAsync(user, UserRoles.Franchise.ToString());
                    model.Password = string.Empty;
                    model.Email = string.Empty;
                    response.StatusCode = ResponseStatus.Success;
                    response.ResponseText = "User Register Successfully";
                }
            }
            return Ok(model);
        }

        [HttpPost(nameof(Login))]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            OSInfo oSInfo = new OSInfo(_httpContextAccessor);
            var res = new Response<AuthenticateResponse>
            {
                StatusCode = ResponseStatus.Failed,
                ResponseText = "Invalid Credentials"
            };
            try
            {
                var result = await _signInManager.PasswordSignInAsync(model.EmailId, model.Password, model.RememberMe, lockoutOnFailure: true);
                if (result.Succeeded)
                {
                    res = await GenerateAccessToken(model.EmailId);
                    if (res.Result.Role.ToUpper() == "ADMIN")
                    {
                        return LocalRedirect("/Admin/Index");
                    }
                }
            }
            catch (Exception ex)
            {
                res.ResponseText = "Something went wrong.Please try after some time.";
                _logger.LogError(ex, ex.Message);
            }
            return Ok(model);
        }        

        [HttpPost(nameof(Logout))]
        public async Task<IActionResult> Logout(string returnUrl = "/")
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            await _signInManager.SignOutAsync();
            HttpContext.Response.Cookies.Delete(".AspNetCore.Cookies");
            HttpContext.Response.Cookies.Delete(".AspNetCore.Identity.Application");
            return LocalRedirect(returnUrl);
        }


        private async Task<Response<AuthenticateResponse>> GenerateAccessToken(string mobileNo)
        {
            var res = new Response<AuthenticateResponse>
            {
                StatusCode = ResponseStatus.Failed,
                ResponseText = "Invalid Credentials"
            };
            var user = await _userManager.FindByMobileNoAsync(mobileNo);

            // Generate Referral Link And Code
            int len = AppConst.StartNumber.Length - user.Id.ToString().Length;
            var Numbers = GenerateZero(len);
            // End Referral

            if (user.Id > 0)
            {
                var claims = new[] {
                        new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(ClaimTypesExtension.Id, user.Id.ToString()),
                        new Claim(ClaimTypesExtension.Role, user.Role ?? "2" ),
                        new Claim(ClaimTypes.Role, user.Role ?? "2" ),
                        new Claim(ClaimTypesExtension.UserName, user.UserName),
                    };
                var token = _tokenService.GenerateAccessToken(claims);
                var authResponse = new AuthenticateResponse(user, token);
                res.StatusCode = ResponseStatus.Success;
                res.ResponseText = "Login Succussful";
                res.Result = authResponse;
            }
            return res;
        }
        private string GenerateZero(int Len)
        {
            StringBuilder res = new StringBuilder();
            for (int i = 1; i <= Len; i++)
            {
                res.Append("0");
            }
            return res.ToString();
        }
    }
}

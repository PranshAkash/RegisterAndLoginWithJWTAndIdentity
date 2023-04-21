using Data;
using Demo.Helper;
using Hangfire;
using Infrastructure;
using Infrastructure.Implementation;
using Data.Models;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Services.Identity;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Data.Migrations;
using FluentMigrator.Runner;
using System.Reflection;
using Microsoft.AspNetCore.DataProtection;

var builder = WebApplication.CreateBuilder(args);

IWebHostEnvironment WebHostEnvironment = builder.Environment;

string dbConnectionString = builder.Configuration.GetConnectionString("SqlConnection");
IConnectionHelper ch = new ConnectionHelper
{
    ConnectionString = dbConnectionString
};
builder.Services.AddSingleton<IConnectionHelper>(ch);
builder.Services.AddSingleton<ITaskService, TaskService>();
builder.Services.AddSingleton<IDapperRepository, DapperRepository>();
builder.Services.AddScoped<ICustomeLogger, CustomeLogger>();
builder.Services.AddScoped<ApplicationDbContext>();
builder.Services.AddScoped<IUserStore<ApplicationUser>, UserStore>();
builder.Services.AddScoped<IRoleStore<ApplicationRole>, RoleStore>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddTransient(typeof(ICustomeLogger<>), typeof(CustomeLogger<>));
builder.Services.AddScoped<Database>();

builder.Services.AddDataProtection().SetApplicationName($"{WebHostEnvironment.EnvironmentName}")
                .PersistKeysToFileSystem(new DirectoryInfo($@"{WebHostEnvironment.ContentRootPath}\keys"));


JWTConfig jwtConfig = new JWTConfig();
builder.Configuration.GetSection("JWT").Bind(jwtConfig);
builder.Services.AddSingleton(jwtConfig);
#region Identity
builder.Services.AddIdentity<ApplicationUser, ApplicationRole>(options =>
{
    options.Password.RequireDigit = false;
    options.Password.RequiredLength = 3;
    options.Password.RequireLowercase = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(30);
    options.Lockout.AllowedForNewUsers = false;
    options.Lockout.MaxFailedAccessAttempts = 3;
    options.User.RequireUniqueEmail = false;
}).AddUserStore<UserStore>()
.AddRoleStore<RoleStore>()
.AddUserManager<ApplicationUserManager>()
.AddDefaultTokenProviders();
builder.Services.AddAuthentication(option =>
{
    option = new Microsoft.AspNetCore.Authentication.AuthenticationOptions
    {
        DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme,
        DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme,
        DefaultScheme = JwtBearerDefaults.AuthenticationScheme
    };
}).AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = false;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration.GetSection("JWT:Issuer").Value,
        ValidAudience = builder.Configuration.GetSection("JWT:Audience").Value,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration.GetSection("JWT:Secretkey").Value))
    };
});
#endregion
builder.Services.AddSwaggerGen(option =>
{
    option.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1.1",
        Title = "API Documentation(v1.1)"
    });
    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "ApiDoc.xml");
    option.IncludeXmlComments(filePath);
    //option.OperationFilter<AddRequiredHeaderParameter>();
    option.UseAllOfToExtendReferenceSchemas();
    option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "Standard authorization header using the bearer scheme(\"Bearer {token}\")",
        In = ParameterLocation.Header,
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });
    option.AddSecurityRequirement(new OpenApiSecurityRequirement {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] { }
        }
    });
});

// Add services to the container.
builder.Services.AddHangfire(x => x.UseSqlServerStorage(dbConnectionString));
builder.Services.AddHangfireServer();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddLogging(c => c.AddFluentMigratorConsole())
.AddFluentMigratorCore()
.ConfigureRunner(c => c.AddSqlServer2016()
                .WithGlobalConnectionString(builder.Configuration.GetConnectionString("SqlConnection"))
                .ScanIn(Assembly.GetExecutingAssembly()).For.Migrations());
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseAuthorization();

app.MapControllers();

app.UseHangfireDashboard("/dashboard", new DashboardOptions
{
    Authorization = new[] { new HangfireAuthorizationFilter() },
    AppPath = "/swagger"
});
//app.MigrateDatabase().Run();
app.Run();

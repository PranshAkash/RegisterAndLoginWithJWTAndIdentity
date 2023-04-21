﻿using Data.Models.Enums;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    public class ApplicationUser : ApplicationUserProcModel
    {
        public string? RefreshToken { get; set; }
        public DateTime RefreshTokenExpiryTime { get; set; } = DateTime.Now.AddDays(30);
        public bool IsActive { get; set; }
    }


    public class ApplicationUserProcModel : IdentityUser<int>
    {
        public string UserId { get; set; }
        public string Role { get; set; }
        public string Name { get; set; }
    }

    public class UserUpdateRequest
    {
        public int Id { get; set; }
        public string PasswordHash { get; set; }
        public bool TwoFactorEnabled { get; set; }
        public string? RefreshToken { get; set; }
        public DateTime? RefreshTokenExpiryTime { get; set; }
    }
    public class AuthenticateResponse
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Role { get; set; }
        public string Token { get; set; }
        public string RefreshToken { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Gender { get; set; }
        public string BussinessTypes { get; set; }
        public string ReferralCode { get; set; }
        public string Referrallink { get; set; }


        public AuthenticateResponse(ApplicationUser user, string token)
        {
            user = user ?? new ApplicationUser();
            Id = user.Id;
            Username = user.UserName;
            Role = user.Role;
            Name = user.Name;
            RefreshToken = user.RefreshToken;
            Token = token;
            PhoneNumber = user.PhoneNumber;
            Email = user.Email;
        }
    }

    public class ApplicationUserResponse
    {
        public int Id { get; set; }
        public decimal Balance { get; set; }
        public UserRoles RoleId { get; set; }
        public string Role { get; set; }
        public string name { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public bool EmailConfirmed { get; set; }
        public string PhoneNumber { get; set; }
        public bool TwoFactorEnabled { get; set; }
        public bool LockoutEnabled { get; set; }
        public bool IsActive { get; set; }
        public string BussinessType { get; set; }
    }
}

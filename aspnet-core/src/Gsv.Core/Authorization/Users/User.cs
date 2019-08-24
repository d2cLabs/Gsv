using System;
using System.Collections.Generic;
using Abp.Authorization.Users;
using Abp.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace Gsv.Authorization.Users
{
    public class User : AbpUser<User>
    {
        public const string DefaultPassword = "123qwe";
        public const string UserDefaultPassword = "123456";

        public static string CreateRandomPassword()
        {
            return Guid.NewGuid().ToString("N").Truncate(16);
        }

        public static User CreateTenantAdminUser(int tenantId, string emailAddress)
        {
            var user = new User
            {
                TenantId = tenantId,
                UserName = AdminUserName,
                Name = AdminUserName,
                Surname = AdminUserName,
                EmailAddress = emailAddress,
                Roles = new List<UserRole>()
            };

            user.SetNormalizedNames();

            return user;
        }

        public static User CreateUser(int tenantId, string name, string password)
        {
            var user = new User
            {
                TenantId = tenantId,
                UserName = name,
                Name = name,
                Surname = name,
                EmailAddress = name + GsvConsts.UserEmailServerName,
                Roles = new List<UserRole>()
            };

            user.SetNormalizedNames();
            user.Password = new PasswordHasher<User>(new OptionsWrapper<PasswordHasherOptions>(new PasswordHasherOptions())).HashPassword(user, password);
            user.IsEmailConfirmed = true;
            user.IsActive = true;
            return user;
        }
    }
}

using System.Linq;
using Microsoft.EntityFrameworkCore;
using Gsv.Types;
using System.Collections.Generic;
using Gsv.Authorization.Roles;
using Abp.Authorization.Roles;
using Gsv.Authorization;
using Abp.Authorization.Users;
using Gsv.Authorization.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Abp.Configuration;

namespace Gsv.EntityFrameworkCore.Seed.Tenants
{
    public class DefaultRoleAndUserBuilder
    {
        private readonly GsvDbContext _context;
        private readonly int _tenantId;

        public DefaultRoleAndUserBuilder(GsvDbContext context, int tenantId)
        {
            _context = context;
            _tenantId = tenantId;
        }
        public void Create()
        {
            CreateRoleAndUser();
        }
        private void CreateRoleAndUser()
        {
            // supervisor role
            var role = _context.Roles.IgnoreQueryFilters().FirstOrDefault(r => r.TenantId == _tenantId && r.Name == StaticRoleNames.Tenants.Supervisor);
            if (role == null)
            {
                role = _context.Roles.Add(new Role(_tenantId, StaticRoleNames.Tenants.Supervisor, StaticRoleNames.Tenants.Supervisor) { IsStatic = true }).Entity;
                _context.SaveChanges();
            }
            
            _context.Permissions.Add(
                new RolePermissionSetting
                {
                    TenantId = _tenantId,
                    Name = PermissionNames.Pages_Supervisor,
                    IsGranted = true,
                    RoleId = role.Id
                }
            );
            _context.SaveChanges();

            // watcher role
            role = _context.Roles.IgnoreQueryFilters().FirstOrDefault(r => r.TenantId == _tenantId && r.Name == StaticRoleNames.Tenants.Watcher);
            if (role == null)
            {
                role = _context.Roles.Add(new Role(_tenantId, StaticRoleNames.Tenants.Watcher, StaticRoleNames.Tenants.Watcher) { IsStatic = true }).Entity;
                _context.SaveChanges();
            }
            
            _context.Permissions.Add(
                new RolePermissionSetting
                {
                    TenantId = _tenantId,
                    Name = PermissionNames.Pages_Watcher,
                    IsGranted = true,
                    RoleId = role.Id
                }
            );
            _context.SaveChanges();
            
            // Watcher user
            var watcherUser = _context.Users.IgnoreQueryFilters().FirstOrDefault(u => u.TenantId == _tenantId && u.UserName == StaticRoleNames.Tenants.Watcher);
            if (watcherUser == null)
            {
                watcherUser = new User
                {
                    TenantId = _tenantId,
                    UserName = StaticRoleNames.Tenants.Watcher,
                    Name = StaticRoleNames.Tenants.Watcher,
                    Surname = StaticRoleNames.Tenants.Watcher,
                    EmailAddress = "watcher@defaulttenant.com",
                    Roles = new List<UserRole>()
                };

                watcherUser.SetNormalizedNames();

                watcherUser.Password = new PasswordHasher<User>(new OptionsWrapper<PasswordHasherOptions>(new PasswordHasherOptions())).HashPassword(watcherUser, "123qwe");
                watcherUser.IsEmailConfirmed = true;
                watcherUser.IsActive = true;

                _context.Users.Add(watcherUser);
                _context.SaveChanges();

                // Assign Watcher role to watcher user
                _context.UserRoles.Add(new UserRole(_tenantId, watcherUser.Id, role.Id));
                _context.SaveChanges();
            }
        }
    }
}

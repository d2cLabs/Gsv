using System.Linq;
using Microsoft.EntityFrameworkCore;
using Abp.Authorization.Roles;
using Abp.Authorization.Users;
using Gsv.Authorization;
using Gsv.Authorization.Roles;
using Gsv.Authorization.Users;

namespace Gsv.EntityFrameworkCore.Seed.Tenants
{
    public class TenantRoleAndUserBuilder
    {
        private readonly GsvDbContext _context;
        private readonly int _tenantId;

        public TenantRoleAndUserBuilder(GsvDbContext context, int tenantId)
        {
            _context = context;
            _tenantId = tenantId;
        }

        public void Create()
        {
            // Admin RoleAndUser
            string[] permissions = new string[] {
                PermissionNames.Pages_Setup, PermissionNames.Pages_Types, PermissionNames.Pages_Objects, PermissionNames.Pages_Staffing
            };
            CreateRoleAndUser(StaticRoleNames.Tenants.Admin, permissions, AbpUserBase.AdminUserName, User.DefaultPassword);

            // Supervisor RoleAndUser
            permissions = new string[] { PermissionNames.Pages_Supervisor };
            CreateRole(StaticRoleNames.Tenants.Supervisor, permissions);

            
            // Watcher
            permissions = new string[] { PermissionNames.Pages_Watcher };

            CreateRole(StaticRoleNames.Tenants.Watcher, permissions);

        }

        private void CreateRoleAndUser(string roleName, string[] permissions, string userName, string password)
        {
            var role = CreateRole(roleName, permissions);
            if (role == null) 
                return;
            // user
            var user = _context.Users.IgnoreQueryFilters().FirstOrDefault(u => u.TenantId == _tenantId && u.UserName == userName);
            if (user == null)
            {
                user = User.CreateUser(_tenantId, userName, password);
                _context.Users.Add(user);
                _context.SaveChanges();

                // Assign role to user
                _context.UserRoles.Add(new UserRole(_tenantId, user.Id, role.Id));
                _context.SaveChanges();
            }
        }

        private Role CreateRole(string name, string[] permissions)
        {
            // role
            var role = _context.Roles.IgnoreQueryFilters().FirstOrDefault(r => r.TenantId == _tenantId && r.Name == name);
            if (role == null)
            {
                role = _context.Roles.Add(new Role(_tenantId, name, name) { IsStatic = true }).Entity;
                _context.SaveChanges();

                // Grant permission to role
                _context.Permissions.AddRange(
                    permissions.Select(permission => new RolePermissionSetting 
                    { 
                        TenantId = _tenantId, 
                        Name = permission, 
                        IsGranted = true, 
                        RoleId = role.Id
                    })
                );
                _context.SaveChanges();
            }
             
            return role;
        }
    }
}

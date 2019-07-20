using Abp.Authorization;
using Gsv.Authorization.Roles;
using Gsv.Authorization.Users;

namespace Gsv.Authorization
{
    public class PermissionChecker : PermissionChecker<Role, User>
    {
        public PermissionChecker(UserManager userManager)
            : base(userManager)
        {
        }
    }
}

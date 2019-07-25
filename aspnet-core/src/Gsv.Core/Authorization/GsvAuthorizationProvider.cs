using Abp.Authorization;
using Abp.Localization;
using Abp.MultiTenancy;

namespace Gsv.Authorization
{
    public class GsvAuthorizationProvider : AuthorizationProvider
    {
        public override void SetPermissions(IPermissionDefinitionContext context)
        {
            context.CreatePermission(PermissionNames.Pages_Host, L("Host"), multiTenancySides: MultiTenancySides.Host);

            context.CreatePermission(PermissionNames.Pages_Setup, L("Setup"));
            context.CreatePermission(PermissionNames.Pages_Types, L("Types"));
            context.CreatePermission(PermissionNames.Pages_Objects, L("Objects"));
            context.CreatePermission(PermissionNames.Pages_Staffing, L("Staffing"));

            context.CreatePermission(PermissionNames.Pages_Watcher, L("Watcher"));
            context.CreatePermission(PermissionNames.Pages_Supervisor, L("Supervisor"));
        }

        private static ILocalizableString L(string name)
        {
            return new LocalizableString(name, GsvConsts.LocalizationSourceName);
        }
    }
}

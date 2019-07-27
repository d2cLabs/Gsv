using Abp.Application.Navigation;
using Abp.Localization;
using Gsv.Authorization;

namespace Gsv.Web.Startup
{
    /// <summary>
    /// This class defines menus for the application.
    /// </summary>
    public class GsvNavigationProvider : NavigationProvider
    {
        public override void SetNavigation(INavigationProviderContext context)
        {
            context.Manager.MainMenu
                // Host
                .AddItem(new MenuItemDefinition(PermissionNames.Pages_Host, new FixedLocalizableString("配置"), icon: "fa fa-home", requiredPermissionName: PermissionNames.Pages_Host)
                    .AddItem(new MenuItemDefinition("Host_Tenants", new FixedLocalizableString("租户"), url: "Tenants"))
                    .AddItem(new MenuItemDefinition("Admin_Roles", new FixedLocalizableString("用户角色"), url: "Roles"))

                // Setup
                ).AddItem(new MenuItemDefinition(PermissionNames.Pages_Setup, new FixedLocalizableString("配置"), icon: "fa fa-globe", requiredPermissionName: PermissionNames.Pages_Setup)
                    .AddItem(new MenuItemDefinition("Admin_Settings", new FixedLocalizableString("全局设置"), url: "TenantSettings"))
                    .AddItem(new MenuItemDefinition("Admin_Users", new FixedLocalizableString("用户"), url: "Users"))
                // Types
                ).AddItem(new MenuItemDefinition(PermissionNames.Pages_Types, new FixedLocalizableString("类型设置"), icon: "fa fa-list", requiredPermissionName: PermissionNames.Pages_Types)
                    .AddItem(new MenuItemDefinition("Type_Categories", new FixedLocalizableString("基本品类"), url: "Categories"))
                    .AddItem(new MenuItemDefinition("Type_Sources", new FixedLocalizableString("进货来源"), url: "Sources"))
                // Objects
                ).AddItem(new MenuItemDefinition(PermissionNames.Pages_Objects, new FixedLocalizableString("标的管理"), icon: "fa fa-file", requiredPermissionName: PermissionNames.Pages_Objects)
                    .AddItem(new MenuItemDefinition("Object_Capitals", new FixedLocalizableString("资本方"), url: "Capitals"))
                    .AddItem(new MenuItemDefinition("Object_Places", new FixedLocalizableString("场地"), url: "Places"))
                    .AddItem(new MenuItemDefinition("Object_Objects", new FixedLocalizableString("监管标的"), url: "Objects"))
                    .AddItem(new MenuItemDefinition("Object_CargoTypes", new FixedLocalizableString("场地品类"), url: "CargoTypes"))
                     .AddItem(new MenuItemDefinition("Object_PlaceShelves", new FixedLocalizableString("场地货架"), url: "PlaceShelves"))
                // Staffs
                ).AddItem(new MenuItemDefinition(PermissionNames.Pages_Staffing, new FixedLocalizableString("员工管理"), icon: "fa fa-file", requiredPermissionName: PermissionNames.Pages_Staffing)
                    .AddItem(new MenuItemDefinition("Staffing_Workers", new FixedLocalizableString("工作人员"), url: "Workers"))
                    .AddItem(new MenuItemDefinition("Staffing_PlaceWorkres", new FixedLocalizableString("场地员工"), url: "PlaceWorkers"))

                // Sites
                ).AddItem(new MenuItemDefinition(PermissionNames.Pages_Watcher, new FixedLocalizableString("场地监看"), icon: "fa fa-th-large", requiredPermissionName: PermissionNames.Pages_Watcher)
                    .AddItem(new MenuItemDefinition("Watcher_InStocks", new FixedLocalizableString("入库单"), url: ""))
                    .AddItem(new MenuItemDefinition("Watcher_OutStocks", new FixedLocalizableString("出库单"), url: ""))

                // Supervision
                ).AddItem(new MenuItemDefinition(PermissionNames.Pages_Supervisor, new FixedLocalizableString("全局监管"), icon: "fa fa-list", requiredPermissionName: PermissionNames.Pages_Supervisor)
                    .AddItem(new MenuItemDefinition("Supervisor_Home", new FixedLocalizableString("看板"), url: ""))
                    .AddItem(new MenuItemDefinition("Supervisor_InStocks", new FixedLocalizableString("入库单"), url: ""))
                    .AddItem(new MenuItemDefinition("Supervisor_OutStocks", new FixedLocalizableString("出库单"), url: ""))
                );
        }

        private static ILocalizableString L(string name)
        {
            return new LocalizableString(name, GsvConsts.LocalizationSourceName);
        }
    }
}

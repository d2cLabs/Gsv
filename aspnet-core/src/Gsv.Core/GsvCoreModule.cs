using Abp.Modules;
using Abp.Reflection.Extensions;
using Abp.Timing;
using Abp.Zero;
using Abp.Zero.Configuration;
using Gsv.Authorization.Roles;
using Gsv.Authorization.Users;
using Gsv.Configuration;
using Gsv.Localization;
using Gsv.MultiTenancy;
using Gsv.Objects.Cache;
using Gsv.Timing;
using Gsv.Types.Cache;

namespace Gsv
{
    [DependsOn(typeof(AbpZeroCoreModule))]
    public class GsvCoreModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.Auditing.IsEnabled = false;
            Configuration.Auditing.IsEnabledForAnonymousUsers = true;

            // Declare entity types
            Configuration.Modules.Zero().EntityTypes.Tenant = typeof(Tenant);
            Configuration.Modules.Zero().EntityTypes.Role = typeof(Role);
            Configuration.Modules.Zero().EntityTypes.User = typeof(User);

            GsvLocalizationConfigurer.Configure(Configuration.Localization);

            // Enable this line to create a multi-tenant application.
            Configuration.MultiTenancy.IsEnabled = GsvConsts.MultiTenancyEnabled;

            // Configure roles
            AppRoleConfig.Configure(Configuration.Modules.Zero().RoleManagement);

            Configuration.Settings.Providers.Add<AppSettingProvider>();
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(GsvCoreModule).GetAssembly());
            
            // Cache for Types
            IocManager.Register<ICategoryCache, CategoryCache>();
            //IocManager.Register<ITaskTypeCache, TaskTypeCache>();

            // Cache for Objects
            IocManager.Register<ICapitalCache, CapitalCache>();
            IocManager.Register<IPlaceCache, PlaceCache>();
            IocManager.Register<ICargoTypeCache, CargoTypeCache>();
        }

        public override void PostInitialize()
        {
            IocManager.Resolve<AppTimes>().StartupTime = Clock.Now;
        }
    }
}

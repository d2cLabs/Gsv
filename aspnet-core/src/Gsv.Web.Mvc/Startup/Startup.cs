using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Castle.Facilities.Logging;
using Abp.AspNetCore;
using Abp.Castle.Logging.Log4Net;
using Gsv.Authentication.JwtBearer;
using Gsv.Configuration;
using Gsv.Identity;
using Gsv.Web.Resources;
using Abp.AspNetCore.SignalR.Hubs;
using Senparc.CO2NET;
using Senparc.CO2NET.RegisterServices;
using Senparc.Weixin;
using Senparc.Weixin.Entities;
using Senparc.Weixin.RegisterServices;

namespace Gsv.Web.Startup
{
    public class Startup
    {
        private readonly IConfigurationRoot _appConfiguration;

        public Startup(IHostingEnvironment env)
        {
            _appConfiguration = env.GetAppConfiguration();
        }

        public IServiceProvider ConfigureServices(IServiceCollection services)
        {           
            // MVC
            services.AddMvc(
                options => options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute())
            );

            services.AddAuthentication()
                .AddCookie(options => {
                    options.LoginPath = new PathString("/WeixinAccount/Login/"); 
                });
                
            IdentityRegistrar.Register(services);
            AuthConfigurer.Configure(services, _appConfiguration);

            services.AddScoped<IWebResourceManager, WebResourceManager>();

            services.AddSignalR();

            // Weixin
            services.AddSenparcGlobalServices(_appConfiguration)            // Senparc.CO2NET
                    .AddSenparcWeixinServices(_appConfiguration);           // Senparc.Weixin

            // Configure Abp and Dependency Injection
            return services.AddAbp<GsvWebMvcModule>(
                // Configure Log4Net logging
                options => options.IocManager.IocContainer.AddFacility<LoggingFacility>(
                    f => f.UseAbpLog4Net().WithConfig("log4net.config")
                )
            );
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory,
            IOptions<SenparcSetting> senparcSetting, IOptions<SenparcWeixinSetting> senparcWeixinSetting)
        {
            app.UseAbp(); // Initializes ABP framework.

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            app.UseStaticFiles();

            app.UseAuthentication();

            app.UseJwtTokenMiddleware();

            app.UseSignalR(routes =>
            {
                routes.MapHub<AbpCommonHub>("/signalr");
            });

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "defaultWithArea",
                    template: "{area}/{controller=Home}/{action=Index}/{id?}");

                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

            // Start CO2NET register
            IRegisterService register = RegisterService.Start(env, senparcSetting.Value)
                .UseSenparcGlobal();
            // Weixin register
            register.UseSenparcWeixin(senparcWeixinSetting.Value, senparcSetting.Value);
        }
    }
}

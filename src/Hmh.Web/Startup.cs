// -----------------------------------------------------------------------
//  <copyright file="Startup.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2015 OSharp. All rights reserved.
//  </copyright>
//  <site>http://www.osharp.org</site>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2015-11-27 21:16</last-date>
// -----------------------------------------------------------------------

using System;

using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;

using OSharp.Autofac.Mvc;
using OSharp.AutoMapper;
using OSharp.Core.Caching;
using OSharp.Core.Dependency;
using OSharp.Core.Security;
using OSharp.Data.Entity;
using Hmh.Core.Identity;
using Hmh.Core.Identity.Models;
using Hmh.Web;
using OSharp.Logging.Log4Net;
using OSharp.Web.Mvc.Initialize;

using Owin;

[assembly: OwinStartup(typeof(Startup))]


namespace Hmh.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ICacheProvider provider = new RuntimeMemoryCacheProvider();
            CacheManager.SetProvider(provider, CacheLevel.First);

            // 有关如何配置应用程序的详细信息，请访问 http://go.microsoft.com/fwlink/?LinkID=316888
            IServicesBuilder builder = new ServicesBuilder();
            IServiceCollection services = builder.Build();
            services.AddDataServices();
            services.AddAutoMapperServices();
            services.AddOAuthServices();
            services.AddLog4NetServices();
            services.AddHmhServices(app);

            IIocBuilder mvcIocBuilder = new MvcAutofacIocBuilder(services);
            app.UseOsharpMvc(mvcIocBuilder);
            app.ConfigureOAuth(mvcIocBuilder.ServiceProvider);
            ConfigureAuth(app);
        }

        private static void ConfigureAuth(IAppBuilder app)
        {
            app.UseCookieAuthentication(new CookieAuthenticationOptions()
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/account/login"),
                LogoutPath = new PathString("/account/logout"),
                ExpireTimeSpan = TimeSpan.FromDays(7),
                CookieName = "__hmh_web_auth",
                CookiePath = "/",
                Provider = new CookieAuthenticationProvider()
                {
                    OnValidateIdentity = SecurityStampValidator.OnValidateIdentity<UserManager, User, int>(TimeSpan.FromMinutes(5),
                        (manager, user) =>
                        {
                            return manager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);
                        },
                        identity =>
                        {
                            return identity.GetUserId<int>();
                        })
                }
            });
            app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);
        }
    }
}
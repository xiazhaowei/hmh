// -----------------------------------------------------------------------
//  <copyright file="ServiceCollectionExtensions.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2015 OSharp. All rights reserved.
//  </copyright>
//  <site>http://www.osharp.org</site>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2015-11-27 22:19</last-date>
// -----------------------------------------------------------------------

using System.Web;
using System.Web.Mvc;

using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.DataProtection;

using OSharp.Core.Dependency;
using Hmh.Core.Identity;
using Hmh.Core.Identity.Models;
using OSharp.Web.Mvc.UI;

using Owin;
using System.Linq;

namespace Hmh.Admin
{
    public static class Extensions
    {
        public static void AddHmhServices(this IServiceCollection services, IAppBuilder app)
        {
            //Identity
            services.AddScoped<UserManager<User, int>, UserManager>();
            services.AddScoped<SignInManager<User, int>, SignInManager>();
            services.AddScoped<IAuthenticationManager>(_ => HttpContext.Current.GetOwinContext().Authentication);
            services.AddScoped<IDataProtectionProvider>(_ => app.GetDataProtectionProvider());

            //Security
            //services.AddScoped<FunctionMapStore>();
            //services.AddScoped<EntityMapStore>();

            //OAuth
            //services.AddScoped<OAuthClientStore>();
            //services.AddScoped<IClientRefreshTokenStore, OAuthClientRefreshTokenStore>();
        }

        /// <summary>
        /// 将登录结果转换为Ajax结果
        /// </summary>
        public static AjaxResult ToAjaxResult(this SignInStatus status)
        {
            switch (status)
            {
                case SignInStatus.Failure:
                    return new AjaxResult("用户名或密码错误", AjaxResultType.Error);
                case SignInStatus.LockedOut:
                    return new AjaxResult("用户因密码错误次数过多而被锁定，请稍后重试", AjaxResultType.Error);
                case SignInStatus.RequiresVerification:
                    return new AjaxResult("用户登录需要验证", AjaxResultType.Error);
                default:
                    return new AjaxResult("用户登录成功", AjaxResultType.Success);
            }
        }

        /// <summary>
        /// 数据转换为表格数据格式
        /// </summary>
        public static GridData<TData> ToGridData<TData>(this IQueryable<TData> dataResult)
        {
            return new GridData<TData>(dataResult, dataResult.Count());
        }
    }
}
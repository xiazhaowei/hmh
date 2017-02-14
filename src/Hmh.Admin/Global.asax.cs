// -----------------------------------------------------------------------
//  <copyright file="Global.asax.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2015 OSharp. All rights reserved.
//  </copyright>
//  <site>http://www.osharp.org</site>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2015-11-27 21:11</last-date>
// -----------------------------------------------------------------------

using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

using OSharp.Utility.Logging;
using OSharp.Web.Mvc.Routing;
using Hmh.Admin.App_Start;
using System.Web.Optimization;

namespace Hmh.Admin
{
    public class Global : HttpApplication
    {
        private ILogger _logger;

        protected void Application_Start(object sender, EventArgs e)
        {
            AreaRegistration.RegisterAllAreas();
            RegisterRoutes();

            //Bundle配置
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        private static void RegisterRoutes()
        {
            RouteCollection routes = RouteTable.Routes;
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapLowerCaseUrlRoute(
                "Default",
                "{controller}/{action}/{id}",
                new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                new[] { "Hmh.Admin.Controllers" });
        }

        protected void Application_Error(object sender, EventArgs e)
        {
            if (_logger == null)
            {
                _logger = LogManager.GetLogger<Global>();
            }
            Exception ex = Server.GetLastError();
            _logger.Fatal("全局异常", ex);
        }
    }
}
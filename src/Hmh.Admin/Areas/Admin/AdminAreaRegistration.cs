// -----------------------------------------------------------------------
//  <copyright file="AdminAreaRegistration.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2015 OSharp. All rights reserved.
//  </copyright>
//  <site>http://www.osharp.org</site>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2015-11-27 20:46</last-date>
// -----------------------------------------------------------------------

using System.Web.Mvc;

using OSharp.Web.Mvc.Routing;


namespace OSharp.Demo.Web.Areas.Admin
{
    public class AdminAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get { return "Admin"; }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapLowerCaseUrlRoute(
                "Admin_default",
                "admin/{controller}/{action}/{id}",
                new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                new[] { "Hmh.Admin.Areas.Admin.Controllers" }
                );
        }
    }
}
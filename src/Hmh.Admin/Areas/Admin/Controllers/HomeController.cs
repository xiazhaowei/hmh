// -----------------------------------------------------------------------
//  <copyright file="HomeController.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2015 OSharp. All rights reserved.
//  </copyright>
//  <site>http://www.osharp.org</site>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2015-11-27 20:47</last-date>
// -----------------------------------------------------------------------

using System.ComponentModel;
using System.Web.Mvc;

using OSharp.Web.Mvc;


namespace Hmh.Admin.Areas.Admin.Controllers
{
    [Description("管理-后台主页")]
    public class HomeController : AdminBaseController 
    {
        [AllowAnonymous]
        [Description("管理-后台首页")]
        public ActionResult Index()
        {
            return View("/App/admin/layout/layout.cshtml");
        }
    }
}
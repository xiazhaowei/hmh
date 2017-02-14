// -----------------------------------------------------------------------
//  <copyright file="HomeController.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2015 OSharp. All rights reserved.
//  </copyright>
//  <site>http://www.osharp.org</site>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2015-11-27 20:42</last-date>
// -----------------------------------------------------------------------

using System.ComponentModel;
using System.Linq.Dynamic;
using System.Threading.Tasks;
using System.Web.Mvc;

using Hmh.Core.Identity;
using Hmh.Core.Identity.Dtos;
using OSharp.Utility.Data;
using OSharp.Web.Mvc;


namespace Hmh.Admin.Controllers
{
    [Description("站点-网站主页")]
    public class HomeController : BaseController
    {
        public IIdentityContract IdentityContract { get; set; }

        [Description("主页-网站首页")]
        public async Task<ActionResult> Index()
        {
            if (IdentityContract.Users.Count()<=2)
            {
                UserInputDto dto = new UserInputDto()
                {
                    RecommendId=0,
                    UserName = "admin",
                    NickName = "管理员",
                    Email = "admin@hh319.com",
                    EmailConfirmed = true,
                    PhoneNumber = "13561962764",
                    Password = "xbwf123456"
                };
                OperationResult result = await IdentityContract.CreateUsers(dto);
                if (!result.Successed)
                {
                    return Content("创建初始用户失败："+result.Message);
                }
                UserRoleMapInputDto mapDto = new UserRoleMapInputDto() { UserId = 1, RoleId = 1 };
                result = await IdentityContract.CreateUserRoleMaps(mapDto);
                if (!result.Successed)
                {
                    return Content("给初始用户赋角色失败：" + result.Message);
                }
            }

            return View();
        }
    }
}
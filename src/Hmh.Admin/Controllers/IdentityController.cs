// -----------------------------------------------------------------------
//  <copyright file="IdentityController.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2016 OSharp. All rights reserved.
//  </copyright>
//  <site>http://www.osharp.org</site>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2016-02-27 23:15</last-date>
// -----------------------------------------------------------------------

using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;

using OSharp.Core.Extensions;
using Hmh.Core.Identity;
using Hmh.Core.Identity.Dtos;
using Hmh.Core.Identity.Models;
using OSharp.Utility.Data;
using OSharp.Utility.Extensions;
using OSharp.Web.Mvc.Security;
using OSharp.Web.Mvc.UI;


namespace Hmh.Admin.Controllers
{
    [Description("网站-身份认证")]
    public class IdentityController : Controller
    {
        /// <summary>
        /// 获取或设置 身份认证业务契约
        /// </summary>
        public IIdentityContract IdentityContract { get; set; }

        /// <summary>
        /// 获取或设置 用户管理器
        /// </summary>
        public UserManager UserManager { get; set; }
        
        /// <summary>
        /// 获取或设置 登录管理器
        /// </summary>
        public SignInManager SignInManager { get; set; }

        private IAuthenticationManager AuthenticationManager
        {
            get { return HttpContext.GetOwinContext().Authentication; }
        }

        [Description("网站-身份认证-用户登录")]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [Description("网站-身份认证-用户登录")]
        public async Task<ActionResult> Login(LoginInfo loginInfo)
        {
            if (!ModelState.IsValid)
            {
                return Json(new AjaxResult("提交信息验证失败", AjaxResultType.Error));
            }
            if (!SecurityHelper.CheckVerify(loginInfo.VerifyCode, true))
            {
                return Json(new AjaxResult("验证码错误，请刷新重试", AjaxResultType.Error));
            }
            OperationResult<User> result = await IdentityContract.Login(loginInfo, true);
            if (!result.Successed)
            {
                return Json(result.ToAjaxResult());
            }
            User user = result.Data;
            await SignInManager.SignInAsync(user, loginInfo.Remember, true);
            IList<string> roles = await UserManager.GetRolesAsync(user.Id);
            var data = new
            {
                User = new { UserId = user.Id, user.UserName, user.NickName, user.Email, UserRole = roles.ExpandAndToString() },
                SessionId = Session.SessionID
            };
            return Json(new AjaxResult("登录成功", AjaxResultType.Success, data));
        }

        [HttpPost]
        [Description("网站-身份认证-退出登录")]
        public ActionResult Logout()
        {
            AuthenticationManager.SignOut();
            return Json(new AjaxResult("退出成功", AjaxResultType.Success));
        }

        [HttpPost]
        [Description("网站-身份认证-用户信息")]
        public ActionResult UserProfile()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return Json(null);
            }
            ClaimsIdentity identity = User.Identity as ClaimsIdentity;
            if (identity == null)
            { 
                return Json(null);
            }
            int userId = identity.GetUserId<int>();
            string userName = identity.GetUserName();
            string nickName = identity.GetClaimValue(ClaimTypes.GivenName);
            string email = identity.GetClaimValue(ClaimTypes.Email);
            string roles = identity.FindAll(ClaimTypes.Role).Select(m => m.Value).ExpandAndToString();
            var data = new
            {
                User = new { UserId = userId, UserName = userName, NickName = nickName, Email = email, UserRole = roles },
                SessionId = Session.SessionID
            };
            return Json(data);
        }

        public async Task<ActionResult> SetPassword(string password)
        {
            User user = await UserManager.FindByNameAsync("admin");
            if (user == null)
            {
                return Content("用户不存在");
            }
            if (password.IsMissing())
            {
                password = "gmf31529019"; 
            }
            IdentityResult result = user.PasswordHash == null
                ? await UserManager.AddPasswordAsync(user.Id, password)
                : await UserManager.ChangePasswordAsync(user.Id, password, password);
            return Content(result.Succeeded ? "密码更改成功" : result.Errors.ExpandAndToString());
        }
    }
}
using Hmh.Core.Identity;
using Hmh.Core.Identity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Hmh.Web.App_Code
{
    /// <summary>
    /// 自定义Filter  未用
    /// </summary>
    public class CurrentUserResultAttribute : FilterAttribute, IResultFilter
    {
        
        public void OnResultExecuting(ResultExecutingContext filterContext)
        {
            //判断登录状态
            if (filterContext.HttpContext.User.Identity.IsAuthenticated)
            {
                User user = IdentityContract.Users.SingleOrDefault(u => u.UserName == filterContext.HttpContext.User.Identity.Name);
                if (user == null)
                {
                    filterContext.HttpContext.GetOwinContext().Authentication.SignOut();
                    filterContext.HttpContext.Response.Redirect("/account/login");
                }
                else
                {
                    filterContext.Controller.TempData["CurrentUser"] = user;
                }
            }
        }
        public void OnResultExecuted(ResultExecutedContext filterContext)
        {
            

            
        }
        /// <summary>
        /// 获取或设置 身份认证业务对象
        /// </summary>
        public IIdentityContract IdentityContract { get; set; }
        
    }
}
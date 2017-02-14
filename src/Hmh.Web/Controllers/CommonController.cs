using Hmh.Core.Identity;
using Hmh.Core.Identity.Models;
using Hmh.Core.Shop;
using Hmh.Core.Goods;
using Hmh.Web.ViewModels;
using OSharp.Web.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Hmh.Core.Order;

namespace Hmh.Web.Controllers
{
    [Description("公共控制器")]
    public class CommonController : BaseController
    {
        
        /// <summary>
        /// 获取或设置 身份认证业务对象
        /// </summary>
        public IIdentityContract IdentityContract { get; set; }

        /// <summary>
        /// 获取或设置 店铺业务对象
        /// </summary>
        public IShopContract ShopContract { get; set; }

        /// <summary>
        /// 获取或设置 商品业务对象
        /// </summary>
        public IGoodsContract GoodsContract { get; set; }

        /// <summary>
        /// 获取或设置 订单业务对象
        /// </summary>
        public IOrderContract OrderContract { get; set; }
        /// <summary>
        /// 获取当前登录用户
        /// </summary>
        protected User CurrentUser
        {
            get
            {          
                //是否要启用缓存     

                //判断登录状态
                if(!User.Identity.IsAuthenticated)
                {
                    return null;
                }

                User user = IdentityContract.Users.SingleOrDefault(u => u.UserName == User.Identity.Name);
                if (user == null)
                {
                    HttpContext.GetOwinContext().Authentication.SignOut();
                    RedirectToAction("Login", "Account");
                }
                else
                {
                    //用户的扩展信息
                    Trace.WriteLine(user.UserExtend.Id);
                    //操作用户的店铺信息
                    Hmh.Core.Shop.Models.Shop shop = ShopContract.Shops.SingleOrDefault(s => s.User.Id == user.Id);
                    if (shop != null)
                        user.Shop = shop;
                }
                return user;
            }
        }

        /// <summary>
        /// 设置当前用户到tempData
        /// </summary>
        protected void SetMyAccount()
        {
            if (CurrentUser != null)
            {
                MyAccountViewModel myAccountViewModel = new MyAccountViewModel
                {
                    User = CurrentUser,
                    MyNewInvoteCount = CurrentUser.MyRecommends.Where(ru => ru.CreatedTime > DateTime.Now.AddDays(-7)).Select(us => us.Id).ToList().Count
                };

                ViewBag.MyAccount = myAccountViewModel;
            }           
        }

        
        /// <summary>
        /// 输出错误到viewmodel
        /// </summary>
        /// <param name="Message"></param>
        protected void AddErrors(string Message)
        {
            ModelState.AddModelError("", Message);           
        }

        /// <summary>
        /// 重写 Action执行方法 加载当前用户到tempdata中
        /// </summary>
        /// <param name="filterContext"></param>
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            SetMyAccount();
        }
    }
}
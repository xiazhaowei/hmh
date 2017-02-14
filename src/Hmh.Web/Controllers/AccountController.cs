// -----------------------------------------------------------------------
//  <copyright file="AccountController.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2015 OSharp. All rights reserved.
//  </copyright>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2015-06-29 22:32</last-date>
// -----------------------------------------------------------------------

using System.ComponentModel;
using System.Web.Mvc;

using OSharp.Web.Mvc;
using System.Threading.Tasks;
using Hmh.Core.Identity;
using Hmh.Core.Identity.Models;
using Hmh.Core.Identity.Dtos;
using OSharp.Utility.Data;
using OSharp.Utility.Extensions;
using Hmh.Web.ViewModels;
using OSharp.Web.Mvc.UI;
using System.Collections.Generic;
using Microsoft.Owin.Security;
using System.Web;
using System.Linq;
using Hmh.Core.Shop.Dtos;
using OSharp.Utility;
using Hmh.Core.Extensions;
using OSharp.Utility.Filter;
using System.Linq.Expressions;
using System;
using OSharp.Core.Data.Extensions;
using OSharp.Web.Mvc.Extensions;
using Hmh.Core.Utils;
using OSharp.Core.Mapping;
using Newtonsoft.Json;
using System.Reflection;

namespace Hmh.Web.Controllers
{
    [Authorize]
    [Description("网站-账户")]
    public class AccountController : CommonController
    {
        /// <summary>
        /// 用户中心-暂时未用
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }

        #region 登录/注册
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

        /// <summary>
        /// 发送邮箱验证码
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        public ActionResult SendEmailCode(string email)
        {
            //发送验证邮件
            SmtpMail smtpmail = new SmtpMail();


            string code = new Random().GetRandomNumberString(4);
            Session["Reg_EmailCode"] = code;

            smtpmail.Body = "您的验证码是：{0}".FormatWith(code);
            smtpmail.To = email;
            smtpmail.Subject = "验证邮件";
            smtpmail.Send();

            return Json(new AjaxResult("发送成功"));
        }

        /// <summary>
        /// 检查名字的唯一性
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> CheckName(string name)
        {
            //检查
            bool isExit = await IdentityContract.CheckUserExists(user => user.UserName == name);
            var result = new { isQnique = !isExit };
            return Json(result);
        }

        /// <summary>
        /// 检查邮箱的唯一性
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> CheckEmail(string name)
        {
            //检查
            bool isExit = await IdentityContract.CheckUserExists(user => user.Email == name);
            var result = new { isQnique = !isExit };
            return Json(result);
        }

        /// <summary>
        /// 登录-视图
        /// </summary>
        /// <param name="returnUrl"></param>
        /// <returns></returns>
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        { 
            if (returnUrl.IsNullOrEmpty())
                returnUrl = "/";
            ViewBag.ReturnUrl = returnUrl;
            return View();            
        }

        /// <summary>
        /// 登录提交
        /// </summary>
        /// <param name="loginInfo"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> Login(LoginInfo loginInfo)
        {           
            if (!ModelState.IsValid)
            {
                return Json(new AjaxResult("提交信息验证失败", AjaxResultType.Error));
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
                User = new { UserId = user.Id, user.UserName, user.NickName, user.Email, UserRole = roles.ExpandAndToString() }
            };
            return Json(new AjaxResult("登录成功", AjaxResultType.Success, data));            
                        
        }


        /// <summary>
        /// 退出登录
        /// </summary>
        /// <returns></returns>
        [HttpPost]        
        [AllowAnonymous]
        public ActionResult Logout(string returnUrl)
        {            
            AuthenticationManager.SignOut();
            if (returnUrl != null)
                return Redirect(returnUrl);
            return RedirectToAction("Index", "Home");
        }

        /// <summary>
        /// 注册视图
        /// </summary>
        /// <param name="recommendId"></param>
        /// <returns></returns>
        [AllowAnonymous]
        public ActionResult Register(string recommendId)
        {
            return View(new UserInputDto {
                RecommendId= recommendId.ToIntDefault()
            });
        }

        /// <summary>
        /// 注册提交
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            model.CheckNotNull(nameof(model));

            if (Session["Reg_EmailCode"] == null || Session["Reg_EmailCode"].ToString() != model.EmailCode)
            {
                return Json(new AjaxResult("验证码错误", AjaxResultType.Error));
            }
            
            UserInputDto dto = model.MapTo<UserInputDto>();
            dto.NickName = dto.UserName;           
            dto.RecommendId = dto.RecommendId == 0 ? 1 : dto.RecommendId;
            dto.EmailConfirmed = true; //邮箱通过验证
           
            OperationResult result =await IdentityContract.CreateUsers(dto);
            if (result.ResultType==OperationResultType.Success)
            {
                //初始化用户角色
                User newuser = IdentityContract.Users.SingleOrDefault(u => u.UserName == dto.UserName);
                if (newuser != null)
                {
                    UserRoleMapInputDto mapDto = new UserRoleMapInputDto() { UserId = newuser.Id, RoleId = 2 };
                    result = await IdentityContract.CreateUserRoleMaps(mapDto);
                    if (!result.Successed)
                    {
                        return Json(new AjaxResult(result.Message, AjaxResultType.Error));
                    }
                }
                #region 用户登录
                LoginInfo loginInfo = new LoginInfo
                {                        
                    UserName = dto.UserName,
                    Password= dto.Password,
                    Remember=false                        
                };                    
                OperationResult<User> loginresult = await IdentityContract.Login(loginInfo, true);
                if(loginresult.ResultType==OperationResultType.Success)
                {
                    User user = loginresult.Data;
                    AuthenticationManager.SignOut();
                    await SignInManager.SignInAsync(user, loginInfo.Remember, true);                    
                }
                #endregion
                return Json(new AjaxResult("登录成功", AjaxResultType.Success));
            }
            else
            {
                return Json(new AjaxResult(result.Message, AjaxResultType.Error));
            }
            
        }


        /// <summary>
        /// 注册欢迎页
        /// </summary>
        /// <returns></returns>
        public ActionResult Welcome()
        {
            return View();
        }

        /// <summary>
        /// 用户使用协议
        /// </summary>
        /// <returns></returns>
        public ActionResult Agreement()
        {
            return View();
        }

        #endregion         

        #region 编辑个人资料
        public ActionResult EditProfile()
        {
            return View();
        }

        public ActionResult GetEditProfile()
        {
            var userExtendInputDto = new UserExtendInputDto {
                NickName = CurrentUser.NickName,
                PhoneNumber = CurrentUser.PhoneNumber,
                Sex = CurrentUser.UserExtend.Sex,
                Birthday = CurrentUser.UserExtend.Birthday
            };
            string json = JsonConvert.SerializeObject(userExtendInputDto);
            return Content(json, "applicatin/json");            
        }

        /// <summary>
        /// 更新用户基本资料
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> EditProfile(EditProfileViewModel model)
        {
            UserExtendInputDto dto = new UserExtendInputDto {
                Id=CurrentUser.Id,
                NickName=model.NickName,
                PhoneNumber=model.PhoneNumber,
                Sex=model.Sex,
                Birthday=model.Birthday
            };
            OperationResult result= await IdentityContract.UpdateUserBase(dto);

            return Json(result.ToAjaxResult());
        }
        #endregion

        #region 安全中心

        public ActionResult SafeCenter()
        {
            return View();
        }

        public ActionResult SelectVerifyType()
        {
            return View();
        }

        public ActionResult PasswordEmailVerify()
        {
            return View();
        }

        public ActionResult SetNewPassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> SetNewPassword(UserPasswordInputDto dto)
        {
            dto.CheckNotNull(nameof(dto));
            dto.Id = CurrentUser.Id;

            OperationResult result =await IdentityContract.UpdateUserPassword(dto);
            return Json(result.ToAjaxResult());
        }

        [HttpPost]
        public ActionResult PasswordEmailVerify(string emailCode)
        {
            if (Session["Pwd_EmailCode"] == null || Session["Pwd_EmailCode"].ToString() != emailCode)
            {
                return Json(new AjaxResult("验证码错误", AjaxResultType.Error));
            }
            return Json(new AjaxResult("验证成功", AjaxResultType.Success));
        }
        /// <summary>
        /// 发送邮箱验证码
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        public ActionResult SendEmailVerifyCode(string email)
        {
            //发送验证邮件
            SmtpMail smtpmail = new SmtpMail();

            string code = new Random().GetRandomNumberString(4);
            Session["Pwd_EmailCode"] = code;

            //应该使用配置文件
            smtpmail.Body = "您的验证码是：{0}".FormatWith(code);
            smtpmail.To = email;
            smtpmail.Subject = "验证邮件";
            smtpmail.Send();

            return Json(new AjaxResult("发送成功"));
        }

        #endregion

        #region 收件地址管理
        public ActionResult MyDeliverAddress()
        {            
            return View();
        }

        public ActionResult GetMyDeliverAddress()
        {
            var myDeliverAddress = CurrentUser.DeliverAddresses.Select(da => new {
                da.Id,
                da.Name,
                da.Region,
                da.Mobile,
                da.DetailAddress,
                da.Zip,
                da.IsDefault
            });
            return Json(myDeliverAddress);
        }

        public ActionResult DelDeliverAddress(int? id)
        {
            OperationResult result = OperationResult.NoChanged;
            if (id.HasValue && id.Value > 0)
            {
                result = IdentityContract.DeleteDeliverAddresses(id.Value);
            }
            return Json(result.ToAjaxResult());
        }
        /// <summary>
        /// 添加收件地址
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult CreateDeliverAddress(DeliverAddressInputDto dto)
        {
            dto.CheckNotNull(nameof(dto));
            dto.UserId = CurrentUser.Id;
            OperationResult result;
            if(dto.Id>0)
                result = IdentityContract.EditDeliverAddresses(dto);
            else
                result = IdentityContract.AddDeliverAddresses(dto);

            return Json(result.ToAjaxResult());
            
        }
        #endregion

        #region 银行卡管理
        public ActionResult MyBankCard()
        {            
            return View();
        }

        public ActionResult GetMyBankCard()
        {
            var myBankCards = CurrentUser.BankCards.Select(bc=>new {
                bc.Id,
                bc.UserName,
                bc.BankName,
                bc.CardNumber
            });
            return Json(myBankCards);
        }


        /// <summary>
        /// 添加收件地址
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult CreateBankCard(UserBankCardInputDto dto)
        {
            dto.CheckNotNull(nameof(dto));
            dto.UserId = CurrentUser.Id;
            OperationResult result = IdentityContract.AddUserBankCards(dto);
            return Json(result.ToAjaxResult());

        }

        public ActionResult DelBankCard(int? id)
        {
            OperationResult result = OperationResult.NoChanged;
            if (id.HasValue && id.Value > 0)
            {
                result = IdentityContract.DeleteUserBankCards(id.Value);
            }
            return Json(result.ToAjaxResult());
        }

        #endregion

        #region 资金管理        

        public ActionResult GetMoneyLog()
        {
            GridRequest request = new GridRequest(Request);
            
            Expression<Func<RmbCoinTransaction, bool>> predicate = FilterHelper.GetExpression<RmbCoinTransaction>(request.FilterGroup);
            var page = IdentityContract.RmbCoinTransactions.Where(t=>t.User.Id==CurrentUser.Id).OrderByDescending(t=>t.Id).ToPage(predicate,
                request.PageCondition,
                m => new
                {
                    m.Id,
                    m.StreamId,
                    m.CreatedTime,
                    m.Amount,
                    m.Direction,                                      
                    m.Preferential,
                    m.Fee,
                    m.RealAmount,
                    m.Type,
                    m.State,
                    m.Remark
                    
                });
            return Json(page.ToGridData(), JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetHMoneyLog()
        {
            GridRequest request = new GridRequest(Request);
            Expression<Func<HCoinTransaction, bool>> predicate = FilterHelper.GetExpression<HCoinTransaction>(request.FilterGroup);
            var page = IdentityContract.HCoinTransactions.ToPage(predicate,
                request.PageCondition,
                m => new
                {
                    m.Id,
                    m.StreamId,
                    m.CreatedTime,
                    m.Amount,
                    m.Direction,
                    m.Preferential,
                    m.Fee,
                    m.RealAmount,
                    m.Type,
                    m.State,
                    m.Remark

                });
            return Json(page.ToGridData(), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 转账 检查收款人是否存在
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GetUserInfo(string userName)
        {
            userName.CheckNotNullOrEmpty(userName);
            User user = IdentityContract.Users.SingleOrDefault(u => u.UserName == userName);
            if(user==null)
            {
                return Json(new AjaxResult("用户不存在", AjaxResultType.Error));
            }
            return Json(new AjaxResult("收款方：({0})".FormatWith(user.NickName), AjaxResultType.Success));
        }

        /// <summary>
        /// 提现
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> CreateWidthdraw(WidthdrawInfo info)
        {
            info.CheckNotNull(nameof(info));

            info.UserId = CurrentUser.Id;

            OperationResult result = await IdentityContract.Widthdraw(info);
            return Json(result.ToAjaxResult());
        }

        /// <summary>
        /// 转账
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> CreateTransaction(TransactionInfo info)
        {
            info.CheckNotNull(nameof(info));

            info.UserId = CurrentUser.Id;
            OperationResult result = await IdentityContract.Transaction(info);
            return Json(result.ToAjaxResult());
        }

        public ActionResult MoneyLog()
        {
            return View();
        }

        public ActionResult HMoneyLog()
        {
            return View();
        }        

        #endregion

        #region 订单管理

        public ActionResult MyOrderList()
        {
            return View();
        }

        #endregion

        #region 收藏
        /// <summary>
        /// 检查收藏状态
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [AllowAnonymous]
        public ActionResult CheckCollect(CollectViewModel model)
        {
            model.CheckNotNull(nameof(model));
            if(CurrentUser==null)
            {
                return Json(new AjaxResult("未登录",AjaxResultType.Error));
            }
            Collect collect = IdentityContract.Collects.SingleOrDefault(c => c.User.Id==CurrentUser.Id && c.AboutId == model.AboutId && c.Type == CollectType.Goods);
            if(collect==null)
            {
                return Json(new AjaxResult("收藏", AjaxResultType.Error));
            }
            return Json(new AjaxResult("已收藏", AjaxResultType.Success));
        }
        [AllowAnonymous]
        public ActionResult DoCollect(CollectViewModel model)
        {
            model.CheckNotNull(nameof(model));
            if (CurrentUser == null)
            {
                return Json(new AjaxResult("未登录", AjaxResultType.Error));
            }
            //获取收藏类型
            CollectType collectType = GetCollectType(model.Type);
            //检查收藏状态
            Collect collect = IdentityContract.Collects.SingleOrDefault(c =>c.User.Id==CurrentUser.Id && c.AboutId == model.AboutId && c.Type == collectType);
            if (collect == null)
            {
                CollectInputDto dto = new CollectInputDto()
                {
                    UserId = CurrentUser.Id,
                    AboutId = model.AboutId,
                    Pic = "default",
                    Type = collectType
                };

                if (collectType==CollectType.Goods)
                {
                    //检查商品
                    Hmh.Core.Goods.Models.Goods goods = GoodsContract.Goodss.SingleOrDefault(g => g.Id == model.AboutId);
                    if(goods==null)
                    {
                        return Json(new AjaxResult("错误商品不存在", AjaxResultType.Error));
                    }
                    dto.Name = goods.Name;
                }
                else
                {
                    //检查店铺
                    Hmh.Core.Shop.Models.Shop shop = ShopContract.Shops.SingleOrDefault(s => s.Id == model.AboutId);
                    if(shop==null)
                    {
                        return Json(new AjaxResult("错误店铺不存在", AjaxResultType.Error));
                    }
                    dto.Name = shop.Name;
                }

                OperationResult result = IdentityContract.AddCollects(dto);   
                if(result.ResultType==OperationResultType.Success)
                    return Json(new AjaxResult("已收藏", AjaxResultType.Success));
                else
                    return Json(new AjaxResult("失败", AjaxResultType.Error));
            }
            else
            {
                OperationResult result = IdentityContract.DeleteCollects(collect.Id);
                if (result.ResultType == OperationResultType.Success)
                    return Json(new AjaxResult("收藏", AjaxResultType.Success));
                else
                    return Json(new AjaxResult("失败", AjaxResultType.Error));
            }

        }

        /// <summary>
        /// 字符串转换为枚举类型
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        private CollectType GetCollectType(string code)
        {
            code.CheckNotNullOrEmpty("code");
            Type type = typeof(CollectType);
            MemberInfo[] members = type.GetMembers(BindingFlags.Public | BindingFlags.Static);
            foreach(MemberInfo member in members)
            {
                CollectType collectType = member.Name.CastTo<CollectType>();
                if(collectType.ToString()==code)
                {
                    return collectType;
                }
            }
            throw new NotSupportedException("不支持的收藏类型"+code);
        }

        public ActionResult MyCollect()
        {
            return View();
        }

        public ActionResult GetMyGoodsCollects()
        {
            var myGoodsCollects = IdentityContract.Collects.Where(c=>c.User.Id==CurrentUser.Id && c.Type==CollectType.Goods).ToList().Select(c=>new {
                c.Name,
                c.Pic,
                c.CreatedTime,
                c.AboutId,
                c.Id,
                c.Type
            });
            return Json(myGoodsCollects);
        }

        public ActionResult DelCollect(int? id)
        {
            OperationResult result = OperationResult.NoChanged;
            if (id.HasValue && id.Value > 0)
            {
                result = IdentityContract.DeleteCollects(id.Value);
            }
            return Json(result.ToAjaxResult());
        }

        #endregion

        #region 我的邀请
        public ActionResult Invote()
        {
            return View();
        }

        public ActionResult MyInvotes()
        {
            return View();
        }
        
        [Description("管理-我的推荐用户-读取")]
        public ActionResult GetMyInvotes()
        {
            GridRequest request = new GridRequest(Request);
            //筛选指定用户
            Expression<Func<User, bool>> predicate = FilterHelper.GetExpression<User>(request.FilterGroup);
            var page = IdentityContract.Users.ToPage(predicate,
                request.PageCondition,
                m => new
                {
                    m.Id,
                    m.UserName,
                    m.NickName,
                    m.Email,
                    m.EmailConfirmed,
                    m.PhoneNumber,
                    m.PhoneNumberConfirmed,
                    m.CreatedTime
                });
            return Json(page.ToGridData(), JsonRequestBehavior.AllowGet);
        }


        #endregion


    }
}
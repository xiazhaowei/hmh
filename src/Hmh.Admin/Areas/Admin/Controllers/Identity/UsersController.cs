// -----------------------------------------------------------------------
//  <copyright file="UsersController.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2015 OSharp. All rights reserved.
//  </copyright>
//  <site>http://www.osharp.org</site>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2015-12-04 17:08</last-date>
// -----------------------------------------------------------------------

using System; 
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Web.Mvc;

using OSharp.Core.Data.Extensions;
using OSharp.Core.Security;
using Hmh.Core.Identity;
using Hmh.Core.Identity.Dtos;
using Hmh.Core.Identity.Models;
using OSharp.Utility;
using OSharp.Utility.Data;
using OSharp.Utility.Filter;
using OSharp.Web.Mvc;
using OSharp.Web.Mvc.Extensions;
using OSharp.Web.Mvc.UI;


namespace Hmh.Admin.Areas.Admin.Controllers
{
    [Description("管理-用户信息")]
    public class UsersController : AdminBaseController
    {
        public IIdentityContract IdentityContract { get; set; }

        [Description("管理-用户信息-读取")]
        [AllowAnonymous]
        public ActionResult Read()
        {
            GridRequest request = new GridRequest(Request);
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
            return Json(page.ToGridData(),JsonRequestBehavior.AllowGet);
        }

        [Description("管理-用户信息-读取节点")]
        public ActionResult ReadNode()
        {
            var nodes = IdentityContract.Users.OrderBy(m => m.UserName).Select(m => new
            {
                UserId = m.Id,
                UserName = m.UserName,
                NickName = m.NickName
            });
            return Json(nodes, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [RoleLimit]
        [Description("管理-用户信息-新增")]
        public async Task<ActionResult> Create(UserInputDto[] dtos)
        {
            dtos.CheckNotNull("dtos");
            OperationResult result = await IdentityContract.CreateUsers(dtos);
            return Json(result.ToAjaxResult());
        }

        [HttpPost]
        [RoleLimit]
        [Description("管理-用户信息-更新")]
        public async Task<ActionResult> Update(UserInputDto[] dtos)
        {
            dtos.CheckNotNull("dtos");
            OperationResult result = await IdentityContract.UpdateUsers(dtos);
            return Json(result.ToAjaxResult());
        }

        [HttpPost]
        [RoleLimit]
        [Description("管理-用户信息-删除")]
        public async Task<ActionResult> Delete(int[] ids)
        {
            ids.CheckNotNull("ids");
            OperationResult result = await IdentityContract.DeleteUsers(ids);
            return Json(result.ToAjaxResult());
        }
    }
}
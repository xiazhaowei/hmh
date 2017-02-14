// -----------------------------------------------------------------------
//  <copyright file="UserRoleMapsController.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2016 OSharp. All rights reserved.
//  </copyright>
//  <site>http://www.osharp.org</site>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2016-03-02 6:07</last-date>
// -----------------------------------------------------------------------

using System;
using System.ComponentModel;
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
    [Description("管理-用户角色")]
    public class UserRoleMapsController : AdminBaseController
    {
        /// <summary>
        /// 获取或设置 身份认证业务契约对象
        /// </summary>
        public IIdentityContract IdentityContract { get; set; }

        [Description("管理-用户角色-读取")]
        public ActionResult Read()
        {
            GridRequest request = new GridRequest(Request);
            Expression<Func<UserRoleMap, bool>> predicate = FilterHelper.GetExpression<UserRoleMap>(request.FilterGroup);
            var page = IdentityContract.UserRoleMaps.ToPage(predicate,
                request.PageCondition,
                m => new
                {
                    m.Id,
                    m.BeginTime,
                    m.EndTime,
                    m.IsLocked,
                    m.CreatedTime,
                    UserId = m.User.Id,
                    UserName = m.User.UserName,
                    NickName = m.User.NickName,
                    RoleId = m.Role.Id,
                    RoleName = m.Role.Name
                });
            return Json(page.ToGridData());
        }

        [HttpPost]
        [RoleLimit]
        [Description("管理-用户角色-新增")]
        public async Task<ActionResult> Create(UserRoleMapInputDto[] dtos)
        {
            dtos.CheckNotNull("dtos");
            OperationResult result = await IdentityContract.CreateUserRoleMaps(dtos);
            return Json(result.ToAjaxResult());
        }

        [HttpPost]
        [RoleLimit]
        [Description("管理-用户角色-更新")]
        public async Task<ActionResult> Update(UserRoleMapInputDto[] dtos)
        {
            dtos.CheckNotNull("dtos");
            OperationResult result = await IdentityContract.UpdateUserRoleMaps(dtos);
            return Json(result.ToAjaxResult());
        }

        [HttpPost]
        [RoleLimit]
        [Description("管理-用户角色-删除")]
        public async Task<ActionResult> Delete(int[] ids)
        {
            ids.CheckNotNull("ids");
            OperationResult result = await IdentityContract.DeleteUserRoleMaps(ids);
            return Json(result.ToAjaxResult());
        }
    }
}
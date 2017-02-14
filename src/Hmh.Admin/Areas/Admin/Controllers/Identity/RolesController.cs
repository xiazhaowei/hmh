// -----------------------------------------------------------------------
//  <copyright file="RolesController.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2015 OSharp. All rights reserved.
//  </copyright>
//  <site>http://www.osharp.org</site>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2015-12-06 2:23</last-date>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
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
    [Description("管理-角色信息")]
    public class RolesController : AdminBaseController
    {
        public IIdentityContract IdentityContract { get; set; }

        [Description("管理-角色信息-读取")]
        public ActionResult Read()
        {
            GridRequest request = new GridRequest(Request);
            Expression<Func<Role, bool>> predicate = FilterHelper.GetExpression<Role>(request.FilterGroup);
            var page = IdentityContract.Roles.ToPage(predicate,
                request.PageCondition,
                m => new
                {
                    m.Id,
                    m.Name,
                    m.Remark,
                    m.IsAdmin,
                    m.IsSystem,
                    m.IsLocked,
                    m.CreatedTime
                });
            return Json(page.ToGridData());
        }

        [Description("管理-角色信息-读取节点数据")]
        public ActionResult ReadNode()
        {
            var nodes = IdentityContract.Roles.OrderBy(m => m.Name).Select(m => new
            {
                RoleId = m.Id,
                RoleName = m.Name
            });
            return Json(nodes, JsonRequestBehavior.AllowGet);
        }

        [Description("管理-角色信息-读取复选框角色数据")]
        public ActionResult ReadCheckNode(int userId)
        {
            int[] checkRoleIds = IdentityContract.UserRoleMaps.Where(m => m.User.Id == userId).Select(m => m.Role.Id).Distinct().ToArray();
            var nodes = IdentityContract.Roles.OrderByDescending(m => m.IsAdmin).ThenBy(m => m.Id).Select(m => new
            {
                m.Id,
                m.Name,
                IsChecked = checkRoleIds.Contains(m.Id)
            }).ToList();
            return Json(nodes, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [RoleLimit]
        [Description("管理-角色信息-新增")]
        public async Task<ActionResult> Create(RoleInputDto[] dtos)
        {
            dtos.CheckNotNull("dtos" );
            OperationResult result = await IdentityContract.CreateRoles(dtos);
            return Json(result.ToAjaxResult());
        }

        [HttpPost]
        [RoleLimit]
        [Description("管理-角色信息-更新")]
        public async Task<ActionResult> Update(RoleInputDto[] dtos)
        {
            dtos.CheckNotNull("dtos");
            OperationResult result = await IdentityContract.UpdateRoles(dtos);
            return Json(result.ToAjaxResult());
        }

        [HttpPost]
        [RoleLimit]
        [Description("管理-角色信息-删除")]
        public async Task<ActionResult> Delete(int[] ids)
        {
            ids.CheckNotNull("ids" );
            OperationResult result = await IdentityContract.DeleteRoles(ids);
            return Json(result.ToAjaxResult());
        }
    }
}
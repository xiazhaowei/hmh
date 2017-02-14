// -----------------------------------------------------------------------
//  <copyright file="FunctionsController.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2015 OSharp. All rights reserved.
//  </copyright>
//  <site>http://www.osharp.org</site>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2015-12-04 16:55</last-date>
// -----------------------------------------------------------------------

using System;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Web.Mvc;

using OSharp.Core.Data.Extensions;
using OSharp.Core.Security;
using Hmh.Core.Security;
using Hmh.Core.Security.Dtos;
using OSharp.Utility;
using OSharp.Utility.Data;
using OSharp.Utility.Filter;
using OSharp.Web.Mvc;
using OSharp.Web.Mvc.Extensions;
using OSharp.Web.Mvc.UI;


namespace Hmh.Admin.Areas.Admin.Controllers
{
    [Description("管理-功能信息")]
    public class FunctionsController : AdminBaseController
    {
        public ISecurityContract SecurityContract { get; set; }

        [Description("管理-功能信息-读取")]
        public ActionResult Read()
        {
            GridRequest request = new GridRequest(Request);
            if (request.PageCondition.SortConditions.Length == 0)
            {
                request.PageCondition.SortConditions = new[]
                {
                    new SortCondition("Area"),
                    new SortCondition("Controller"),
                    new SortCondition("Action")
                };
            }
            Expression<Func<Function, bool>> predicate = FilterHelper.GetExpression<Function>(request.FilterGroup);
            var page = SecurityContract.Functions.ToPage(predicate,
                request.PageCondition,
                m => new
                {
                    m.Id,
                    m.Name,
                    m.Area,
                    m.Controller,
                    m.Action,
                    m.FunctionType,
                    m.IsTypeChanged,
                    m.OperateLogEnabled,
                    m.DataLogEnabled,
                    m.CacheExpirationSeconds,
                    m.IsCacheSliding,
                    m.PlatformToken,
                    m.IsController,
                    m.IsAjax,
                    m.IsChild,
                    m.IsLocked,
                    m.IsCustom,
                    m.IsDeleted,
                    m.Url
                });
            return Json(page.ToGridData());
        }

        [Description("管理-功能信息-读取节点")]
        public ActionResult ReadNode()
        {
            var nodes = SecurityContract.Functions.OrderBy(m => m.Name).Select(m => new
            {
                FunctionId = m.Id,
                FunctionName = m.Name
            });
            return Json(nodes, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [RoleLimit]
        [Description("管理-功能信息-新增")]
        public async Task<ActionResult> Create(FunctionInputDto[] dtos)
        {
            dtos.CheckNotNull("dtos" );
            OperationResult result = await SecurityContract.CreateFunctions(dtos);
            return Json(result.ToAjaxResult());
        }

        [HttpPost]
        [RoleLimit]
        [Description("管理-功能信息-更新")]
        public async Task<ActionResult> Update(FunctionInputDto[] dtos)
        {
            dtos.CheckNotNull("dtos" );
            OperationResult result = await SecurityContract.UpdateFunctions(dtos);
            return Json(result.ToAjaxResult());
        }

        [HttpPost]
        [RoleLimit]
        [Description("管理-功能信息-删除")]
        public async Task<ActionResult> Delete(Guid[] ids)
        {
            ids.CheckNotNull("ids" );
            OperationResult result = await SecurityContract.DeleteFunctions(ids);
            return Json(result.ToAjaxResult());
        }
    }

}
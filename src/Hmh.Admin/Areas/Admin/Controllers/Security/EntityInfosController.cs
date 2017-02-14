// -----------------------------------------------------------------------
//  <copyright file="EntityInfosController.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2015 OSharp. All rights reserved.
//  </copyright>
//  <site>http://www.osharp.org</site>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2015-12-04 16:55</last-date>
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
    [Description("管理-实体信息")]
    public class EntityInfosController : AdminBaseController
    {
        public ISecurityContract SecurityContract { get; set; }

        [Description("管理-实体信息-读取")]
        public ActionResult Read()
        {
            GridRequest request = new GridRequest(Request);
            if (request.PageCondition.SortConditions.Length == 0)
            {
                request.PageCondition.SortConditions = new[]
                {
                    new SortCondition("ClassName")
                };
            }
            Expression<Func<EntityInfo, bool>> predicate = FilterHelper.GetExpression<EntityInfo>(request.FilterGroup);
            var page = SecurityContract.EntityInfos.ToPage(predicate,
                request.PageCondition,
                m => new
                {
                    m.Id,
                    m.Name,
                    m.ClassName,
                    m.DataLogEnabled,
                    m.IsDeleted
                });
            return Json(page.ToGridData());
        }

        [HttpPost]
        [RoleLimit]
        [Description("管理-实体信息-更新")]
        public async Task<ActionResult> Update(EntityInfoInputDto[] dtos)
        {
            dtos.CheckNotNull("dtos");
            OperationResult result = await SecurityContract.UpdateEntityInfos(dtos);
            return Json(result.ToAjaxResult());
        }
    }
}
// -----------------------------------------------------------------------
//  <copyright file="FunctionUserMapsController.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2016 OSharp. All rights reserved.
//  </copyright>
//  <site>http://www.osharp.org</site>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2016-03-04 20:53</last-date>
// -----------------------------------------------------------------------

using System;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Web.Mvc;

using OSharp.Core.Data.Extensions;
using OSharp.Core.Security;
using Hmh.Core.Security;
using Hmh.Core.Security.Dtos;
using Hmh.Core.Security.Models;
using OSharp.Utility;
using OSharp.Utility.Data;
using OSharp.Utility.Filter;
using OSharp.Web.Mvc;
using OSharp.Web.Mvc.Extensions;
using OSharp.Web.Mvc.UI;


namespace Hmh.Admin.Areas.Admin.Controllers
{
    [Description("管理-用户功能")]
    public class FunctionUserMapsController : AdminBaseController
    {
        /// <summary>
        /// 获取或设置 安全模块业务契约对象
        /// </summary>
        public ISecurityContract SecurityContract { get; set; }

        [Description("管理-用户功能-读取")]
        public ActionResult Read()
        {
            GridRequest request = new GridRequest(Request);
            Expression<Func<FunctionUserMap, bool>> predicate = FilterHelper.GetExpression<FunctionUserMap>(request.FilterGroup);
            var page = SecurityContract.FunctionUserMaps.ToPage(predicate,
                request.PageCondition,
                m => new
                {
                    m.Id,
                    m.FilterType,
                    m.IsLocked,
                    m.CreatedTime,
                    UserId = m.User.Id,
                    UserName = m.User.UserName,
                    NickName = m.User.NickName,
                    FunctionId = m.Function.Id,
                    FunctionName = m.Function.Name
                });
            return Json(page.ToGridData());
        }

        [HttpPost]
        [RoleLimit]
        [Description("管理-用户功能-新增")]
        public async Task<ActionResult> Create(FunctionUserMapInputDto[] dtos)
        {
            dtos.CheckNotNull("dtos");
            OperationResult result = await SecurityContract.CreateFunctionUserMaps(dtos);
            return Json(result.ToAjaxResult());
        }

        [HttpPost]
        [RoleLimit]
        [Description("管理-用户功能-更新")]
        public async Task<ActionResult> Update(FunctionUserMapInputDto[] dtos)
        {
            dtos.CheckNotNull("dtos");
            OperationResult result = await SecurityContract.UpdateFunctionUserMaps(dtos);
            return Json(result.ToAjaxResult());
        }

        [HttpPost]
        [RoleLimit]
        [Description("管理-用户功能-删除")]
        public async Task<ActionResult> Delete(int[] ids)
        {
            ids.CheckNotNull("ids");
            OperationResult result = await SecurityContract.DeleteFunctionUserMaps(ids);
            return Json(result.ToAjaxResult());
        }
    }
}
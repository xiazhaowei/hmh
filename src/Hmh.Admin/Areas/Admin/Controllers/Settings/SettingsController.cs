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
using Hmh.Core.Settings;
using Hmh.Core.Settings.Dtos;
using OSharp.Utility;
using OSharp.Utility.Data;
using OSharp.Utility.Filter;
using OSharp.Web.Mvc;
using OSharp.Web.Mvc.Extensions;
using OSharp.Web.Mvc.UI;
using Hmh.Core.Settings.Models;

namespace Hmh.Admin.Areas.Admin.Controllers
{
    [Description("管理-系统参数信息")]
    public class SettingsController : AdminBaseController
    {
        public ISettingsContract SettingsContract { get; set; }

        [Description("管理-设置参数-读取")]
        public ActionResult Read()
        {
            GridRequest request = new GridRequest(Request);
            if (request.PageCondition.SortConditions.Length == 0)
            {
                request.PageCondition.SortConditions = new[]
                {
                    new SortCondition("Id")
                };
            }
            Expression<Func<SystemSetting, bool>> predicate = FilterHelper.GetExpression<SystemSetting>(request.FilterGroup);
            var page = SettingsContract.SystemSettings.ToPage(predicate,
                request.PageCondition,
                m => new
                {
                    m.Id,
                    m.Key,
                    m.Description,
                    m.Value
                });
            return Json(page.ToGridData());
        }
        [HttpPost]
        [RoleLimit]
        [Description("管理-角色信息-新增")]
        public ActionResult Create(SystemSettingInputDto[] dtos)
        {
            dtos.CheckNotNull("dtos");
            OperationResult result = SettingsContract.AddSystemSettings(dtos);
            return Json(result.ToAjaxResult());
        }


        [HttpPost]
        [RoleLimit]
        [Description("管理-设置参数-更新")]
        public ActionResult Update(SystemSettingInputDto[] dtos)
        {
            dtos.CheckNotNull("dtos");
            OperationResult result = SettingsContract.EditSystemSettings(dtos);
            return Json(result.ToAjaxResult());
        }
    }
}
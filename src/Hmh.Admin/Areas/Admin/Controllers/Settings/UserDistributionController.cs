﻿// -----------------------------------------------------------------------
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
    [Description("管理-三级分销信息")]
    public class UserDistributionController : AdminBaseController
    {
        public ISettingsContract SettingsContract { get; set; }

        [Description("管理-八代分润-读取")]
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
            Expression<Func<UserDistributionLevel, bool>> predicate = FilterHelper.GetExpression<UserDistributionLevel>(request.FilterGroup);
            var page = SettingsContract.UserDistributionLevels.ToPage(predicate,
                request.PageCondition,
                m => new
                {
                    m.Id,
                    m.Name,
                    m.Money,
                    m.Persent,
                    m.RewardType
                });
            return Json(page.ToGridData());
        }
        [HttpPost]
        [RoleLimit]
        [Description("管理-八代分润-新增")]
        public ActionResult Create(UserDistributionLevelInputDto[] dtos)
        {
            dtos.CheckNotNull("dtos");
            OperationResult result = SettingsContract.AddUserDistributionLevels(dtos);
            return Json(result.ToAjaxResult());
        }


        [HttpPost]
        [RoleLimit]
        [Description("管理-八代分润-更新")]
        public ActionResult Update(UserDistributionLevelInputDto[] dtos)
        {
            dtos.CheckNotNull("dtos");
            OperationResult result = SettingsContract.EditUserDistributionLevels(dtos);
            return Json(result.ToAjaxResult());
        }

        [HttpPost]
        [RoleLimit]
        [Description("管理-八代分润-删除")]
        public ActionResult Delete(int[] ids)
        {
            ids.CheckNotNull("ids");
            OperationResult result = SettingsContract.DeleteUserDistributionLevels(ids);
            return Json(result.ToAjaxResult());
        }
    }
}
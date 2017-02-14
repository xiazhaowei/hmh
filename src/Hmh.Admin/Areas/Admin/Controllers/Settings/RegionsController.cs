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
using Hmh.Core.Shop;
using Hmh.Core.Shop.Dtos;
using OSharp.Utility;
using OSharp.Utility.Data;
using OSharp.Utility.Filter;
using OSharp.Web.Mvc;
using OSharp.Web.Mvc.Extensions;
using OSharp.Web.Mvc.UI;
using Hmh.Core.Shop.Models;

namespace Hmh.Admin.Areas.Admin.Controllers
{
    [Description("管理-地区信息")]
    public class RegionsController : AdminBaseController
    {
        public IShopContract ShopContract { get; set; }

        [Description("管理-地区-读取")]
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
            Expression<Func<Region, bool>> predicate = FilterHelper.GetExpression<Region>(request.FilterGroup);
            var page = ShopContract.Regions.ToPage(predicate,
                request.PageCondition,
                m => new
                {
                    m.Id,                    
                    m.Province,
                    m.City,
                    m.County,
                    m.Street,
                    m.IsOpenServices
                });
            return Json(page.ToGridData());
        }
        [HttpPost]
        [RoleLimit]
        [Description("管理-地区-新增")]
        public ActionResult Create(RegionInputDto[] dtos)
        {
            dtos.CheckNotNull("dtos");
            OperationResult result = ShopContract.AddRegions(dtos);
            return Json(result.ToAjaxResult());
        }


        [HttpPost]
        [RoleLimit]
        [Description("管理-地区-更新")]
        public ActionResult Update(RegionInputDto[] dtos)
        {
            dtos.CheckNotNull("dtos");
            OperationResult result = ShopContract.EditRegions(dtos);
            return Json(result.ToAjaxResult());
        }

        [HttpPost]
        [RoleLimit]
        [Description("管理-地区-删除")]
        public ActionResult Delete(int[] ids)
        {
            ids.CheckNotNull("ids");
            OperationResult result = ShopContract.DeleteRegions(ids);
            return Json(result.ToAjaxResult());
        }
    }
}
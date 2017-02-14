// -----------------------------------------------------------------------
//  <copyright file="EntityInfosController.cs" company="OSharp开源团队">
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
using Hmh.Core.Goods;
using Hmh.Core.Goods.Dtos;
using Hmh.Core.Goods.Models;
using OSharp.Utility;
using OSharp.Utility.Data;
using OSharp.Utility.Filter;
using OSharp.Web.Mvc;
using OSharp.Web.Mvc.Extensions;
using OSharp.Web.Mvc.UI;
using Hmh.Admin.ViewModel;

namespace Hmh.Admin.Areas.Admin.Controllers
{
    [Description("管理-发布分类信息")]
    public class CategoryController : AdminBaseController
    {
        public IGoodsContract GoodsContract { get; set; }

        [Description("管理-发布分类-读取")]
        public ActionResult Read()
        {           

            GridRequest request = new GridRequest(Request);
            if (request.PageCondition.SortConditions.Length == 0)
            {
                request.PageCondition.SortConditions = new[]
                {
                    new SortCondition("SortCode")
                };
            }
            Expression<Func<Category, bool>> predicate = FilterHelper.GetExpression<Category>(request.FilterGroup);
            var page = GoodsContract.Categorys.ToPage(predicate,
                request.PageCondition,
                m => new CategoryTreeListNodeViewModel()
                {
                    Id = m.Id,
                    ParentId = m.Parent!=null? m.Parent.Id.ToString() :null,
                    Name=m.Name,
                    SortCode=m.SortCode,
                    Profit=m.Profit,
                    Distribution=m.Distribution
                });
            return Json(page.Data);
        }

        

        [HttpPost]
        [RoleLimit]
        [Description("管理-发布分类-新增")]
        public ActionResult Create(CategoryInputDto[] dtos)
        {
            dtos.CheckNotNull("dtos");
            OperationResult result = GoodsContract.AddCategorys(dtos);
            Category category = result.Data as Category;
            if (category != null)
                return Json(new CategoryTreeListNodeViewModel
                {
                    Id = category.Id,
                    ParentId = category.Parent != null ? category.Parent.Id.ToString() : null,
                    Name = category.Name,
                    SortCode = category.SortCode,
                    Profit = category.Profit,
                    Distribution = category.Distribution
                });
            else
                return Json(result.ToAjaxResult());
        }


        [HttpPost]
        [RoleLimit]
        [Description("管理-发布分类-更新")]
        public ActionResult Update(CategoryInputDto[] dtos)
        {
            dtos.CheckNotNull("dtos");
            OperationResult result = GoodsContract.EditCategorys(dtos);
            Category category = result.Data as Category;
            if (category != null)
                return Json(new CategoryTreeListNodeViewModel
                {
                    Id = category.Id,
                    ParentId = category.Parent != null ? category.Parent.Id.ToString() : null,
                    Name = category.Name,
                    SortCode = category.SortCode,
                    Profit = category.Profit,
                    Distribution = category.Distribution
                });
            else
                return Json(result.ToAjaxResult());
        }

        [HttpPost]
        [RoleLimit]
        [Description("管理-用户信息-删除")]
        public ActionResult Delete(int[] ids)
        {
            ids.CheckNotNull("ids");
            OperationResult result = GoodsContract.DeleteCategorys(ids);
            return Json(result.ToAjaxResult());
        }
    }
}
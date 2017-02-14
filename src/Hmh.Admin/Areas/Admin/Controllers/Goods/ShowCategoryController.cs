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
    public class ShowCategoryController : AdminBaseController
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
            Expression<Func<ShowCategory, bool>> predicate = FilterHelper.GetExpression<ShowCategory>(request.FilterGroup);
            var page = GoodsContract.ShowCategorys.ToPage(predicate,
                request.PageCondition,
                m => new ShowCategoryTreeListNodeViewModel()
                {
                    Id = m.Id,
                    ParentId = m.Parent!=null? m.Parent.Id.ToString() :null,
                    Name=m.Name,
                    SortCode=m.SortCode,
                    Link=m.Link,
                    Logo=m.Logo,
                    IsShow = m.IsShow
                });
            return Json(page.Data);
        }

        

        [HttpPost]
        [RoleLimit]
        [Description("管理-发布分类-新增")]
        public ActionResult Create(ShowCategoryInputDto[] dtos)
        {
            dtos.CheckNotNull("dtos");
            OperationResult result = GoodsContract.AddShowCategorys(dtos);
            ShowCategory showCategory = result.Data as ShowCategory;
            if (showCategory != null)
                return Json(new ShowCategoryTreeListNodeViewModel
                {
                    Id = showCategory.Id,
                    ParentId = showCategory.Parent != null ? showCategory.Parent.Id.ToString() : null,
                    Name = showCategory.Name,
                    SortCode = showCategory.SortCode,
                    Link = showCategory.Link,
                    Logo = showCategory.Logo,
                    IsShow = showCategory.IsShow
                });
            else
                return Json(result.ToAjaxResult());
        }


        [HttpPost]
        [RoleLimit]
        [Description("管理-发布分类-更新")]
        public ActionResult Update(ShowCategoryInputDto[] dtos)
        {
            dtos.CheckNotNull("dtos");
            OperationResult result = GoodsContract.EditShowCategorys(dtos);
            ShowCategory showCategory = result.Data as ShowCategory;
            if (showCategory != null)
                return Json(new ShowCategoryTreeListNodeViewModel
                {
                    Id = showCategory.Id,
                    ParentId = showCategory.Parent != null ? showCategory.Parent.Id.ToString() : null,
                    Name = showCategory.Name,
                    SortCode = showCategory.SortCode,
                    Link = showCategory.Link,
                    Logo = showCategory.Logo,
                    IsShow=showCategory.IsShow
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
            OperationResult result = GoodsContract.DeleteShowCategorys(ids);
            return Json(result.ToAjaxResult());
        }
    }
}
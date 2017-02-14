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
using OSharp.Utility.Extensions;

namespace Hmh.Admin.Areas.Admin.Controllers
{
    [Description("管理-规格信息")]
    public class SpecController : AdminBaseController
    {
        public IGoodsContract GoodsContract { get; set; }

        [Description("管理-规格-读取")]
        public ActionResult Read(int categoryId)
        {
            var attrs = GoodsContract.GoodsSpecifications.Where(a => a.Category.Id == categoryId).Select(m=> new {
                Id = m.Id,
                CategoryId = m.Category.Id,               
                Name = m.Name,
                SelectableValues = m.SelectableValues
            });

            return Json(attrs.ToGridData());          
            
        }        



        [HttpPost]
        [RoleLimit]
        [Description("管理-规格-新增")]
        public ActionResult Create(GoodsSpecificationInputDto[] dtos)
        {
            dtos.CheckNotNull("dtos");
            OperationResult result = GoodsContract.AddGoodsSpecifications(dtos);
            return Json(result.ToAjaxResult());
        }

        

        [HttpPost]
        [RoleLimit]
        [Description("管理-规格-更新")]
        public ActionResult Update(GoodsSpecificationInputDto[] dtos)
        {
            dtos.CheckNotNull("dtos");
            OperationResult result = GoodsContract.EditGoodsSpecifications(dtos);
            return Json(result.ToAjaxResult());
        }

        [HttpPost]
        [RoleLimit]
        [Description("管理-规格-删除")]
        public ActionResult Delete(int[] ids)
        {
            ids.CheckNotNull("ids");
            OperationResult result = GoodsContract.DeleteGoodsSpecifications(ids);
            return Json(result.ToAjaxResult());
        }
    }
}
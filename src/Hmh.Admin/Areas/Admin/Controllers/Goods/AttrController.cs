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
    [Description("管理-属性信息")]
    public class AttrController : AdminBaseController
    {
        public IGoodsContract GoodsContract { get; set; }

        [Description("管理-属性-读取")]
        public ActionResult Read(int categoryId)
        {
            var attrs = GoodsContract.Attrs.Where(a => a.Category.Id == categoryId).Select(m=> new {
                Id = m.Id,
                CategoryId = m.Category.Id,
                IsMust = m.IsMust,
                Name = m.Name,
                SelectableValues = m.SelectableValues,
                Type = m.Type

            });

            return Json(attrs.ToGridData());          
            
        }        



        [HttpPost]
        [RoleLimit]
        [Description("管理-属性-新增")]
        public ActionResult Create(AttrInputDto[] dtos)
        {
            dtos.CheckNotNull("dtos");
            OperationResult result = GoodsContract.AddAttrs(dtos);
            return Json(result.ToAjaxResult());
        }

        

        [HttpPost]
        [RoleLimit]
        [Description("管理-属性-更新")]
        public ActionResult Update(AttrInputDto[] dtos)
        {
            dtos.CheckNotNull("dtos");
            OperationResult result = GoodsContract.EditAttrs(dtos);
            return Json(result.ToAjaxResult());
        }

        [HttpPost]
        [RoleLimit]
        [Description("管理-属性-删除")]
        public ActionResult Delete(int[] ids)
        {
            ids.CheckNotNull("ids");
            OperationResult result = GoodsContract.DeleteAttrs(ids);
            return Json(result.ToAjaxResult());
        }
    }
}
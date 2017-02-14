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
using Hmh.Core.Shop;
using Hmh.Core.Shop.Dtos;
using Hmh.Core.Shop.Models;
using OSharp.Utility;
using OSharp.Utility.Data;
using OSharp.Utility.Filter;
using OSharp.Web.Mvc;
using OSharp.Web.Mvc.Extensions;
using OSharp.Web.Mvc.UI;
using Hmh.Admin.ViewModel;

namespace Hmh.Admin.Areas.Admin.Controllers
{
    [Description("管理-合同信息")]
    public class ContractsController : AdminBaseController
    {
        public IShopContract ShopContract { get; set; }

        [Description("管理-合同-读取")]
        public ActionResult Read(int shopId)
        {
            var attrs = ShopContract.Contracts.Where(a => a.Shop.Id == shopId).Select(m => new {
                m.Id,
                ShopId = m.Shop.Id,
                m.InitalFee,
                m.Number,
                m.HCoinLimit,
                PayState= m.ContractPay.PayState,
                m.IsLocked,
                m.BeginTime,
                m.EndTime,
                m.CreatedTime,
                m.Fee,
                m.Year,                
                m.State
            });

            return Json(attrs.ToGridData());

        }       
        
    }
}
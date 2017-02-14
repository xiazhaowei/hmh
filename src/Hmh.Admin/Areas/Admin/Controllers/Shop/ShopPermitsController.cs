// -----------------------------------------------------------------------
//  <copyright file="UsersController.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2015 OSharp. All rights reserved.
//  </copyright>
//  <site>http://www.osharp.org</site>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2015-12-04 17:08</last-date>
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


namespace Hmh.Admin.Areas.Admin.Controllers
{
    [Description("管理-用户信息")]
    public class ShopPermitsController : AdminBaseController
    {
        public IShopContract ShopContract { get; set; }

        [Description("管理-信息-读取")]
        public ActionResult Read()
        {
            GridRequest request = new GridRequest(Request);
            request.FilterGroup.Rules.Add(new FilterRule("State", ShopPermitState.Verifying, FilterOperate.Equal));

            Expression<Func<ShopPermit, bool>> predicate = FilterHelper.GetExpression<ShopPermit>(request.FilterGroup);
            var page = ShopContract.ShopPermits.ToPage(predicate,
                request.PageCondition,
                m => new
                {
                    m.Id,
                    ShopName=m.Shop.Name,
                    ShopId = m.Shop.Id,
                    m.UserName,
                    m.UserCardNum,
                    m.BusinessLicenseNum,                    
                    m.CreatedTime,
                    m.UserCardFront, 
                    m.UserCardReverse,
                    m.AuthLicensePic,
                    m.BusinessLicensePic,
                    m.State
                });
            return Json(page.ToGridData());
        }        
        

        
    }
}
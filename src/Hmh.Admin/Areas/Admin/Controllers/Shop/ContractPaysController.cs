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
    [Description("管理-合同付款信息")]
    public class ContractPaysController : AdminBaseController
    {
        public IShopContract ShopContract { get; set; }

        
        

        [Description("管理-合同付款信息-读取")]
        public ActionResult Read()
        {
            GridRequest request = new GridRequest(Request);
            //只显示未审核的资金付款
            request.FilterGroup.Rules.Add(new FilterRule("PayState", PayState.Verifying, FilterOperate.Equal));

            Expression<Func<ContractPay, bool>> predicate = FilterHelper.GetExpression<ContractPay>(request.FilterGroup);
            var page = ShopContract.ContractPays.ToPage(predicate,
                request.PageCondition,
                m => new
                {
                    m.Id,
                    ShopName= m.Contract.Shop.Name,
                    m.Money,
                    m.PayType,
                    m.PayStreamId,                    
                    ContractId=m.Contract.Id,
                    ContractFee=m.Contract.Fee,
                    m.PayState,
                    m.CreatedTime
                });
            return Json(page.ToGridData());
        }

        [HttpPost]
        [RoleLimit]
        [Description("管理-更新合同付款状态")]
        public ActionResult Update(ContractPayInputDto[] Dtos)
        {
            Dtos.CheckNotNull(nameof(Dtos));
            OperationResult result = ShopContract.EditContractPays(Dtos);
            return Json(result.ToAjaxResult());
        }

    }
}
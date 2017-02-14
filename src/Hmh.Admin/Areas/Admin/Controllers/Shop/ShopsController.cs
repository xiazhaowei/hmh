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
    public class ShopsController : AdminBaseController
    {
        public IShopContract ShopContract { get; set; }

        [Description("管理-信息-读取")]
        public ActionResult Read()
        {
            GridRequest request = new GridRequest(Request);
            Expression<Func<Shop, bool>> predicate = FilterHelper.GetExpression<Shop>(request.FilterGroup);
            var page = ShopContract.Shops.ToPage(predicate,
                request.PageCondition,
                m => new
                {
                    m.Id,
                    m.Name,
                    UserId = m.User.Id,
                    UserName =m.User.UserName,
                    m.LinkMan,
                    m.LinkManPhone,
                    m.CreatedTime,                    
                    m.AddrDetail,
                    m.HCoinLimit, 
                    m.BusinessState,
                    m.State
                });
            return Json(page.ToGridData());
        }        
        

        [HttpPost]
        [RoleLimit]
        [Description("管理-信息-更新")]
        public ActionResult Update(ShopInputDto[] dtos)
        {
            dtos.CheckNotNull("dtos");
            OperationResult result = ShopContract.EditShops(dtos);
            return Json(result.ToAjaxResult());
        }

        [Description("读取-店铺认证资料")]
        public ActionResult ReadShopPermit(int shopId)
        {
            ShopPermit shopPermit = ShopContract.ShopPermits.SingleOrDefault(sp=>sp.Shop.Id==shopId);
            return Json(shopPermit, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [RoleLimit]
        [Description("提交-店铺认证信息修改")]
        public ActionResult PostShopPermit(ShopPermitInputDto Dto)
        {
            //Dto.CheckNotNull(nameof(Dto));
            OperationResult result = ShopContract.EditShopPermits(Dto);
            return Json(result.ToAjaxResult());
        }

        [HttpPost]
        [RoleLimit]
        [Description("管理-信息-删除")]
        public ActionResult Delete(int[] ids)
        {
            ids.CheckNotNull("ids");
            OperationResult result = ShopContract.DeleteShops(ids);
            return Json(result.ToAjaxResult());
        }
    }
}
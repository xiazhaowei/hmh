using OSharp.Utility.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OSharp.Web.Mvc.UI;
using Hmh.Core.Order.Dtos;
using OSharp.Utility;
using System.Threading.Tasks;
using OSharp.Utility.Extensions;
using Hmh.Web.ViewModels;

namespace Hmh.Web.Controllers
{
    public class CartController : CommonController
    {
        #region 购物车ajax
        /// <summary>
        /// 获取当前用户购物车中的商品-用在购物车下拉菜单
        /// </summary>
        /// <returns></returns>
        public ActionResult GetCartGoods()
        {
            if (CurrentUser == null)
                return Content("");
            var cartGoods = CurrentUser.CartGoodses.Select(cg=>new {
                cg.Id,
                cg.Name,
                cg.Pic,
                cg.Price,
                cg.SkuInfo,
                cg.BuyCount,
                cg.Amount,
                SkuId=cg.Sku.Id,
                GoodsId=cg.Goods.Id
            });
            return Json(cartGoods);
        }

        /// <summary>
        /// 获取购物车中的商品 通过店名分组
        /// </summary>
        /// <returns></returns>
        public ActionResult GetCartGoodsGrouped()
        {
            var carGoods2 = from cg in CurrentUser.CartGoodses
                            group cg by cg.Goods.Shop.Name into g
                            select new {
                                g.Key,  
                                //HCoinLimit=g.Select(good=>good.Goods.Shop.HCoinLimit),                                                              
                                Goodses= g.Select(good=>new {
                                    good.Id,
                                    good.Name,
                                    GoodsId=good.Goods.Id,
                                    good.Pic,
                                    good.BuyCount,
                                    good.Price,                                    
                                    good.SkuInfo,
                                    Stock=good.Sku==null? 0:good.Sku.Stock
                                })
                            };           
            return Json(carGoods2);
        }

        /// <summary>
        /// 添加到购物车
        /// </summary>
        /// <returns></returns>
        public async Task<ActionResult> AddCartGoods(CartGoodsInputDto dto)
        {
            //只要提交过来的都是经过前端验证的用户已经登录
            dto.CheckNotNull(nameof(dto));

            dto.UserId = CurrentUser.Id;
            OperationResult result;
            //判断购物车中是否相同商品，相同商品只改变数量即可
            result = await OrderContract.AddCartGoodses(dto);
            return Json(result.ToAjaxResult());
            
        }

        /// <summary>
        /// 删除购物车商品
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult DelCartGoods(int? id)
        {
            OperationResult result = OperationResult.NoChanged;
            if (id.HasValue && id.Value > 0)
            {
                result = OrderContract.DeleteCartGoodses(id.Value);
            }
            return Json(result.ToAjaxResult());
        }

        /// <summary>
        /// 提交订单
        /// </summary>
        /// <returns></returns>
        public async Task<ActionResult> CreateOrder(OrderViewModel orderInfo)
        {
            //做店铺分单处理 就是创建不同的订单dto
            List<OrderInputDto> dtos=new List<OrderInputDto>();

            foreach(OrderShop shop in orderInfo.Shops)
            {
                //获取店铺
                Core.Shop.Models.Shop sp = ShopContract.Shops.SingleOrDefault(s => s.Name == shop.Name);
                if(sp==null)
                {
                    throw new Exception("没有改店铺");
                }
                //订单Dto
                OrderInputDto dto = new OrderInputDto {
                    UserId=CurrentUser.Id,
                    ShopId=sp.Id,
                    OrderNumber= DateTime.Now.ToString("yyMMddHHmmss")+new Random().GetRandomNumberString(4) ,
                    ShopName=sp.Name,
                    Remark=shop.Remark,
                    Amount=shop.Amount,
                    RealAmount=shop.RealAmount,
                    HPayAmount=shop.HPayAmount,
                    ExpressFee=shop.ExpressFee,
                    State=Core.Order.Models.OrderState.UnPay
                };

                //设置订单的收货信息
                dto.OrderExpress = new OrderExpressInputDto {
                    Name=orderInfo.OrderExpress.Name,
                    Region=orderInfo.OrderExpress.Region,
                    DetailAddress=orderInfo.OrderExpress.DetailAddress,
                    Zip=orderInfo.OrderExpress.Zip,
                    Mobile=orderInfo.OrderExpress.Mobile
                };

                //遍历商品
                foreach(OrderGoods goods in shop.Items)
                {                    
                    //订单商品
                    OrderGoodsInputDto orderGoodsInputDto = new OrderGoodsInputDto {
                        Name=goods.Name,
                        GoodsId=goods.GoodsId,
                        SkuInfo=goods.SkuInfo,
                        Pic=goods.Pic,
                        BuyCount=goods.BuyCount,
                        Price=goods.Price,
                        Amount=goods.Price * goods.BuyCount,
                        ExpressFee=0,
                        ServiceState=Core.Order.Models.GoodsServiceState.Normal
                    };
                    dto.OrderGoodses.Add(orderGoodsInputDto);
                }
                dtos.Add(dto);
            }

            OperationResult result = await OrderContract.AddOrders(dtos.ToArray());
            return Json(result.ToAjaxResult());
        }

        #endregion


        //购物车视图  
        public ActionResult Index()
        {
            return View();
        }        


        /// <summary>
        /// 确认购物车视图
        /// </summary>
        /// <returns></returns>
        public ActionResult ConfirmOrder()
        {
            //接收提交过来的数据 然后传到前端
            string orderCart = Request.Params["orderCart"].CastTo("");
            string referer = Request.Params["referer"].CastTo("cart");
            string sfrom = Request.Params["sfrom"].CastTo(new Random().GetRandomNumberString(6));

            ViewBag.orderCart = orderCart;
            ViewBag.referer = referer;
            ViewBag.sfrom = sfrom;

            return View();
        }

        /// <summary>
        /// 订单付款 视图
        /// </summary>
        /// <returns></returns>
        public ActionResult PayOrder()
        {
            return View();
        }
	}
}
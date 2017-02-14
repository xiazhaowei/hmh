using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hmh.Web.ViewModels
{
    /// <summary>
    /// 前端提交过来的数据包装
    /// </summary>
    public class OrderViewModel
    {
        public OrderExpress OrderExpress { get; set; }

        public ICollection<OrderShop> Shops { get; set; }

        public OrderViewModel()
        {
            Shops = new List<OrderShop>();
        }

    }
    /// <summary>
    /// 店铺包装
    /// </summary>
    public class OrderShop
    {
        public string Name { get; set; }

        public string Remark { get; set; }

        public decimal Amount { get; set; }

        public decimal RealAmount { get; set; }

        public decimal HPayAmount { get; set; }

        public decimal Preferential { get; set; }


        public decimal ExpressFee { get; set; }

        public ICollection<OrderGoods> Items { get; set; }

        public OrderShop()
        {
            Items = new List<OrderGoods>();
        }
    }
    /// <summary>
    /// 店铺的商品
    /// </summary>
    public class OrderGoods
    {
        public string Name { get; set; }

        public int GoodsId { get; set; }

        public string Pic { get; set; }

        public int BuyCount { get; set; }

        public decimal Price { get; set; }

        public string SkuInfo { get; set; }

        public int SkuId { get; set; }

    }

    /// <summary>
    /// 订单收件地址
    /// </summary>
    public class OrderExpress
    {
        public string Name { get; set; }

        public string Region { get; set; }

        public string Mobile { get; set; }

        public string DetailAddress { get; set; }

        public string Zip { get; set; }
       
    }
    
}
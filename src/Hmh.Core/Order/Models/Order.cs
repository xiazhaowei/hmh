using OSharp.Core.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hmh.Core.Order.Models
{
    [Description("订单-订单")]
    public class Order:EntityBase<int>, ICreatedTime
    {
        public Order()
        {
            OrderGoodses = new List<OrderGoods>();
        }
        /// <summary>
        /// 获取或设置 订单号 12位数字
        /// </summary>
        [Required][StringLength(30)]
        public string OrderNumber { get; set; }

        /// <summary>
        /// 获取或设置 店铺名称
        /// </summary>
        [Required][StringLength(50)]
        public string ShopName { get; set; }

        /// <summary>
        /// 获取或设置 商品金额
        /// </summary>
        [Required]
        public decimal Amount { get; set; }

        /// <summary>
        /// 获取或设置 H币付款金额
        /// </summary>
        public decimal HPayAmount { get; set; }

        /// <summary>
        /// 获取或设置 实付款
        /// </summary>
        [Required]
        public decimal RealAmount { get; set; }

        /// <summary>
        /// 获取设置 优惠金额
        /// </summary>
        public decimal Preferential { get; set; }

        /// <summary>
        /// 获取或设置 运费总额
        /// </summary>
        [Required]
        public decimal ExpressFee { get; set; }

        /// <summary>
        /// 获取或设置 用户
        /// </summary>
        public virtual Identity.Models.User User { get; set; }

        /// <summary>
        /// 获取或设置 店铺
        /// </summary>
        public virtual Shop.Models.Shop Shop { get; set; }

        /// <summary>
        /// 获取或设置 订单快递信息
        /// </summary>
        public virtual OrderExpress OrderExpress { get; set; }

        /// <summary>
        /// 获取或设置 订单的商品列表
        /// </summary>
        public virtual ICollection<OrderGoods> OrderGoodses { get; set; }

        /// <summary>
        /// 获取或设置 买家留言
        /// </summary>
        [StringLength(50)]
        public string Remark { get; set; }

        /// <summary>
        /// 获取或设置 订单状态
        /// </summary>
        [Required]
        public OrderState State { get; set; }

        #region Implementration of ICreateTime

        /// <summary>
        /// 获取或设置 信息创建时间
        /// </summary>
        public DateTime CreatedTime { get; set; }
        #endregion


    }

    /// <summary>
    /// 订单状态
    /// </summary>
    [Description("枚举-订单状态")]
    public enum OrderState
    {
        [Description("待付款")]
        UnPay,

        [Description("等待卖家发货")]
        Payed,

        [Description("卖家已发货")]
        Expressing,

        [Description("交易成功")]
        Success,

        [Description("交易关闭")]
        Closed        

    }

    
}

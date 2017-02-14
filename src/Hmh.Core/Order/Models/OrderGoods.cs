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
    [Description("订单-订单商品")]
    public class OrderGoods:EntityBase<int>,ICreatedTime
    {
        /// <summary>
        /// 获取或设置 商品名称
        /// </summary>
        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        /// <summary>
        /// 获取或设置 商品图片
        /// </summary>
        [Required]
        [StringLength(50)]
        public string Pic { get; set; }

        /// <summary>
        /// 获取或设置 购买数量
        /// </summary>
        [Required]
        public int BuyCount { get; set; }

        /// <summary>
        /// 获取或设置 所选规格价格
        /// </summary>
        [Required]
        public decimal Price { get; set; }

        /// <summary>
        /// 获取或设置 商品小计
        /// </summary>
        public decimal Amount { get; set; }

        /// <summary>
        /// 获取或设置 运费总额
        /// </summary>
        [Required]
        public decimal ExpressFee { get; set; }

        /// <summary>
        /// 获取或设置 SKU描述信息
        /// </summary>
        public string SkuInfo { get; set; } 

        /// <summary>
        /// 获取或设置 商品
        /// </summary>
        public virtual Goods.Models.Goods Goods { get; set; }

        /// <summary>
        /// 获取或设置 订单
        /// </summary>
        public virtual Order Order { get; set; }

        #region Implementration of ICreateTime

        /// <summary>
        /// 获取或设置 信息创建时间
        /// </summary>
        public DateTime CreatedTime { get; set; }
        #endregion

        /// <summary>
        /// 获取或设置 售后状态（退货退款）
        /// </summary>
        [Required]
        public GoodsServiceState ServiceState { get; set; }
    }

    /// <summary>
    /// 售后状态
    /// </summary>
    [Description("商品售后状态")]
    public enum GoodsServiceState
    {
        [Description("正常")]
        Normal,

        [Description("退款处理中")]
        DrawBack,

        [Description("退货处理中")]
        GoodsBack
    }
}

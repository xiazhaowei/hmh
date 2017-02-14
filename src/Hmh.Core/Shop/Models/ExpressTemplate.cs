using OSharp.Core.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hmh.Core.Shop.Models
{
    /// <summary>
    /// 实体类--运费模板
    /// </summary>
    [Description("店铺运费模板")]
    public class ExpressTemplate:EntityBase<int>
    {
        public ExpressTemplate()
        {
            SpecialExpressAddresses = new List<SpecialExpressAddress>();
        }
        /// <summary>
        /// 获取或设置 所属店铺
        /// </summary>
        public virtual Shop Shop { get; set; }

        /// <summary>
        /// 获取或设置 模板名称
        /// </summary>
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// 获取或设置 发货地址
        /// </summary>
        [Required]
        public string DeliverAddress { get; set; }
        /// <summary>
        /// 获取或设置 发货时间
        /// </summary>
        [Required]
        public string DeliverTime { get; set; }

        /// <summary>
        /// 获取或设置 是否包邮
        /// </summary>
        [Required]
        public bool IsFree { get; set; }
        /// <summary>
        /// 获取或设置 首件内
        /// </summary>
        [Required]
        public int Count { get; set; }
        /// <summary>
        /// 获取或设置 运费
        /// </summary>
        [Required]
        public decimal Price { get; set; }
        /// <summary>
        /// 获取或设置 每增加件数
        /// </summary>
        [Required]
        public int CountAdd { get; set; }
        /// <summary>
        /// 获取或设置 增加金额
        /// </summary>
        [Required]
        public decimal PriceAdd { get; set; }

        /// <summary>
        /// 获取或设置 特殊地区
        /// </summary>
        public virtual ICollection<SpecialExpressAddress> SpecialExpressAddresses { get; set; }
    }

    /// <summary>
    /// 实体类-- 运费模板特殊情况
    /// </summary>
    [Description("店铺运费模板特殊区域设置")]
    public class SpecialExpressAddress:EntityBase<int>
    {
        public virtual ExpressTemplate ExpressTemplate { get; set; }
        /// <summary>
        /// 获取或设置 特殊城市
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// 获取或设置 首件内
        /// </summary>
        [Required]
        public int Count { get; set; }

        /// <summary>
        /// 获取或设置 运费
        /// </summary>
        [Required]
        public decimal Price { get; set; }
        /// <summary>
        /// 获取或设置 每增加件数
        /// </summary>
        [Required]
        public int CountAdd { get; set; }
        /// <summary>
        /// 获取或设置 增加金额
        /// </summary>
        [Required]
        public decimal PriceAdd { get; set; }
    }
}

using Hmh.Core.Order.Models;
using OSharp.Core.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hmh.Core.Order.Dtos
{
    public class OrderGoodsInputDto:IInputDto<int>
    {
        /// <summary>
        /// 获取或设置 主键，唯一标识
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 获取设置 商品ID
        /// </summary>
        public int? GoodsId { get; set; }   
       

        /// <summary>
        /// 获取设置 商品名称
        /// </summary>
        [Required]
        [StringLength(200)]
        public string Name { get; set; }

        /// <summary>
        /// 获取或设置 产品图片
        /// </summary>
        [Required]
        [StringLength(200)]
        public string Pic { get; set; }

        /// <summary>
        /// 获取或设置 Sku描述信息
        /// </summary>
        [StringLength(200)]
        public string SkuInfo { get; set; }

        /// <summary>
        /// 获取或设置 购买数量
        /// </summary>
        [Required]
        public int BuyCount { get; set; }

        /// <summary>
        /// 获取或设置 商品价格
        /// </summary>
        [Required]
        public decimal Price { get; set; }

        /// <summary>
        /// 获取或设置 价格小计
        /// </summary>
        [Required]
        public decimal Amount { get; set; }

        /// <summary>
        /// 获取或设置 邮费
        /// </summary>
        public decimal ExpressFee { get; set; }

        /// <summary>
        /// 获取或设置 售后状态（退货退款）
        /// </summary>        
        public GoodsServiceState ServiceState { get; set; }
    }
}

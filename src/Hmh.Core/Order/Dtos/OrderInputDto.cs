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
    public class OrderInputDto:IInputDto<int>
    {
        public OrderInputDto()
        {
            OrderGoodses = new List<OrderGoodsInputDto>();
        }
        /// <summary>
        /// 获取或设置 主键，唯一标识
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 获取或设置 订单号
        /// </summary>
        [Required][StringLength(30)]
        public string OrderNumber { get; set; }

        /// <summary>
        /// 获取设置 商品ID
        /// </summary>
        public int? ShopId { get; set; }               


        /// <summary>
        /// 获取或设置 用户Id
        /// </summary>
        public int? UserId { get; set; }

        /// <summary>
        /// 获取设置 商品名称
        /// </summary>
        [Required]
        [StringLength(200)]
        public string ShopName { get; set; }

        /// <summary>
        /// 获取或设置 备注
        /// </summary>
        [Required]
        [StringLength(200)]
        public string Remark { get; set; }

        
        /// <summary>
        /// 获取或设置 订单总价
        /// </summary>
        [Required]
        public decimal Amount { get; set; }

        /// <summary>
        /// 获取或设置 实际支付RMB数量
        /// </summary>
        [Required]
        public decimal RealAmount { get; set; }

        /// <summary>
        /// 获取或设置 H币付款金额
        /// </summary>
        [Required]
        public decimal HPayAmount { get; set; }

        
        /// <summary>
        /// 获取或设置 优惠金额
        /// </summary>
        [Required]
        public decimal Preferential { get; set; }

        /// <summary>
        /// 获取或设置 邮费
        /// </summary>
        public decimal ExpressFee { get; set; }


        /// <summary>
        /// 获取或设置 订单的快递信息
        /// </summary>
        public OrderExpressInputDto OrderExpress { get; set; } 


        /// <summary>
        /// 获取或设置 订单状态
        /// </summary>        
        public OrderState State { get; set; }

        /// <summary>
        /// 获取或设置 订单商品
        /// </summary>
        public ICollection<OrderGoodsInputDto> OrderGoodses { get; set; }

    }

    public class OrderExpressInputDto : IInputDto<int>
    {
        /// <summary>
        /// 获取或设置 Id
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 获取或设置 收件人姓名
        /// </summary>
        [StringLength(200)]
        public string Name { get; set; }

        /// <summary>
        /// 获取或设置 收件人地区
        /// </summary>
        [StringLength(200)]
        public string Region { get; set; }


        /// <summary>
        /// 获取或设置 详细地址
        /// </summary>
        [StringLength(200)]
        public string DetailAddress { get; set; }

        /// <summary>
        /// 获取或设置 邮编
        /// </summary>
        [StringLength(50)]
        public string Zip { get; set; }

        /// <summary>
        /// 获取或设置 收件人电话
        /// </summary>
        [StringLength(50)]
        public string Mobile { get; set; }

        /// <summary>
        /// 获取或设置 快递名称
        /// </summary>
        [StringLength(50)]
        public string ExpressName { get; set; }

        /// <summary>
        /// 获取或设置 快递单号
        /// </summary>
        [StringLength(50)]
        public string ExpressNumber { get; set; }

    }
}

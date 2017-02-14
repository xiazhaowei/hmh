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
    [Description("订单-快递信息")]
    public class OrderExpress:EntityBase<int>
    {
        /// <summary>
        /// 获取或设置 订单
        /// </summary>
        public virtual Order Order { get; set; }

        /// <summary>
        /// 获取或设置 收件人姓名
        /// </summary>
        [Required][StringLength(20)]
        public string Name { get; set; }

        /// <summary>
        /// 获取或设置 地区信息
        /// </summary>
        [Required][StringLength(20)]
        public string Region { get; set; }

        /// <summary>
        /// 获取或设置 详细地址
        /// </summary>
        [Required][StringLength(50)]
        public string DetailAddress { get; set; }

        /// <summary>
        /// 获取或设置 邮编
        /// </summary>
        [StringLength(20)]
        public string Zip { get; set; }

        /// <summary>
        /// 获取或设置 收件人电话
        /// </summary>
        [Required][StringLength(20)]
        public string Mobile { get; set; }

        /// <summary>
        /// 获取或设置 快递公司
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

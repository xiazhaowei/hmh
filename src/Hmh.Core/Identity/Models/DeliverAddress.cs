using Hmh.Core.Shop.Models;
using OSharp.Core.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hmh.Core.Identity.Models
{
    [Description("认证-收件地址管理")]
    public class DeliverAddress:EntityBase<int>
    {
        /// <summary>
        /// 获取设置 用户信息
        /// </summary>
        public virtual User User { get; set; }

        /// <summary>
        /// 收件人
        /// </summary>
        [Required][StringLength(20)]
        public string Name { get; set; }

        /// <summary>
        /// 获取设置 收件区域
        /// </summary>
        [Required][StringLength(50)]
        public string Region { get; set; }

        /// <summary>
        /// 详细收件地址
        /// </summary>
        [Required][StringLength(200)]
        public string DetailAddress { get; set; }

        /// <summary>
        /// 获取设置 收件人手机
        /// </summary>
        [Required][StringLength(20)]
        public string Mobile { get; set; }

        /// <summary>
        /// 获取设置 邮编
        /// </summary>
        [Required][StringLength(20)]
        public string Zip { get; set; }

        /// <summary>
        /// 获取设置 是否默认
        /// </summary>
        public bool IsDefault { get; set; }
    }
}

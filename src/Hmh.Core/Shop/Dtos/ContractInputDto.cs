using Hmh.Core.Shop.Models;
using OSharp.Core.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hmh.Core.Shop.Dtos
{
    public class ContractInputDto : IInputDto<int>
    {
        /// <summary>
        /// 获取或设置 主键，唯一标识
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 获取或设置 店铺编号
        /// </summary>        
        public int? ShopId { get; set; }

        /// <summary>
        /// 获取或设置 合同编号
        /// </summary>
        [Required]
        public string Number { get; set; }
        /// <summary>
        /// 获取设置 合同年限
        /// </summary>
        public int Year { get; set; }

        /// <summary>
        /// 获取设置 加盟费
        /// </summary>
        [Required]
        public decimal InitalFee { get; set; }

        /// <summary>
        /// 获取设置  H币消费限制
        /// </summary>
        [Required]
        public decimal HCoinLimit { get; set; }

        /// <summary>
        /// 获取设置 合同金额也就是保本金额
        /// </summary>
        public decimal Fee { get; set; }

        /// <summary>
        /// 获取或设置 生效时间
        /// </summary>
        public DateTime BeginTime { get; set; }

        /// <summary>
        /// 获取或设置 过期时间
        /// </summary>
        public DateTime EndTime { get; set; }
        /// <summary>
        /// 获取设置合同状态
        /// </summary>
        public ContractState State { get; set; }        
        
    }
}

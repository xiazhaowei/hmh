using Hmh.Core.Shop.Models;
using OSharp.Core.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hmh.Core.Shop.Dtos
{
    public class ContractPayInputDto:IInputDto<int>
    {

        public int Id { get; set; }

        /// <summary>
        /// 获取或设置 合同ID
        /// </summary>        
        public int? ContractId { get; set; }

        /// <summary>
        /// 获取或设置 付款金额
        /// </summary>
        [Required]
        public decimal Money { get; set; }

        /// <summary>
        /// 获取或设置 付款方式 微信/支付宝/银行打款
        /// </summary>
        [Required]
        public PayType PayType { get; set; }

        /// <summary>
        /// 获取或设置 付款流水号
        /// </summary>        
        [Required]
        public string PayStreamId { get; set; }

        /// <summary>
        /// 获取或设置 付款状态
        /// </summary>
        [Required]
        public PayState PayState { get; set; }

    }

    
}

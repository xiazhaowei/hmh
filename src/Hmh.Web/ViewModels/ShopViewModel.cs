using Hmh.Core.Shop.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Hmh.Web.ViewModels
{
    /// <summary>
    /// 开店信息-视图模型
    /// </summary>
    public class OpenShopViewModel
    {
        /// <summary>
        /// 获取设置 店铺
        /// </summary>
        public Shop Shop { get; set; }
        /// <summary>
        /// 店铺未开通原因
        /// </summary>
        public List<string> ErrorList { get; set; }
    }

    /// <summary>
    /// 合同预览--视图模型
    /// </summary>
    public class ContractPreViewModel
    {
        public Shop Shop { get; set; }
        public ContractLevel ContractLevel { get; set; }
        public int Years { get; set; }
    }

    /// <summary>
    /// 签订合同视图模型
    /// </summary>
    public class SignContractViewModel
    {
        /// <summary>
        /// 获取或设置 签订年限
        /// </summary>
        [Required]
        public int Year { get; set; }
        /// <summary>
        /// 获取设置 合同级别
        /// </summary>
        [Required]
        public string ContractLevel { get; set; }

        
    }

    public class ContractPayViewModel
    {
        /// <summary>
        /// 获取或设置 付款流水号
        /// </summary>        
        [Required]
        [Description("银行打款流水号")]
        public string PayStreamId { get; set; }

        /// <summary>
        /// 获取或设置 付款金额
        /// </summary>
        [Required]
        [Description("打款金额")]
        public decimal Money { get; set; }
    }
}
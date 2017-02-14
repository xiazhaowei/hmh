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
    /// 实体类---合同付款信息
    /// </summary>
    [Description("店铺-合同付款信息")]
    public class ContractPay:EntityBase<int>,ICreatedTime
    {       

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
        public string PayStreamId { get; set; }

        /// <summary>
        /// 获取或设置 合同
        /// </summary>
        public virtual Contract Contract { get; set; }

        /// <summary>
        /// 获取设置 付款状态
        /// </summary>
        public PayState PayState { get; set; }

        #region Implementation of ICreatedTime

        /// <summary>
        /// 获取设置 信息创建时间
        /// </summary>
        public DateTime CreatedTime { get; set; }

        

        #endregion
    }

    /// <summary>
    /// 支付方式
    /// </summary>
    [Description("枚举-支付方式")]
    public enum PayType
    {
        /// <summary>
        /// 支付宝
        /// </summary>
        [Description("支付宝")]
        Alipay,
        /// <summary>
        /// 微信支付
        /// </summary>
        [Description("微信支付")]
        Wechart,
        /// <summary>
        /// 银行
        /// </summary>
        [Description("银行付款")]
        Bank
    }

    /// <summary>
    /// 合同付款状态
    /// </summary>
    public enum PayState
    {        
        /// <summary>
        /// 审核中
        /// </summary>
        [Description("审核中")]
        Verifying,

        /// <summary>
        /// 已付款
        /// </summary>
        [Description("已付款")]
        Payed,

        /// <summary>
        /// 审核失败
        /// </summary>
        [Description("审核失败")]
        VerifyFail
    }
}

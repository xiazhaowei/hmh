using Hmh.Core.Identity.Models;
using OSharp.Core.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hmh.Core.Goods.Models;
using System.ComponentModel;

namespace Hmh.Core.Identity.Models
{
    /// <summary>
    /// 实体类  现金交易记录
    /// </summary>
    [Description("交易-现金交易记录")]
    public class RmbCoinTransaction:EntityBase<int>,ICreatedTime
    {

        /// <summary>
        /// 获取设置 用户
        /// </summary>
        public virtual User User { get; set; }

        /// <summary>
        /// 获取设置 流水号
        /// </summary>
        [Required, StringLength(50)]
        public string StreamId { get; set; }

        /// <summary>
        /// 获取设置 对方信息
        /// </summary>
        [StringLength(50)]
        public string OtherSideInfo { get; set; }

        /// <summary>
        /// 获取设置 交易金额
        /// </summary>
        public decimal Amount { get; set; }             

        /// <summary>
        /// 获取设置 优惠金额
        /// </summary>
        public decimal Preferential { get; set; }

        /// <summary>
        /// 获取设置 实付金额
        /// </summary>
        public decimal RealAmount { get; set; }

        /// <summary>
        /// 获取设置 服务费
        /// </summary>
        public decimal Fee { get; set; }
        /// <summary>
        /// 获取设置 交易类型
        /// </summary>
        public RmbTransactionType Type { get; set; }

        /// <summary>
        /// 获取设置 收入支出
        /// </summary>
        public TransactionDirection Direction { get; set; }

        /// <summary>
        /// 获取设置 交易状态
        /// </summary>
        public TransactionState State { get; set; }

        /// <summary>
        /// 获取设置 备注
        /// </summary>
        [StringLength(50)]
        public string Remark { get; set; }        

        #region Implementation of ICreateTime
        /// <summary>
        /// 获取设置 信息创建时间
        /// </summary>
        public DateTime CreatedTime { get; set; }      

        #endregion
    }

    /// <summary>
    /// 枚举 店铺类型
    /// </summary>
    [Description("枚举-交易类型")]
    public enum RmbTransactionType
    {
        /// <summary>
        /// 充值
        /// </summary>
        [Description("充值")]
        Charge,

        /// <summary>
        /// 提现
        /// </summary>
        [Description("提现")]
        Withdraw,

        /// <summary>
        /// 购物
        /// </summary>
        [Description("购物")]
        Shopping,

        /// <summary>
        /// 转账
        /// </summary>
        [Description("转账")]
        Transaction,

        /// <summary>
        /// 三级分销
        /// </summary>
        [Description("三级分销")]
        ShopRebate,
        
        /// <summary>
        /// 八代分润
        /// </summary>
        [Description("八代分润")]
        UserRebate,

        /// <summary>
        /// 其他
        /// </summary>
        [Description("其他")]
        Other
    }


    [Description("枚举-交易状态")]
    public enum TransactionState
    {
        [Description("交易成功")]
        Success,

        [Description("待确认")]
        UnConfirmed
    }

    [Description("枚举-收入支出类型")]
    public enum TransactionDirection
    {
        /// <summary>
        /// 收入
        /// </summary>
        [Description("收入")]
        InCome,

        /// <summary>
        /// 支出
        /// </summary>
        [Description("支出")]
        Expend
    }
}

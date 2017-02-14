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
    /// 实体类---合同信息
    /// </summary>
    [Description("店铺-合同信息")]
    public class Contract : EntityBase<int>, IExpirable, ILockable, ICreatedTime
    {        

        /// <summary>
        /// 获取或设置 合同编号HMHxxxxxxxxx
        /// </summary>
        [Required]
        public string Number { get; set; }

        /// <summary>
        /// 获取设置 店铺Id
        /// </summary>
        //[Required]
        //public int ShopId { get; set; }
        /// <summary>
        /// 获取设置 合同金额也就是保本金额
        /// </summary>
        public decimal Fee { get; set; }

        /// <summary>
        /// 获取设置 合同年限
        /// </summary>
        public int Year { get; set; }

        /// <summary>
        /// 获取设置 合同每年加盟费
        /// </summary>
        public decimal InitalFee { get; set; }
        /// <summary>
        /// 获取设置 H币限制
        /// </summary>
        public decimal HCoinLimit { get; set; }
        /// <summary>
        /// 获取设置 所属店铺
        /// </summary>
        public virtual Shop Shop { get; set; }
        
        /// <summary>
        /// 获取设置 合同状态
        /// </summary>
        public ContractState State { get; set; }
        
        /// <summary>
        /// 获取或设置 付款记录
        /// </summary>
        public virtual ContractPay ContractPay { get; set; }

        #region Implementation of ICreatedTime

        /// <summary>
        /// 获取设置 信息创建时间
        /// </summary>
        public DateTime CreatedTime { get; set; }

        #endregion

        #region Implementation of ILockable
        /// <summary>
        /// 获取或设置 是否锁定，用于禁用当前信息
        /// </summary>
        public bool IsLocked { get; set; }
        #endregion

        #region Implementation of IExpirable
        /// <summary>
        /// 获取或设置 生效时间
        /// </summary>
        public DateTime? BeginTime { get; set; }

        /// <summary>
        /// 获取或设置 过期时间
        /// </summary>
        public DateTime? EndTime { get; set; }
        #endregion
    }  
  
    /// <summary>
    /// 枚举  合同状态
    /// </summary>
    [Description("枚举-合同状态")]
    public enum ContractState
    {   
        /// <summary>
        /// 合同未完成
        /// </summary>
        [Description("未生效")]
        UnAvliable,     
        /// <summary>
        /// 合同履行中
        /// </summary>
        [Description("合同履行中")]
        Using,

        /// <summary>
        /// 合同未生效
        /// </summary>
        [Description("等待中")]
        Waiting,

        /// <summary>
        /// 合同终止
        /// </summary>
        [Description("异常终止合同")]
        Stop,

        /// <summary>
        /// 合同清算中
        /// </summary>
        [Description("合同清算中")]
        Settlling,

        /// <summary>
        /// 合同到期
        /// </summary>
        [Description("合同结束")]
        Finish
    }

    
}

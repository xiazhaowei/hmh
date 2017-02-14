using OSharp.Core.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hmh.Core.Settings.Models
{
    /// <summary>
    /// 实体类三级分销级别设置
    /// </summary>
    [Description("系统-店铺分销级别")]
    public class ShopDistributionLevel : EntityBase<int>
    {
        /// <summary>
        /// 获取设置 级别名称
        /// </summary>
        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        /// <summary>
        /// 获取设置 提成奖金
        /// </summary>
        public decimal Money { get; set; }
        /// <summary>
        /// 获取设置 级别提成比例
        /// </summary>
        public decimal Persent { get; set; }

        /// <summary>
        /// 获取或设置 奖金方式
        /// </summary>
        public RewardType RewardType { get; set; }
    }

    [Description("奖金提成方式")]
    public enum RewardType
    {
        /// <summary>
        /// 固定奖金
        /// </summary>
        [Description("固定金额")]
        SolidMoney,

        /// <summary>
        /// 提成
        /// </summary>
        [Description("提点")]
        GetPersent
    }
}

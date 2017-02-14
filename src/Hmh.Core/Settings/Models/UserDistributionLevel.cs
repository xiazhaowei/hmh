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
    /// 实体类八代分润级别设置
    /// </summary>
    [Description("系统设置-用户分销级别")]
    public class UserDistributionLevel : EntityBase<int>
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
}

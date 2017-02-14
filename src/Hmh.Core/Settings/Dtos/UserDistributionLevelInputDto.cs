using Hmh.Core.Settings.Models;
using OSharp.Core.Data;
using System.ComponentModel.DataAnnotations;


namespace Hmh.Core.Settings.Dtos
{
    public class UserDistributionLevelInputDto : IInputDto<int>
    {
        /// <summary>
        /// 获取或设置 主键，唯一标识
        /// </summary>
        public int Id { get; set; }

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
        public string Persent { get; set; }

        /// <summary>
        /// 获取或设置 奖金方式
        /// </summary>
        public RewardType RewardType { get; set; }
    }
}

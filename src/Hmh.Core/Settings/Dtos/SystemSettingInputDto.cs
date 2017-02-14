using OSharp.Core.Data;
using System.ComponentModel.DataAnnotations;

namespace Hmh.Core.Settings.Dtos
{
    public class SystemSettingInputDto:IInputDto<int>
    {
        /// <summary>
        /// 获取或设置 主键，唯一标识
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 获取设置 设置说明
        /// </summary>
        [Required]
        [StringLength(100)]
        public string Description { get; set; }

        /// <summary>
        /// 获取设置 设置名称
        /// </summary>
        [Required]
        [StringLength(50)]
        public string Key { get; set; }

        
        [StringLength(50)]
        public string Value { get; set; }
    }
}

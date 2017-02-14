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
    /// 实体类-系统参数
    /// </summary>
    [Description("系统-系统设置信息")]
    public class SystemSetting:EntityBase<int>
    {
        /// <summary>
        /// 获取设置 设置说明
        /// </summary>
        [Required][StringLength(100)]
        public string Description { get; set; }

        /// <summary>
        /// 获取设置 设置名称
        /// </summary>
        [Required][StringLength(50)]
        public string Key { get; set; }

        [StringLength(50)]
        public string Value { get; set; }
    }
}

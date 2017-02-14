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
    /// 实体类   区域信息
    /// </summary>
    [Description("店铺-区域信息")]
    public class Region : EntityBase<int>
    {        
        /// <summary>
        /// 获取设置 省份
        /// </summary>
        [StringLength(50)]
        public string Province { get; set; }
        /// <summary>
        /// 获取设置 市
        /// </summary>
        [StringLength(50)]
        public string City { get; set; }
        /// <summary>
        /// 获取设置  县区
        /// </summary>
        [StringLength(50)]
        public string County { get; set; }

        /// <summary>
        /// 获取设置 街道或商圈
        /// </summary>
        [StringLength(50)]
        public string Street { get; set; }

        /// <summary>
        /// 获取或设置 是否开通业务
        /// </summary>
        [Required]
        public bool IsOpenServices { get; set; }
    }
}

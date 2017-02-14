using OSharp.Core.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hmh.Core.Shop.Dtos
{
    public class RegionInputDto: IInputDto<int>
    {
        /// <summary>
        /// 获取或设置 主键，唯一标识
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 获取设置 省份
        /// </summary>
        [Required]
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

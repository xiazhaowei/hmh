using Hmh.Core.Goods.Models;
using OSharp.Core.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hmh.Core.Goods.Dtos
{
    public class GoodsColorSpecificationItemInputDto : IInputDto<int>
    {
        /// <summary>
        /// 获取或设置 主键，唯一标识
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 获取设置 颜色名称
        /// </summary>
        [Required]
        [StringLength(20)]
        public string Name { get; set; }

        /// <summary>
        /// 对应的颜色值#f00
        /// </summary>
        public string ColorValue { get; set; }

        /// <summary>
        /// 可选值 分组名称
        /// </summary>
        public string GroupName { get; set; }

        /// <summary>
        /// 获取或设置 分组颜色值
        /// </summary>
        public string GroupValue { get; set; }
    }
}

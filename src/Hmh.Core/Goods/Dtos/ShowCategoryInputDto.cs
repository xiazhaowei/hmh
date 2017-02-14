using OSharp.Core.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hmh.Core.Goods.Dtos
{
    public class ShowCategoryInputDto:IInputDto<int>
    {
        /// <summary>
        /// 获取或设置 主键，唯一标识
        /// </summary>
        public int Id { get; set; }


        public int? ParentId { get; set; }

        /// <summary>
        /// 获取设置 品牌名称
        /// </summary>
        [Required]
        [StringLength(20)]
        public string Name { get; set; }
        /// <summary>
        /// 获取设置 Logo
        /// </summary>
        [StringLength(100)]
        public string Logo { get; set; }

        /// <summary>
        /// 获取或设置 排序码
        /// </summary>
        [Range(0, 999)]
        public int SortCode { get; set; }

        /// <summary>
        /// 获取或设置 连接地址
        /// </summary>
        public string Link { get; set; }

        /// <summary>
        /// 获取或设置 是否展示
        /// </summary>
        public bool IsShow { get; set; }
    }
}

using OSharp.Core.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hmh.Core.Goods.Dtos
{
    public class CategoryInputDto:IInputDto<int>
    {
        /// <summary>
        /// 获取或设置 主键，唯一标识
        /// </summary>
        public int Id { get; set; }


        public int? ParentId { get; set; }

        /// <summary>
        /// 获取设置 类名
        /// </summary>
        [Required]
        [StringLength(20)]
        public string Name { get; set; }

        /// <summary>
        /// 获取或设置 排序码
        /// </summary>
        [Range(0, 999)]
        public int SortCode { get; set; }

        /// <summary>
        /// 获取设置 利润率
        /// </summary>
        [Required]
        [Range(0, 100)]
        public int Profit { get; set; }

        /// <summary>
        /// 获取设置 提成比例（用来客户分润）
        /// </summary>
        [Required]
        [Range(0, 100)]
        public int Distribution { get; set; }
    }
}

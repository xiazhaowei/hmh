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
    public class AttrInputDto:IInputDto<int>
    {
        /// <summary>
        /// 获取或设置 主键，唯一标识
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 获取设置 属性所属分类
        /// </summary>
        public int? CategoryId { get; set; }
        /// <summary>
        /// 获取设置 属性名称
        /// </summary>
        [Required]
        [StringLength(20)]
        public string Name { get; set; }

        /// <summary>
        /// 获取设置 属性类型
        /// </summary>
        public AttrType Type { get; set; }

        /// <summary>
        /// 获取设置 是否必填
        /// </summary>
        [Required]
        public bool IsMust { get; set; }

        /// <summary>
        /// 可选值
        /// </summary>
        public string SelectableValues { get; set; }
    }
}

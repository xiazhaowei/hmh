using OSharp.Core.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hmh.Core.Goods.Models
{
    /// <summary>
    /// 实体类 商品规格
    /// </summary>
    [Description("商品-规格信息")]
    public class GoodsSpecification:EntityBase<int>
    {
       
        /// <summary>
        /// 获取或设置 类别
        /// </summary>
        public Category Category { get; set; }
        /// <summary>
        /// 获取设置 规格名称
        /// </summary>
        [StringLength(20)]
        public string Name { get; set; }

        /// <summary>
        /// 获取或设置 可选值
        /// </summary>
        public string SelectableValues { get; set; } 
    }
}

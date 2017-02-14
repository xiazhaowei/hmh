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
    /// 实体类   属性
    /// </summary>
    [Description("商品-属性信息")]
    public class Attr:EntityBase<int>
    {      
        /// <summary>
        /// 获取设置 属性所属分类
        /// </summary>
        public virtual Category Category { get; set; }
        /// <summary>
        /// 获取设置 属性名称
        /// </summary>
        [Required][StringLength(20)]
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
        /// 排序编号
        /// </summary>
        [Required]
        public int SortCode { get; set; }
        /// <summary>
        /// 可选值
        /// </summary>
        public string SelectableValues { get; set; }
    }

    /// <summary>
    /// 枚举 属性类型
    /// </summary>
    [Description("枚举-属性类型")]
    public enum AttrType
    {
        /// <summary>
        /// 单选
        /// </summary>
        [Description("单选。")]
        SingleSelect,

        /// <summary>
        /// 多选
        /// </summary>
        [Description("多选。")]
        DoubleSelect,

        /// <summary>
        /// 输入
        /// </summary>
        [Description("输入。")]
        Input,

        /// <summary>
        /// 选输
        /// </summary>
        [Description("选输")]
        InputSelect
    }
}

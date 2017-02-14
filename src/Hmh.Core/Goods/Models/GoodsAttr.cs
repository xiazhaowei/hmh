using OSharp.Core.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hmh.Core.Goods.Models
{
    /// <summary>
    /// 实体类 商品属性值
    /// </summary>
    [Description("商品属性值")]
    public class GoodsAttr:EntityBase<int>
    {
        /// <summary>
        /// 获取或设置 所属商品
        /// </summary>
        public virtual Goods Goods { get; set; }

        /// <summary>
        /// 获取或设置 属性名称
        /// </summary>
        public string AttrName { get; set; }

        /// <summary>
        /// 获取或设置 属性值 多选项用，分割
        /// </summary>
        public string AttrValue { get; set; }
    }
}

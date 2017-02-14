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
    /// 实体类 类别
    /// </summary>
    [Description("商品-商品分类信息")]
    public class Category:EntityBase<int>
    { 
        public Category()
        {
            Children = new List<Category>();
            Attrs = new List<Attr>();
            GoodsSpecifications = new List<GoodsSpecification>();
        }
        /// <summary>
        /// 获取设置 上级
        /// </summary>
        public virtual Category Parent { get; set; }

        /// <summary>
        /// 获取设置 类名
        /// </summary>
        [Required][StringLength(20)]
        public string Name { get; set; }

        /// <summary>
        /// 获取或设置 排序码
        /// </summary>
        [Range(0, 999)]
        public int SortCode { get; set; }

        /// <summary>
        /// 获取设置 利润率
        /// </summary>
        [Required][Range(0, 100)]
        public int Profit { get; set; }

        /// <summary>
        /// 获取设置 提成比例
        /// </summary>
        [Required][Range(0,100)]
        public int Distribution { get; set; }

        /// <summary>
        /// 是否开启颜色规格
        /// </summary>
        public bool IsUseColor { get; set; }

        /// <summary>
        /// 获取设置 分类下的商品
        /// </summary>
        public virtual ICollection<Goods> Goodses { get; set; }

        /// <summary>
        /// 获取设置 分类的子类别
        /// </summary>
        public virtual ICollection<Category> Children { get; set; }

        /// <summary>
        /// 获取或设置 分类的属性集
        /// </summary>
        public virtual ICollection<Attr> Attrs { get; set; }

        /// <summary>
        /// 获取或设置 分类的规格
        /// </summary>
        public virtual ICollection<GoodsSpecification> GoodsSpecifications { get; set; }
    }
}

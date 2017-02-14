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
    /// 实体类  品牌广告
    /// </summary>
    [Description("商品-展示分类")]
    public class ShowCategory:EntityBase<int>
    {   
        public ShowCategory()
        {
            Children = new List<ShowCategory>();
        }
        /// <summary>
        /// 获取或设置 父类别
        /// </summary>
        public ShowCategory Parent { get; set; }    
        /// <summary>
        /// 获取设置 品牌名称
        /// </summary>
        [Required][StringLength(20)]
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

        /// <summary>
        /// 获取或设置 子类别
        /// </summary>
        public virtual ICollection<ShowCategory> Children { get; set; }
    }
}

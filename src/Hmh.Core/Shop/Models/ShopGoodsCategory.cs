using OSharp.Core.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hmh.Core.Shop.Models
{
    [Description("店铺商品分类")]
    public class ShopGoodsCategory:EntityBase<int>
    {
        /// <summary>
        /// 获取或设置 分类所属店铺
        /// </summary>
        public virtual Shop Shop { get; set; }

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
    }
}

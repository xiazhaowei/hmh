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
    /// 实体类--SKU
    /// </summary>
    [Description("商品SKU")]
    public class Sku:EntityBase<int>
    {
        /// <summary>
        /// 获取或设置 所属商品
        /// </summary>
        public Goods Goods { get; set; }
        /// <summary>
        /// 获取或设置 规格1名
        /// </summary>
        public string Names { get; set; }
        /// <summary>
        /// 获取或设置 规格1值
        /// </summary>
        public string Values { get; set; }        

        /// <summary>
        /// 获取或设置 商品编号
        /// </summary>
        public string GoodsNumber { get; set; }

        /// <summary>
        /// 获取或设置 条形码
        /// </summary>
        public string BarCode { get; set; }
        /// <summary>
        /// 获取或设置 价格
        /// </summary>
        [Required]
        public decimal Price { get; set; }

        /// <summary>
        /// 获取或设置 库存量
        /// </summary>
        [Required]
        public int Stock { get; set; }

        /// <summary>
        /// 获取或设置 商品 sku图片地址
        /// </summary>
        public string SkuPic { get; set; }
    }
}

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
    public class SkuInputDto:IInputDto<int>
    {
        /// <summary>
        /// 获取或设置 主键，唯一标识
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 获取或设置 所属商品
        /// </summary>
        public int GoodsId { get; set; }
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
    }
}

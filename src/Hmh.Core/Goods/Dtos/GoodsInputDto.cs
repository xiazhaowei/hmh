using OSharp.Core.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hmh.Core.Goods.Dtos
{
    public class GoodsInputDto:IInputDto<int>
    {
        /// <summary>
        /// 获取或设置 主键，唯一标识
        /// </summary>
        public int Id { get; set; }


        /// <summary>
        /// 获取设置 商品名称
        /// </summary>
        [Required]
        [StringLength(100)]
        public string Name { get; set; }


        /// <summary>
        /// 获取或设置 图片
        /// </summary>
        public string GoodsPics { get; set; }

        /// <summary>
        /// 获取设置 产品所属分类
        /// </summary>
        public int? CategoryId { get; set; }       

        /// <summary>
        /// 获取设置 商品简单介绍
        /// </summary>
        [StringLength(200)]
        public string Description { get; set; }

        /// <summary>
        /// 产品详情
        /// </summary>
        public string Detail { get; set; }

        /// <summary>
        /// 获取设置 一口价
        /// </summary>
        [Required]
        public decimal Price { get; set; }

        /// <summary>
        /// 获取或设置 总库存
        /// </summary>
        [Required]
        public int Stock { get; set; }

        /// <summary>
        /// 获取或设置 商品编号
        /// </summary>
        public string GoodsNumber { get; set; }

        /// <summary>
        /// 获取或设置 条形码
        /// </summary>
        public string BarCode { get; set; }

        /// <summary>
        /// 获取设置 所属店铺
        /// </summary>
        public int? ShopId { get; set; }

        /// <summary>
        /// 获取或设置 运费模板
        /// </summary>
        public int? ExpressTemplateId { get; set; }

        /// <summary>
        /// 获取或设置 是否提供发票
        /// </summary>
        public bool IsReceipt { get; set; }

        /// <summary>
        /// 获取或设置 是否保修服务
        /// </summary>
        public bool IsGuarantee { get; set; }

        /// <summary>
        /// 获取或设置 是否退换货承诺
        /// </summary>
        public bool IsReplacement { get; set; }

        /// <summary>
        /// 获取或设置 是否支持7天退货服务
        /// </summary>
        public bool IsSevenDayReplacement { get; set; }
        /// <summary>
        /// 获取或设置 是否橱窗推荐
        /// </summary>
        public bool IsCommend { get; set; }

        /// <summary>
        /// 获取或设置 商品属性
        /// </summary>
        public ICollection<GoodsAttr> GoodsAttrs { get; set; }

        /// <summary>
        /// 获取或设置 SKU
        /// </summary>
        public ICollection<Sku> Skus { get; set; }
        public GoodsInputDto()
        {
            GoodsAttrs = new List<GoodsAttr>();
            Skus = new List<Sku>();
        }
    }

    public class GoodsAttr : IInputDto<int>
    {
        /// <summary>
        /// 获取或设置 主键，唯一标识
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 获取或设置 属性名称
        /// </summary>
        public string AttrName { get; set; }

        /// <summary>
        /// 获取或设置 属性值
        /// </summary>
        public string AttrValue { get; set; }
    }

    public class Sku : IInputDto<int>
    {
        /// <summary>
        /// 获取或设置 主键，唯一标识
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 获取或设置 规格名称
        /// </summary>
        public ICollection<string> Names { get; set; }

        /// <summary>
        /// 获取或设置 规格值
        /// </summary>
        public ICollection<string> Values { get; set; }

        /// <summary>
        /// 获取或设置 商品编码
        /// </summary>
        public string GoodsNumber { get; set; }

        /// <summary>
        /// 获取或设置 条形码
        /// </summary>
        public string BarCode { get; set; }

        /// <summary>
        /// 获取或设置 价格
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// 获取或设置 库存
        /// </summary>
        public int Stock { get; set; }

        /// <summary>
        /// 获取或设置 SKU图片
        /// </summary>
        public string SkuPic { get; set; }

    }
}

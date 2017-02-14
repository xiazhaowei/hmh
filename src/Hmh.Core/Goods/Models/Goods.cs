using OSharp.Core.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hmh.Core.Shop.Models;
using System.ComponentModel;

namespace Hmh.Core.Goods.Models
{
    /// <summary>
    /// 实体类 商品
    /// </summary>
    [Description("商品-商品信息")]
    public class Goods:EntityBase<int>,ICreatedTime,ILockable
    {
        public Goods()
        {
            Skus = new List<Sku>();
            GoodsAttrs = new List<GoodsAttr>();           
        }


       
        /// <summary>
        /// 获取设置 商品名称
        /// </summary>
        [Required][StringLength(100)]
        public string Name { get; set; }        

        /// <summary>
        /// 获取设置 产品类别
        /// </summary>
        public virtual Category Category { get; set; }
        /// <summary>
        /// 获取设置 商品简单介绍
        /// </summary>
        [StringLength(200)]
        public string Description { get; set; }

        /// <summary>
        /// 产品详情
        /// </summary>
        public string Detail { get; set; }

        #region 一口价信息
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

        #endregion
        /// <summary>
        /// 获取设置 所属店铺
        /// </summary>
        public virtual Hmh.Core.Shop.Models.Shop Shop { get; set; }

        /// <summary>
        /// 获取设置 所属店铺分类
        /// </summary>
        public virtual Shop.Models.ShopGoodsCategory ShopGoodsCategory { get; set; }

        /// <summary>
        /// 获取设置 商品规格库存库
        /// </summary>
        public virtual ICollection<Sku> Skus { get; set; }

        /// <summary>
        /// 获取或设置 商品属性
        /// </summary>
        public virtual ICollection<GoodsAttr> GoodsAttrs { get; set; }
        /// <summary>
        /// 获取设置 商品主图图片用 字符分割 最好用 | 分割
        /// </summary>
        public string GoodsPics { get; set; }

        /// <summary>
        /// 获取或设置 运费模板
        /// </summary>
        public virtual ExpressTemplate ExpressTemplate { get; set; }

        /// <summary>
        /// 获取或设置 商品评论
        /// </summary>
        public virtual ICollection<GoodsComment> GoodsComments { get; set; }

        /// <summary>
        /// 获取或设置 已销售数量
        /// </summary>
        public int SellCount { get; set; }

        /// <summary>
        /// 获取或设置 排序编号 该编号根据规则定期刷新
        /// </summary>
        public int SortCode { get; set; }

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
        /// 获取或设置 上架时间
        /// </summary>
        public DateTime BeginTime { get; set; }

        #region Implementration of ICreateTime

        /// <summary>
        /// 获取或设置 信息创建时间
        /// </summary>
        public DateTime CreatedTime { get; set; }
        #endregion

        #region Implementration of ILockable
        /// <summary>
        /// 获取或设置 是否锁定，用于禁用当前信息
        /// </summary>
        public bool IsLocked { get; set; }
        #endregion

    }    
}

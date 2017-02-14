using Hmh.Core.Identity.Models;
using OSharp.Core.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hmh.Core.Goods.Models;
using System.ComponentModel;

namespace Hmh.Core.Shop.Models
{
    /// <summary>
    /// 实体类  店铺
    /// </summary>
    [Description("店铺-店铺信息")]
    public class Shop:EntityBase<int>,ICreatedTime
    {

        public Shop()
        {
            Contracts = new List<Contract>();
            Goodses = new List<Goods.Models.Goods>();
            ExpressTemplates = new List<ExpressTemplate>();
            Orders = new List<Order.Models.Order>();
            ShopGoodsCategoryes = new List<ShopGoodsCategory>();
        }        

        /// <summary>
        /// 获取设置 店铺名称
        /// </summary>
        [Required, StringLength(50)]
        public string Name { get; set; }

        /// <summary>
        /// 店铺简介
        /// </summary>
        [StringLength(50)]
        public string Description { get; set; }

        /// <summary>
        /// 获取设置 店铺拥有着
        /// </summary>
        public virtual User User { get; set; }

        /// <summary>
        /// 获取设置 联系人
        /// </summary>
        public string LinkMan { get; set; }
        /// <summary>
        /// 获取设置 联系人手机号
        /// </summary>
        public string LinkManPhone { get; set; }
        /// <summary>
        /// 获取设置 店铺区域
        /// </summary>
        public virtual Region Region { get; set; }

        /// <summary>
        /// 详细地址
        /// </summary>
        [StringLength(100)]
        public string AddrDetail { get; set; }        

        /// <summary>
        /// 获取设置 店铺类型
        /// </summary>
        public StoreType Type { get; set; }

        /// <summary>
        /// 获取设置 营业状态
        /// </summary>
        public ShopBusinessState BusinessState { get; set; }

        /// <summary>
        /// 获取设置  H币消费限制
        /// </summary>
        [Required]
        public int HCoinLimit { get; set; }

        /// <summary>
        /// 获取设置 店铺状态
        /// </summary>
        public ShopState State { get; set; }
        

        /// <summary>
        /// 获取设置 店铺现在的合同
        /// </summary>
        public virtual Contract CurrentContract { get; set; }

        /// <summary>
        /// 获取设置 店铺所有合同
        /// </summary>
        public virtual ICollection<Contract> Contracts { get; set; }

        /// <summary>
        /// 获取设置 商家的商品
        /// </summary>
        public virtual ICollection<Hmh.Core.Goods.Models.Goods> Goodses { get; set; }

        /// <summary>
        /// 获取或设置 运费模板
        /// </summary>
        public virtual ICollection<ExpressTemplate> ExpressTemplates { get; set; }
        /// <summary>
        /// 获取或设置 商家的相册
        /// </summary>
        public virtual ICollection<Album> Albums { get; set; }

        /// <summary>
        /// 获取或设置 订单
        /// </summary>
        public virtual ICollection<Order.Models.Order> Orders { get; set; }
        
        /// <summary>
        /// 获取或设置 店铺的商品分类
        /// </summary>
        public virtual ICollection<ShopGoodsCategory> ShopGoodsCategoryes { get; set; }

        /// <summary>
        /// 获取设置 商家认证资料
        /// </summary>
        public virtual ShopPermit ShopPermit { get; set; }

        #region Implementation of ICreateTime
        /// <summary>
        /// 获取设置 信息创建时间
        /// </summary>
        public DateTime CreatedTime { get; set; }      

        #endregion
    }

    /// <summary>
    /// 枚举 店铺类型
    /// </summary>
    [Description("枚举-店铺类型")]
    public enum StoreType
    {        
        /// <summary>
        /// 店铺，非自营
        /// </summary>
        [Description("店铺，非自营")]
        Store,

        /// <summary>
        /// 自营
        /// </summary>
        [Description("自营")]
        Self
    }

    [Description("店铺营业状态")]
    public enum ShopBusinessState
    {
        /// <summary>
        /// 营业中
        /// </summary>
        [Description("营业中")]
        InOperation,
        /// <summary>
        /// 休息
        /// </summary>
        [Description("休息中")]
        Pause
    }


    [Description("枚举-店铺状态")]
    public enum ShopState
    {
        /// <summary>
        /// 店铺正常
        /// </summary>
        [Description("店铺正常")]
        Normal,
        /// <summary>
        /// 店铺锁定
        /// </summary>
        [Description("店铺被锁定")]
        Locked
    }
}

using Hmh.Core.Shop.Models;
using OSharp.Core.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hmh.Core.Shop.Dtos
{
    public class ShopInputDto : IInputDto<int>
    {
        /// <summary>
        /// 获取或设置 主键，唯一标识
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 获取设置 店铺名称
        /// </summary>
        [Required, StringLength(50)]
        [Display(Name = "店铺名称")]
        public string Name { get; set; }

        /// <summary>
        /// 获取设置 店铺地区
        /// </summary>
        public int? RegionId { get; set; }

        /// <summary>
        /// 获取设置 地区名称
        /// </summary>
        public string RegionStr { get; set; }

        /// <summary>
        /// 获取或设置 店铺简介
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 获取或设置 联系人
        /// </summary>
        [Required,StringLength(50)]
        [Display(Name = "店铺联系人")]
        public string LinkMan { get; set; }

        /// <summary>
        /// 获取或设置 联系人手机号
        /// </summary>
        [Required,StringLength(50)]
        [Display(Name = "联系人手机号")]
        public string LinkManPhone { get; set; }

        /// <summary>
        /// 获取设置  H币消费限制
        /// </summary>        
        public int HCoinLimit { get; set; }

        /// <summary>
        /// 获取设置 店铺详细地址
        /// </summary>
        [StringLength(100)]
        public string AddrDetail { get; set; }

        /// <summary>
        /// 获取设置 店铺类型
        /// </summary>
        public StoreType Type { get; set; }

        /// <summary>
        /// 获取设置 店铺类型
        /// </summary>
        public ShopState State { get; set; }

        /// <summary>
        /// 获取或设置 店铺营业状态
        /// </summary>
        public ShopBusinessState BusinessState { get; set; }

        /// <summary>
        /// 获取设置 开店人编号
        /// </summary>
        public int? UserId { get; set; }
    }
}

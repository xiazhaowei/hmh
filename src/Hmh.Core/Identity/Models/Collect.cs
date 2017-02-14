using OSharp.Core.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hmh.Core.Identity.Models
{
    [Description("认证-收藏")]
    public class Collect:EntityBase<int>,ICreatedTime
    {

        /// <summary>
        /// 获取设置 用户信息
        /// </summary>
        public virtual User User { get; set; }

        /// <summary>
        /// 获取设置 收藏名称
        /// </summary>
        [Required][StringLength(50)]
        public string Name { get; set; }

        /// <summary>
        /// 获取设置 收藏主图
        /// </summary>
        [Required][StringLength(50)]
        public string Pic { get; set; }

        /// <summary>
        /// 获取设置 卡号
        /// </summary>
        [Required]
        public int AboutId { get; set; }

        /// <summary>
        /// 获取设置 收藏类型
        /// </summary>
        public CollectType Type { get; set; }


        /// <summary>
        /// 获取或设置 信息创建时间
        /// </summary>
        public DateTime CreatedTime { get; set; }

    }

    [Description("收藏类型")]
    public enum CollectType
    {
        /// <summary>
        /// 商品
        /// </summary>
        [Description("商品")]
        Goods,

        /// <summary>
        /// 店铺
        /// </summary>
        [Description("店铺")]
        Shop
    }
}

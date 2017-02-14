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
    [Description("商品评论")]
    public class GoodsComment:EntityBase<int>,ICreatedTime
    {

        /// <summary>
        /// 获取或设置 所属商品
        /// </summary>
        public virtual Goods Goods { get; set; }

        /// <summary>
        /// 获取或设置 评论人
        /// </summary>
        public virtual Identity.Models.User User { get; set; }

        /// <summary>
        /// 获取或设置 用户购物商品规格
        /// </summary>
        [StringLength(200)]
        public string SkuInfo { get; set; }

        /// <summary>
        /// 获取或设置 评论内容
        /// </summary>
        [Required][StringLength(200)]
        public string Content { get; set; }
        
        /// <summary>
        /// 获取或设置 评论图片
        /// </summary>
        [StringLength(400)]
        public string Pics { get; set; }

        #region Implementration of ICreateTime

        /// <summary>
        /// 获取或设置 信息创建时间
        /// </summary>
        public DateTime CreatedTime { get; set; }
        #endregion


    }
}

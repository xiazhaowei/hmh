using OSharp.Core.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hmh.Core.Goods.Dtos
{
    public class GoodsCommentInputDto:IInputDto<int>
    {
        /// <summary>
        /// 获取或设置 主键，唯一标识
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 获取设置 商品ID
        /// </summary>
        public int? GoodsId { get; set; }

        /// <summary>
        /// 获取设置 评论用户ID
        /// </summary>
        public int? UserId { get; set; }

        /// <summary>
        /// 获取设置 商品SKU
        /// </summary>
        [Required]
        [StringLength(200)]
        public string SkuInfo { get; set; }


        /// <summary>
        /// 获取或设置 评论内容
        /// </summary>
        [StringLength(200)]
        public string Content { get; set; }

             

        /// <summary>
        /// 获取设置 评论图片
        /// </summary>
        [StringLength(200)]
        public string Pics { get; set; }
        

               
        
    }    
}

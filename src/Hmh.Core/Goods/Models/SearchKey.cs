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
    /// 实体类   搜索关键词
    /// </summary>
    [Description("搜索-搜索关键词")]
    public class SearchKey:EntityBase<int>
    {     
        
        /// <summary>
        /// 获取设置 属性名称 全部小写形式
        /// </summary>
        [Required][StringLength(50)]
        public string Key { get; set; }

        /// <summary>
        /// 获取设置 搜索关键词的拼音
        /// </summary>
        [StringLength(200)]
        public string Pinyin { get; set; }
        
        /// <summary>
        /// 获取设置 拼音简码
        /// </summary>
        [StringLength(50)]
        public string PinyinFirst { get; set; }

        /// <summary>
        /// 获取设置 商品大致数量
        /// </summary>
        public int GoodsCount { get; set; }
       
        /// <summary>
        /// 获取设置 排序号
        /// </summary>
        public int SortCode { get; set; }   
        
        /// <summary>
        /// 获取设置 搜索次数
        /// </summary>
        public int SearchCount { get; set; }     
    }

    
}

using OSharp.Core.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hmh.Core.Shop.Models
{
    /// <summary>
    /// 实体类-店铺图库
    /// </summary>
    [Description("店铺图库")]
    public class Album: EntityBase<int>
    {
        /// <summary>
        /// 获取或设置 所属商家
        /// </summary>
        public virtual Hmh.Core.Shop.Models.Shop Shop { get; set; }

        /// <summary>
        /// 获取或设置 相册名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 获取或设置 图片集合
        /// </summary>
        public string Images { get; set; }
    }
}

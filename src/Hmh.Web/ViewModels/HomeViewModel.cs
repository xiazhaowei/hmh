using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hmh.Web.ViewModels
{
    public class AttrFilter
    {
        /// <summary>
        /// 获取或设置 属性名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 获取或设置 筛选值
        /// </summary>
        public ICollection<string> Values { get; set; }

        public AttrFilter()
        {
            Values = new List<string>();
        }
    }

    public class SpecFilter
    {
        /// <summary>
        /// 获取或设置 规格名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 获取或设置 筛选值
        /// </summary>
        public ICollection<string> Values { get; set; }

        public SpecFilter()
        {
            Values = new List<string>();
        }
    }
}
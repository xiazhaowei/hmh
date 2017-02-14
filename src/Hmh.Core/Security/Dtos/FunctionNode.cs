// -----------------------------------------------------------------------
//  <copyright file="FunctionNode.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2016 OSharp. All rights reserved.
//  </copyright>
//  <site>http://www.osharp.org</site>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2016-03-10 11:45</last-date>
// -----------------------------------------------------------------------

using System.Collections.Generic;

using OSharp.Core.Security;


namespace Hmh.Core.Security.Dtos
{
    /// <summary>
    /// 功能树节点
    /// </summary>
    public class FunctionNode
    {
        /// <summary>
        /// 获取或设置 功能编号
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 获取或设置 验证类型
        /// </summary>
        public FilterType FilterType { get; set; }

        /// <summary>
        /// 获取或设置 子级功能节点集合
        /// </summary>
        public ICollection<FunctionNode> Children { get; set; }
    }
}
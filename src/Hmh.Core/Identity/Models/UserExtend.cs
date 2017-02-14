// -----------------------------------------------------------------------
//  <copyright file="UserExtend.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2015 OSharp. All rights reserved.
//  </copyright>
//  <site>http://www.osharp.org</site>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2015-11-27 20:01</last-date>
// -----------------------------------------------------------------------

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

using OSharp.Core.Data;
using System;

namespace Hmh.Core.Identity.Models
{
    /// <summary>
    /// 实体类——用户扩展信息
    /// </summary>
    [Description("用户扩展信息")]
    public class UserExtend : EntityBase<int>
    {        
        /// <summary>
        /// 获取设置 人民币账户
        /// </summary>
        public decimal RmbCoin { get; set; }
        /// <summary>
        /// 获取设置 H币账户
        /// </summary>
        public decimal HCoin { get; set; }

        [StringLength(15)]
        public string RegistedIp { get; set; }

        [StringLength(100)]
        public string HeadImage { get; set; }

        /// <summary>
        /// 获取或设置 设置信息
        /// </summary>
        public string Settings { get; set; }

        /// <summary>
        /// 获取或设置 性别
        /// </summary>
        [StringLength(20)]
        public string Sex { get; set; }
        /// <summary>
        /// 获取或设置 生日
        /// </summary>
        public DateTime Birthday { get; set; }

        /// <summary>
        /// 获取或设置 用户
        /// </summary>
        public virtual User User { get; set; }
    }
}
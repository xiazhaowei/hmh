// -----------------------------------------------------------------------
//  <copyright file="UserInputDto.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2015 OSharp. All rights reserved.
//  </copyright>
//  <site>http://www.osharp.org</site>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2015-11-27 20:01</last-date>
// -----------------------------------------------------------------------

using Hmh.Core.Identity.Models;
using OSharp.Core.Data;
using OSharp.Core.Identity.Dtos;
using System;
using System.ComponentModel.DataAnnotations;

namespace Hmh.Core.Identity.Dtos
{
    /// <summary>
    /// 输入DTO——银行卡信息
    /// </summary>
    public class CollectInputDto : IInputDto<int>
    {
        public int Id { get; set; }

        /// <summary>
        /// 获取设置 用户信息
        /// </summary>
        public int? UserId { get; set; }

        /// <summary>
        /// 获取设置 收藏名称
        /// </summary>
        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        /// <summary>
        /// 获取设置 收藏主图
        /// </summary>
        [Required]
        [StringLength(50)]
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
    }
}
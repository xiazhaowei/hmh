// -----------------------------------------------------------------------
//  <copyright file="UserInputDto.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2015 OSharp. All rights reserved.
//  </copyright>
//  <site>http://www.osharp.org</site>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2015-11-27 20:01</last-date>
// -----------------------------------------------------------------------

using OSharp.Core.Data;
using OSharp.Core.Identity.Dtos;
using System;
using System.ComponentModel.DataAnnotations;

namespace Hmh.Core.Identity.Dtos
{
    /// <summary>
    /// 输入DTO——收件地址信息
    /// </summary>
    public class DeliverAddressInputDto : IInputDto<int>
    {
        public int Id { get; set; }

        /// <summary>
        /// 获取设置 用户信息
        /// </summary>
        public int? UserId { get; set; }

        /// <summary>
        /// 收件人
        /// </summary>
        [Required]
        [StringLength(20)]
        public string Name { get; set; }

        /// <summary>
        /// 获取设置 收件区域
        /// </summary>
        [Required]
        [StringLength(50)]
        public string Region { get; set; }

        /// <summary>
        /// 详细收件地址
        /// </summary>
        [Required]
        [StringLength(200)]
        public string DetailAddress { get; set; }

        /// <summary>
        /// 获取设置 收件人手机
        /// </summary>
        [Required]
        [StringLength(20)]
        public string Mobile { get; set; }

        /// <summary>
        /// 获取设置 邮编
        /// </summary>
        [Required]
        [StringLength(20)]
        public string Zip { get; set; }

        /// <summary>
        /// 获取设置 是否默认
        /// </summary>
        public bool IsDefault { get; set; }
    }
}
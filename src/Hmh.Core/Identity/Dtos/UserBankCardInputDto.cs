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
    /// 输入DTO——银行卡信息
    /// </summary>
    public class UserBankCardInputDto : IInputDto<int>
    {
        public int Id { get; set; }

        /// <summary>
        /// 获取设置 用户信息
        /// </summary>
        public int? UserId { get; set; }

        /// <summary>
        /// 获取设置 持卡人姓名
        /// </summary>
        [Required]
        [StringLength(20)]
        public string UserName { get; set; }

        /// <summary>
        /// 获取设置 银行名 工行|支付宝
        /// </summary>
        [Required]
        [StringLength(20)]
        public string BankName { get; set; }

        /// <summary>
        /// 获取设置 卡号
        /// </summary>
        [Required]
        [StringLength(20)]
        public string CardNumber { get; set; }
    }
}
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

namespace Hmh.Core.Identity.Dtos
{
    /// <summary>
    /// 输入DTO——用户信息
    /// </summary>
    public class UserExtendInputDto : IInputDto<int>
    {
        public int Id { get; set; }        

        public string NickName { get; set; }

        public string PhoneNumber { get; set; }

        public string Sex { get; set; }

        public DateTime Birthday { get; set; }
    }

    public class UserPasswordInputDto : IInputDto<int>
    {
        public int Id { get; set; }

        public string Password { get; set; }
    }
}
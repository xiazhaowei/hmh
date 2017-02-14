// -----------------------------------------------------------------------
//  <copyright file="UserLogin.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2015 OSharp. All rights reserved.
//  </copyright>
//  <site>http://www.osharp.org</site>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2015-11-27 20:01</last-date>
// -----------------------------------------------------------------------

using System;
using System.ComponentModel;

using OSharp.Core.Identity.Models;


namespace Hmh.Core.Identity.Models
{
    /// <summary>
    /// 实体类——用户第三方登录（OAuth）信息
    /// </summary>
    [Description("第三方登录信息")]
    public class UserLogin : UserLoginBase<Guid, User, int>
    { }
}
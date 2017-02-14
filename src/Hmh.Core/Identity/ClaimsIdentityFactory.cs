// -----------------------------------------------------------------------
//  <copyright file="ClaimsIdentityFactory.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2016 OSharp. All rights reserved.
//  </copyright>
//  <site>http://www.osharp.org</site>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2016-03-08 9:48</last-date>
// -----------------------------------------------------------------------

using OSharp.Core.Identity;
using Hmh.Core.Identity.Models;


namespace Hmh.Core.Identity
{
    /// <summary>
    /// ClaimsIdentity创建工厂
    /// </summary>
    public class ClaimsIdentityFactory : ClaimsIdentityFactoryBase<User, int>
    { }
}
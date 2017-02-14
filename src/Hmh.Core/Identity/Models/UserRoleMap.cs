// -----------------------------------------------------------------------
//  <copyright file="UserRoleMap.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2015 OSharp. All rights reserved.
//  </copyright>
//  <site>http://www.osharp.org</site>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2015-11-27 20:01</last-date>
// -----------------------------------------------------------------------

using System.ComponentModel;

using OSharp.Core.Identity.Models;


namespace Hmh.Core.Identity.Models
{
    /// <summary>
    /// 实体类——用户角色映射信息
    /// </summary>
    [Description("用户角色映射信息")]
    public class UserRoleMap : UserRoleMapBase<int, User, int, Role, int>
    { }
}
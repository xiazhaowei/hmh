// -----------------------------------------------------------------------
//  <copyright file="RoleManager.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2015 OSharp. All rights reserved.
//  </copyright>
//  <site>http://www.osharp.org</site>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2015-11-27 20:01</last-date>
// -----------------------------------------------------------------------

using Microsoft.AspNet.Identity;

using OSharp.Core.Identity;
using Hmh.Core.Identity.Models;


namespace Hmh.Core.Identity
{
    /// <summary>
    /// 角色管理器
    /// </summary>
    public class RoleManager : RoleManagerBase<Role, int>
    {
        public RoleManager(IRoleStore<Role, int> store)
            : base(store)
        { }
    }
}
﻿// -----------------------------------------------------------------------
//  <copyright file="EntityRoleMap.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2015 OSharp. All rights reserved.
//  </copyright>
//  <site>http://www.osharp.org</site>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2015-11-27 20:01</last-date>
// -----------------------------------------------------------------------

using System;
using System.ComponentModel;

using OSharp.Core.Security;
using OSharp.Core.Security.Models;
using Hmh.Core.Identity.Models;


namespace Hmh.Core.Security.Models
{
    /// <summary>
    /// 实体类——数据角色映射信息
    /// </summary>
    [Description("数据角色映射信息")]
    public class EntityRoleMap : EntityRoleMapBase<int, EntityInfo, Guid, Role, int>
    { }
}
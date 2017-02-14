// -----------------------------------------------------------------------
//  <copyright file="SecurityManager.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2016 OSharp. All rights reserved.
//  </copyright>
//  <site>http://www.osharp.org</site>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2016-03-14 14:59</last-date>
// -----------------------------------------------------------------------

using System;

using OSharp.Core.Security;
using Hmh.Core.Identity.Models;
using Hmh.Core.Security.Dtos;
using Hmh.Core.Security.Models;


namespace Hmh.Core.Security
{
    /// <summary>
    /// 权限安全管理器
    /// </summary>
    public class SecurityManager
        : SecurityManagerBase<Role, int, User, int, Module, ModuleInputDto, int, Function, FunctionInputDto, Guid,
              EntityInfo, EntityInfoInputDto, Guid, FunctionUserMap, FunctionUserMapInputDto, int,
              EntityRoleMap, EntityRoleMapInputDto, int, EntityUserMap, EntityUserMapInputDto, int>
    { }
}
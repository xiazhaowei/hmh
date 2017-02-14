// -----------------------------------------------------------------------
//  <copyright file="Module.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2016 OSharp. All rights reserved.
//  </copyright>
//  <site>http://www.osharp.org</site>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2016-03-14 14:57</last-date>
// -----------------------------------------------------------------------

using System;

using OSharp.Core.Security;
using OSharp.Core.Security.Models;
using Hmh.Core.Identity.Models;
using System.ComponentModel;

namespace Hmh.Core.Security.Models
{
    /// <summary>
    /// 实体类——模块信息
    /// </summary>
    [Description("模块信息")]
    public class Module : ModuleBase<int, Module, Function, Guid, Role, int, User, int>
    { }
}
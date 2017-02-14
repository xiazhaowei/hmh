// -----------------------------------------------------------------------
//  <copyright file="UserStore.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2015 OSharp. All rights reserved.
//  </copyright>
//  <site>http://www.osharp.org</site>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2015-11-27 20:01</last-date>
// -----------------------------------------------------------------------

using System;

using OSharp.Core.Identity;
using Hmh.Core.Identity.Dtos;
using Hmh.Core.Identity.Models;


namespace Hmh.Core.Identity
{
    public class UserStore
        : UserStoreBase<User, int, Role, int, UserRoleMap, UserRoleMapInputDto, int, UserLogin, Guid, UserClaim, Guid>
    { }
}
// -----------------------------------------------------------------------
//  <copyright file="OAuthClientRefreshTokenStore.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2015 OSharp. All rights reserved.
//  </copyright>
//  <site>http://www.osharp.org</site>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2015-11-27 20:01</last-date>
// -----------------------------------------------------------------------

using System;

using OSharp.Core.Security;
using Hmh.Core.Identity.Models;
using Hmh.Core.OAuth.Models;


namespace Hmh.Core.OAuth
{
    public class OAuthClientRefreshTokenStore : OAuthClientRefreshTokenStoreBase<OAuthClientRefreshToken, Guid, OAuthClient, int, User, int>
    { }
}
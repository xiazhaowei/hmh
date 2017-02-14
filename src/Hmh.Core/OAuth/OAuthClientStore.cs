// -----------------------------------------------------------------------
//  <copyright file="OAuthClientStore.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2015 OSharp. All rights reserved.
//  </copyright>
//  <site>http://www.osharp.org</site>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2015-11-27 20:01</last-date>
// -----------------------------------------------------------------------

using OSharp.Core.Security;
using Hmh.Core.OAuth.Dtos;
using Hmh.Core.OAuth.Models;


namespace Hmh.Core.OAuth
{
    public class OAuthClientStore : OAuthClientStoreBase<OAuthClient, int, OAuthClientSecret, int, OAuthClientInputDto, OAuthClientSecretInputDto>
    { }
}
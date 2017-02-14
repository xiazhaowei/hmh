// -----------------------------------------------------------------------
//  <copyright file="OAuthClientSecret.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2015 OSharp. All rights reserved.
//  </copyright>
//  <site>http://www.osharp.org</site>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2015-11-27 20:01</last-date>
// -----------------------------------------------------------------------

using System.ComponentModel;

using OSharp.Core.Security.Models;


namespace Hmh.Core.OAuth.Models
{
    /// <summary>
    /// 实体类——OAuth客户端密钥信息
    /// </summary>
    [Description("OAuth客户端密钥信息")]
    public class OAuthClientSecret : OAuthClientSecretBase<int, OAuthClient, int>
    { }
}
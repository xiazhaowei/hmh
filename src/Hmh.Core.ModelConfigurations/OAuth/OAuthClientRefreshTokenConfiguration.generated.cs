﻿// <autogenerated>
//   This file was generated by T4 code generator ModelCodeScript.tt.
//   Any changes made to this file manually will be lost next time the file is regenerated.
// </autogenerated>

// -----------------------------------------------------------------------
//  <copyright file="OAuthClientRefreshTokenConfiguration.generated.cs" company="OSharp开源团队">
//      Copyright (c) 2015 OSHARP. All rights reserved.
//  </copyright>
//  <site>http://www.osharp.org</site>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2016-03-14 15:45</last-date>
// -----------------------------------------------------------------------

using System;

using OSharp.Data.Entity;

using Hmh.Core.OAuth.Models;


namespace Hmh.Core.ModelConfigurations.OAuth
{
    /// <summary>
    /// 实体类-数据表映射——OAuth客户端刷新Token信息
    /// </summary> 
	public partial class OAuthClientRefreshTokenConfiguration : EntityConfigurationBase<OAuthClientRefreshToken, Guid>
    { 
        /// <summary>
        /// 初始化一个<see cref="OAuthClientRefreshTokenConfiguration"/>类型的新实例
        /// </summary>
        public OAuthClientRefreshTokenConfiguration()
        {
            OAuthClientRefreshTokenConfigurationAppend();
        }

        /// <summary>
        /// 额外的数据映射
        /// </summary>
        partial void OAuthClientRefreshTokenConfigurationAppend();
    }
}

﻿// <autogenerated>
//   This file was generated by T4 code generator ModelCodeScript.tt.
//   Any changes made to this file manually will be lost next time the file is regenerated.
// </autogenerated>

// -----------------------------------------------------------------------
//  <copyright file="UserRoleMapConfiguration.generated.cs" company="OSharp开源团队">
//      Copyright (c) 2015 OSHARP. All rights reserved.
//  </copyright>
//  <site>http://www.osharp.org</site>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2016-03-14 15:45</last-date>
// -----------------------------------------------------------------------

using System;

using OSharp.Data.Entity;

using Hmh.Core.Identity.Models;


namespace Hmh.Core.ModelConfigurations.Identity
{
    /// <summary>
    /// 实体类-数据表映射——用户角色映射信息
    /// </summary> 
	public partial class UserRoleMapConfiguration : EntityConfigurationBase<UserRoleMap, int>
    { 
        /// <summary>
        /// 初始化一个<see cref="UserRoleMapConfiguration"/>类型的新实例
        /// </summary>
        public UserRoleMapConfiguration()
        {
            UserRoleMapConfigurationAppend();
        }

        /// <summary>
        /// 额外的数据映射
        /// </summary>
        partial void UserRoleMapConfigurationAppend();
    }
}
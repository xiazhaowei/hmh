﻿// -----------------------------------------------------------------------
//  <copyright file="UserRoleMapConfiguration.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2015 OSharp. All rights reserved.
//  </copyright>
//  <site>http://www.osharp.org</site>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2015-11-27 20:00</last-date>
// -----------------------------------------------------------------------

namespace Hmh.Core.ModelConfigurations.Identity
{
    public partial class UserRoleMapConfiguration
    {
        partial void UserRoleMapConfigurationAppend()
        {
            HasRequired(m => m.User).WithMany();
            HasRequired(m => m.Role).WithMany();
        }
    }
}
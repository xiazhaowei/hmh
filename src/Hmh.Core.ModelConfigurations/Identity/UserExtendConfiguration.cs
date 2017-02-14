// -----------------------------------------------------------------------
//  <copyright file="UserExtendConfiguration.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2015 OSharp. All rights reserved.
//  </copyright>
//  <site>http://www.osharp.org</site>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2015-11-27 20:00</last-date>
// -----------------------------------------------------------------------

using System.ComponentModel.DataAnnotations.Schema;

namespace Hmh.Core.ModelConfigurations.Identity
{
    public partial class UserExtendConfiguration
    {
        partial void UserExtendConfigurationAppend()
        {
            Property(ud => ud.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);       
            HasRequired(m => m.User).WithRequiredDependent(n => n.UserExtend);
        }
    }
}
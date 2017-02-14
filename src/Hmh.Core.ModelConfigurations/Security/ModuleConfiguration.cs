// -----------------------------------------------------------------------
//  <copyright file="ModuleConfiguration.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2016 OSharp. All rights reserved.
//  </copyright>
//  <site>http://www.osharp.org</site>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2016-03-14 15:46</last-date>
// -----------------------------------------------------------------------

namespace Hmh.Core.ModelConfigurations.Security
{
    public partial class ModuleConfiguration
    {
        partial void ModuleConfigurationAppend()
        {
            HasOptional(m => m.Parent).WithMany(n => n.SubModules);
            HasMany(m => m.Functions).WithMany();
            HasMany(m => m.Roles).WithMany();
            HasMany(m => m.Users).WithMany();
        }
    }
}
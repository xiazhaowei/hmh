// -----------------------------------------------------------------------
//  <copyright file="EntityUserMapInputDto.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2015 OSharp. All rights reserved.
//  </copyright>
//  <site>http://www.osharp.org</site>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2015-11-27 20:01</last-date>
// -----------------------------------------------------------------------

using System;

using OSharp.Core.Security.Dtos;


namespace Hmh.Core.Security.Dtos
{
    /// <summary>
    /// 输入DTO——数据用户映射信息
    /// </summary>
    public class EntityUserMapInputDto : EntityUserMapBaseInputDto<int, Guid, int>
    { }
}
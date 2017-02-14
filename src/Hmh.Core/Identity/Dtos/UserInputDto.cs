// -----------------------------------------------------------------------
//  <copyright file="UserInputDto.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2015 OSharp. All rights reserved.
//  </copyright>
//  <site>http://www.osharp.org</site>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2015-11-27 20:01</last-date>
// -----------------------------------------------------------------------

using OSharp.Core.Identity.Dtos;


namespace Hmh.Core.Identity.Dtos
{
    /// <summary>
    /// 输入DTO——用户信息
    /// </summary>
    public class UserInputDto : UserBaseInputDto<int>
    {
        /// <summary>
        /// 获取设置 推荐人ID
        /// </summary>
        public int RecommendId { get; set; }
    }
}
// -----------------------------------------------------------------------
//  <copyright file="UserValidator.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2016 OSharp. All rights reserved.
//  </copyright>
//  <site>http://www.osharp.org</site>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2016-03-08 9:24</last-date>
// -----------------------------------------------------------------------

using Microsoft.AspNet.Identity;

using OSharp.Core.Identity;
using Hmh.Core.Identity.Models;


namespace Hmh.Core.Identity
{
    /// <summary>
    /// 用户验证类
    /// </summary>
    public class UserValidator : UserValidatorBase<User, int>
    {
        /// <summary>
        /// 初始化一个<see cref="UserValidator"/>类型的新实例
        /// </summary>
        public UserValidator(UserManager<User, int> manager)
            : base(manager)
        { }
    }
}
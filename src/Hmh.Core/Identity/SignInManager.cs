// -----------------------------------------------------------------------
//  <copyright file="SignInManager.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2015 OSharp. All rights reserved.
//  </copyright>
//  <site>http://www.osharp.org</site>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2015-11-27 20:01</last-date>
// -----------------------------------------------------------------------

using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;

using OSharp.Core.Dependency;
using Hmh.Core.Identity.Models;


namespace Hmh.Core.Identity
{
    public class SignInManager : SignInManager<User, int>
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="userManager"/>
        /// <param name="authenticationManager"/>
        public SignInManager(UserManager<User, int> userManager, IAuthenticationManager authenticationManager)
            : base(userManager, authenticationManager)
        { }
    }
}
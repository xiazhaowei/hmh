// -----------------------------------------------------------------------
//  <copyright file="SecurityService.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2015 OSharp. All rights reserved.
//  </copyright>
//  <site>http://www.osharp.org</site>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2015-11-27 20:01</last-date>
// -----------------------------------------------------------------------

using System;

using OSharp.Core.Data;
using OSharp.Core.Security;
using Hmh.Core.Security.Models;


namespace Hmh.Core.Security
{
    /// <summary>
    /// 业务实现——安全模块
    /// </summary>
    public partial class SecurityService : ISecurityContract
    {
        /// <summary>
        /// 获取或设置 服务提供者
        /// </summary>
        public IServiceProvider ServiceProvider { get; set; }

        /// <summary>
        /// 获取或设置 功能信息仓储对象
        /// </summary>
        public IRepository<Function, Guid> FunctionRepository { private get; set; }

        /// <summary>
        /// 获取或设置 实体信息仓储对象
        /// </summary>
        public IRepository<EntityInfo, Guid> EntityInfoRepository { private get; set; }
        
        /// <summary>
        /// 获取或设置 用户功能映射信息仓储对象
        /// </summary>
        public IRepository<FunctionUserMap, int> FunctionUserMapRepository { get; set; }

        /// <summary>
        /// 获取或设置 角色数据映射信息仓储对象
        /// </summary>
        public IRepository<EntityRoleMap, int> EntityRoleMapRepository { get; set; }

        /// <summary>
        /// 获取或设置 用户数据映射信息仓储对象
        /// </summary>
        public IRepository<EntityUserMap, int> EntityUserMapRepository { get; set; }

        /// <summary>
        /// 获取或设置 安全权限管理器对象
        /// </summary>
        public SecurityManager SecurityManager { get; set; }
    }
}
// -----------------------------------------------------------------------
//  <copyright file="IdentityService.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2015 OSharp. All rights reserved.
//  </copyright>
//  <site>http://www.osharp.org</site>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2015-11-27 20:01</last-date>
// -----------------------------------------------------------------------

using OSharp.Core.Data;
using Hmh.Core.Identity;
using Hmh.Core.Identity.Dtos;
using Hmh.Core.Identity.Models;


namespace Hmh.Core.Identity
{
    /// <summary>
    /// 业务实现——身份认证模块
    /// </summary>
    public partial class IdentityService : IIdentityContract
    {
        /// <summary>
        /// 获取或设置 用户信息仓储对象
        /// </summary>
        public IRepository<User, int> UserRepository { get; set; }

        /// <summary>
        /// 获取或设置 角色信息仓储对象
        /// </summary>
        public IRepository<Role, int> RoleRepository { get; set; }

        /// <summary>
        /// 获取或设置 用户扩展信息仓储对象
        /// </summary>
        public IRepository<UserExtend, int> UserExtendRepository { get; set; }

        /// <summary>
        /// 获取或设置 用户角色映射仓储对象
        /// </summary>
        public IRepository<UserRoleMap, int> UserRoleMapRepository { get; set; }


        /// <summary>
        /// 获取或设置 收件地址信息仓储对象
        /// </summary>
        public IRepository<DeliverAddress, int> DeliverAddressRepository { get; set; }

        /// <summary>
        /// 获取或设置 银行卡信息仓储对象
        /// </summary>
        public IRepository<UserBankCard, int> UserBankCardRepository { get; set; }

        /// <summary>
        /// 获取或设置 人民币记录信息仓储对象
        /// </summary>
        public IRepository<RmbCoinTransaction, int> RmbCoinTransactionRepository { get; set; }

        /// <summary>
        /// 获取或设置 H币记录信息仓储对象
        /// </summary>
        public IRepository<HCoinTransaction, int> HCoinTransactionRepository { get; set; }

        /// <summary>
        /// 获取或设置 收藏信息仓储对象
        /// </summary>
        public IRepository<Collect, int> CollectRepository { get; set; }
    }
}
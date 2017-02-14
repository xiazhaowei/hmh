// -----------------------------------------------------------------------
//  <copyright file="IIdentityContract.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2015 OSharp. All rights reserved.
//  </copyright>
//  <site>http://www.osharp.org</site>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2015-11-27 20:01</last-date>
// -----------------------------------------------------------------------

using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

using OSharp.Core.Dependency;
using Hmh.Core.Identity.Dtos;
using Hmh.Core.Identity.Models;
using OSharp.Utility.Data;


namespace Hmh.Core.Identity
{
    /// <summary>
    /// 业务契约——身份认证模块
    /// </summary>
    public interface IIdentityContract : IScopeDependency
    {
        #region 用户信息业务

        /// <summary>
        /// 获取 用户信息查询数据集
        /// </summary>
        IQueryable<User> Users { get; }

        /// <summary>
        /// 检查用户信息信息是否存在
        /// </summary>
        /// <param name="predicate">检查谓语表达式</param>
        /// <param name="id">更新的用户信息编号</param>
        /// <returns>用户信息是否存在</returns>
        Task<bool> CheckUserExists(Expression<Func<User, bool>> predicate, int id = 0);

        /// <summary>
        /// 添加用户信息信息
        /// </summary>
        /// <param name="dtos">要添加的用户信息DTO信息</param>
        /// <returns>业务操作结果</returns>
        Task<OperationResult> CreateUsers(params UserInputDto[] dtos);

        /// <summary>
        /// 更新用户信息信息
        /// </summary>
        /// <param name="dtos">包含更新信息的用户信息DTO信息</param>
        /// <returns>业务操作结果</returns>
        Task<OperationResult> UpdateUsers(params UserInputDto[] dtos);
        /// <summary>
        /// 更新用户基本信息
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<OperationResult> UpdateUserBase(UserExtendInputDto dto);
        /// <summary>
        /// 更新用户密码
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<OperationResult> UpdateUserPassword(UserPasswordInputDto dto);
        /// <summary>
        /// 删除用户信息信息
        /// </summary>
        /// <param name="ids">要删除的用户信息编号</param>
        /// <returns>业务操作结果</returns>
        Task<OperationResult> DeleteUsers(params int[] ids);

        /// <summary>
        /// 提款申请
        /// </summary>
        /// <param name="widthdrawinfo"></param>
        /// <returns></returns>
        Task<OperationResult> Widthdraw(WidthdrawInfo widthdrawinfo);
        /// <summary>
        /// 转账
        /// </summary>
        /// <param name="transactionInfo"></param>
        /// <returns></returns>
        Task<OperationResult> Transaction(TransactionInfo transactionInfo);
        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="loginInfo">用户登录信息</param>
        /// <param name="shouldLockout">是否启用登录锁定</param>
        /// <returns>业务操作结果</returns>
        Task<OperationResult<User>> Login(LoginInfo loginInfo, bool shouldLockout);

        #endregion

        #region 角色信息业务

        /// <summary>
        /// 获取 角色信息查询数据集
        /// </summary>
        IQueryable<Role> Roles { get; }

        /// <summary>
        /// 检查角色信息信息是否存在
        /// </summary>
        /// <param name="predicate">检查谓语表达式</param>
        /// <param name="id">更新的角色信息编号</param>
        /// <returns>角色信息是否存在</returns>
        Task<bool> CheckRoleExists(Expression<Func<Role, bool>> predicate, int id = 0);

        /// <summary>
        /// 添加角色信息信息
        /// </summary>
        /// <param name="dtos">要添加的角色信息DTO信息</param>
        /// <returns>业务操作结果</returns>
        Task<OperationResult> CreateRoles(params RoleInputDto[] dtos);

        /// <summary>
        /// 更新角色信息信息
        /// </summary>
        /// <param name="dtos">包含更新信息的角色信息DTO信息</param>
        /// <returns>业务操作结果</returns>
        Task<OperationResult> UpdateRoles(params RoleInputDto[] dtos);

        /// <summary>
        /// 删除角色信息信息
        /// </summary>
        /// <param name="ids">要删除的角色信息编号</param>
        /// <returns>业务操作结果</returns>
        Task<OperationResult> DeleteRoles(params int[] ids);

        #endregion

        #region 用户角色映射信息业务

        /// <summary>
        /// 获取 用户角色映射信息查询数据集
        /// </summary>
        IQueryable<UserRoleMap> UserRoleMaps { get; }

        /// <summary>
        /// 检查用户角色映射信息信息是否存在
        /// </summary>
        /// <param name="predicate">检查谓语表达式</param>
        /// <param name="id">更新的用户角色映射信息编号</param>
        /// <returns>用户角色映射信息是否存在</returns>
        Task<bool> CheckUserRoleMapExists(Expression<Func<UserRoleMap, bool>> predicate, int id = 0);
        
        /// <summary>
        /// 添加用户角色映射信息信息
        /// </summary>
        /// <param name="dtos">要添加的用户角色映射信息DTO信息</param>
        /// <returns>业务操作结果</returns>
        Task<OperationResult> CreateUserRoleMaps(params UserRoleMapInputDto[] dtos);

        /// <summary>
        /// 更新用户角色映射信息信息
        /// </summary>
        /// <param name="dtos">包含更新信息的用户角色映射信息DTO信息</param>
        /// <returns>业务操作结果</returns>
        Task<OperationResult> UpdateUserRoleMaps(params UserRoleMapInputDto[] dtos);

        /// <summary>
        /// 由选中的用户编号设置用户拥有的角色
        /// </summary>
        /// <param name="userId">用户编号</param>
        /// <param name="roleIds">角色编号集合</param>
        /// <returns>业务操作结果</returns>
        Task<OperationResult> SetUserRoleMaps(int userId, int[] roleIds);

        /// <summary>
        /// 删除用户角色映射信息信息
        /// </summary>
        /// <param name="ids">要删除的用户角色映射信息编号</param>
        /// <returns>业务操作结果</returns>
        Task<OperationResult> DeleteUserRoleMaps(params int[] ids);

        #endregion

        #region 用户收件地址信息业务
        /// <summary>
        /// 获取 查询数据集
        /// </summary>
        IQueryable<DeliverAddress> DeliverAddresss { get; }

        /// <summary>
        /// 检查信息是否存在
        /// </summary>
        /// <param name="predicate">检查谓语表达式</param>
        /// <param name="id">更新的信息编号</param>
        /// <returns>信息是否存在</returns>
        bool CheckDeliverAddressExists(Expression<Func<DeliverAddress, bool>> predicate, int id = 0);

        /// <summary>
        /// 添加信息
        /// </summary>
        /// <param name="inputDtos">要添加的信息DTO信息</param>
        /// <returns>业务操作结果</returns>
        OperationResult AddDeliverAddresses(params DeliverAddressInputDto[] inputDtos);

        /// <summary>
        /// 更新信息
        /// </summary>
        /// <param name="inputDtos">包含更新信息的DTO信息</param>
        /// <returns>业务操作结果</returns>
        OperationResult EditDeliverAddresses(params DeliverAddressInputDto[] inputDtos);

        /// <summary>
        /// 删除信息
        /// </summary>
        /// <param name="ids">要删除的编号</param>
        /// <returns>业务操作结果</returns>
        OperationResult DeleteDeliverAddresses(params int[] ids);
        #endregion

        #region 用户收件地址信息业务
        /// <summary>
        /// 获取 查询数据集
        /// </summary>
        IQueryable<UserBankCard> UserBankCards { get; }

        /// <summary>
        /// 检查信息是否存在
        /// </summary>
        /// <param name="predicate">检查谓语表达式</param>
        /// <param name="id">更新的信息编号</param>
        /// <returns>信息是否存在</returns>
        bool CheckUserBankCardExists(Expression<Func<UserBankCard, bool>> predicate, int id = 0);

        /// <summary>
        /// 添加信息
        /// </summary>
        /// <param name="inputDtos">要添加的信息DTO信息</param>
        /// <returns>业务操作结果</returns>
        OperationResult AddUserBankCards(params UserBankCardInputDto[] inputDtos);

        /// <summary>
        /// 更新信息
        /// </summary>
        /// <param name="inputDtos">包含更新信息的DTO信息</param>
        /// <returns>业务操作结果</returns>
        OperationResult EditUserBankCards(params UserBankCardInputDto[] inputDtos);

        /// <summary>
        /// 删除信息
        /// </summary>
        /// <param name="ids">要删除的编号</param>
        /// <returns>业务操作结果</returns>
        OperationResult DeleteUserBankCards(params int[] ids);
        #endregion

        #region 人民币记录信息业务
        /// <summary>
        /// 获取 查询数据集
        /// </summary>
        IQueryable<RmbCoinTransaction> RmbCoinTransactions { get; }


        #endregion

        #region H币记录信息业务
        /// <summary>
        /// 获取 查询数据集
        /// </summary>
        IQueryable<HCoinTransaction> HCoinTransactions { get; }


        #endregion

        #region 收藏信息业务
        /// <summary>
        /// 获取 查询数据集
        /// </summary>
        IQueryable<Collect> Collects {get;}

        /// <summary>
        /// 检查信息是否存在
        /// </summary>
        /// <param name="predicate">检查谓语表达式</param>
        /// <param name="id">更新的信息编号</param>
        /// <returns>信息是否存在</returns>
        bool CollectCardExists(Expression<Func<Collect, bool>> predicate, int id = 0);



        /// <summary>
        /// 添加信息
        /// </summary>
        /// <param name="inputDtos">要添加的信息DTO信息</param>
        /// <returns>业务操作结果</returns>
        OperationResult AddCollects(params CollectInputDto[] inputDtos);



        /// <summary>
        /// 删除信息
        /// </summary>
        /// <param name="ids">要删除的信息编号</param>
        /// <returns>业务操作结果</returns>
        OperationResult DeleteCollects(params int[] ids);
       

        #endregion
    }
}
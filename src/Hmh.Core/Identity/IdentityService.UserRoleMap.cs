// -----------------------------------------------------------------------
//  <copyright file="IdentityService.UserRoleMap.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2016 OSharp. All rights reserved.
//  </copyright>
//  <site>http://www.osharp.org</site>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2016-03-04 9:04</last-date>
// -----------------------------------------------------------------------

using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

using Hmh.Core.Identity.Dtos;
using Hmh.Core.Identity.Models;
using OSharp.Utility.Data;
using OSharp.Utility.Extensions;


namespace Hmh.Core.Identity
{
    public partial class IdentityService
    {
        #region Implementation of IIdentityContract

        /// <summary>
        /// 获取 用户角色映射信息查询数据集
        /// </summary>
        public IQueryable<UserRoleMap> UserRoleMaps
        {
            get { return UserRoleMapRepository.Entities; }
        }

        /// <summary>
        /// 检查用户角色映射信息信息是否存在
        /// </summary>
        /// <param name="predicate">检查谓语表达式</param>
        /// <param name="id">更新的用户角色映射信息编号</param>
        /// <returns>用户角色映射信息是否存在</returns>
        public Task<bool> CheckUserRoleMapExists(Expression<Func<UserRoleMap, bool>> predicate, int id = 0)
        {
            return UserRoleMapRepository.CheckExistsAsync(predicate, id);
        }
        
        /// <summary>
        /// 添加用户角色映射信息信息
        /// </summary>
        /// <param name="dtos">要添加的用户角色映射信息DTO信息</param>
        /// <returns>业务操作结果</returns>
        public async Task<OperationResult> CreateUserRoleMaps(params UserRoleMapInputDto[] dtos)
        {
            UserRoleMapRepository.UnitOfWork.TransactionEnabled = true;
            foreach (UserRoleMapInputDto dto in dtos)
            {
                OperationResult result = await UserManager.CreateUserRoleMapAsync(dto);
                if (!result.Successed)
                {
                    return result;
                }
            }
            return await UserRoleMapRepository.UnitOfWork.SaveChangesAsync() > 0
                ? new OperationResult(OperationResultType.Success, "用户角色映射信息创建成功")
                : OperationResult.NoChanged;
        }

        /// <summary>
        /// 更新用户角色映射信息信息
        /// </summary>
        /// <param name="dtos">包含更新信息的用户角色映射信息DTO信息</param>
        /// <returns>业务操作结果</returns>
        public async Task<OperationResult> UpdateUserRoleMaps(params UserRoleMapInputDto[] dtos)
        {
            UserRoleMapRepository.UnitOfWork.TransactionEnabled = true;
            foreach (UserRoleMapInputDto dto in dtos)
            {
                OperationResult result = await UserManager.UpdateUserRoleMapAsync(dto);
                if (!result.Successed)
                {
                    return result;
                }
            }
            return await UserRoleMapRepository.UnitOfWork.SaveChangesAsync() > 0
                ? new OperationResult(OperationResultType.Success, "用户角色映射信息更新成功")
                : OperationResult.NoChanged;
        }

        /// <summary>
        /// 由选中的用户编号设置用户拥有的角色
        /// </summary>
        /// <param name="userId">用户编号</param>
        /// <param name="roleIds">角色编号集合</param>
        /// <returns>业务操作结果</returns>
        public async Task<OperationResult> SetUserRoleMaps(int userId, int[] roleIds)
        {
            User user = await UserManager.FindByIdAsync(userId);
            if (user == null)
            {
                return new OperationResult(OperationResultType.QueryNull, $"编号为“{userId}”的用户信息不存在");
            }
            int[] existRoleIds = UserRoleMapRepository.Entities.Where(m => m.User.Id == userId).Select(m => m.Role.Id).Distinct().ToArray();
            int[] addRoleIds = roleIds.Except(existRoleIds).ToArray();
            int[] removeRoleIds = existRoleIds.Except(roleIds).ToArray();
            UserRoleMapRepository.UnitOfWork.TransactionEnabled = true;
            foreach (int roleId in addRoleIds)
            {
                UserRoleMapInputDto dto = new UserRoleMapInputDto() { UserId = userId, RoleId = roleId };
                OperationResult result = await UserManager.CreateUserRoleMapAsync(dto);
                if (!result.Successed)
                {
                    return result;
                }
            }
            int[] removeMapIds = UserRoleMapRepository.Entities.Where(m => m.User.Id == userId && removeRoleIds.Contains(m.Role.Id))
                .Select(m => m.Id).ToArray();
            foreach (int mapId in removeMapIds)
            {
                OperationResult result = await UserManager.DeleteUserRoleMapAsync(mapId);
                if (!result.Successed)
                {
                    return result;
                }
            }
            return await UserRoleMapRepository.UnitOfWork.SaveChangesAsync() > 0
                ? new OperationResult(OperationResultType.Success,
                    "用户设置角色操作成功，共添加 {0} 个角色，删除 {1} 个角色".FormatWith(addRoleIds.Length, removeMapIds.Length))
                : new OperationResult(OperationResultType.NoChanged);
        }

        /// <summary>
        /// 删除用户角色映射信息信息
        /// </summary>
        /// <param name="ids">要删除的用户角色映射信息编号</param>
        /// <returns>业务操作结果</returns>
        public async Task<OperationResult> DeleteUserRoleMaps(params int[] ids)
        {
            UserRoleMapRepository.UnitOfWork.TransactionEnabled = true;
            foreach (int id in ids)
            {
                OperationResult result = await UserManager.DeleteUserRoleMapAsync(id);
                if (!result.Successed)
                {
                    return result;
                }
            }
            return await UserRoleMapRepository.UnitOfWork.SaveChangesAsync() > 0
                ? new OperationResult(OperationResultType.Success, "用户角色映射信息删除成功")
                : OperationResult.NoChanged;
        }

        #endregion
    }
}
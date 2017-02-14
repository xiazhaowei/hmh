// -----------------------------------------------------------------------
//  <copyright file="IdentityService.Role.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2015 OSharp. All rights reserved.
//  </copyright>
//  <site>http://www.osharp.org</site>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2015-12-04 17:50</last-date>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

using Microsoft.AspNet.Identity;

using OSharp.Core.Identity;
using OSharp.Core.Mapping;
using Hmh.Core.Identity.Dtos;
using Hmh.Core.Identity.Models;
using OSharp.Utility.Data;
using OSharp.Utility.Extensions;


namespace Hmh.Core.Identity
{
    public partial class IdentityService
    {
        /// <summary>
        /// 获取或设置 角色管理器
        /// </summary>
        public RoleManager RoleManager { get; set; }

        #region Implementation of IIdentityContract

        /// <summary>
        /// 获取 角色信息查询数据集
        /// </summary>
        public IQueryable<Role> Roles
        {
            get { return RoleRepository.Entities; }
        }

        /// <summary>
        /// 检查角色信息信息是否存在
        /// </summary>
        /// <param name="predicate">检查谓语表达式</param>
        /// <param name="id">更新的角色信息编号</param>
        /// <returns>角色信息是否存在</returns>
        public Task<bool> CheckRoleExists(Expression<Func<Role, bool>> predicate, int id = 0)
        {
            return RoleRepository.CheckExistsAsync(predicate, id);
        }

        /// <summary>
        /// 添加角色信息信息
        /// </summary>
        /// <param name="dtos">要添加的角色信息DTO信息</param>
        /// <returns>业务操作结果</returns>
        public async Task<OperationResult>CreateRoles(params RoleInputDto[] dtos)
        {
            RoleRepository.UnitOfWork.TransactionEnabled = true;
            List<string>names = new List<string>();
            foreach (RoleInputDto dto in dtos)
            {
                Role role = dto.MapTo<Role>();
                IdentityResult result = await RoleManager.CreateAsync(role);
                if (!result.Succeeded)
                {
                    return result.ToOperationResult();
                }
                names.Add(role.Name);
            }
            return await RoleRepository.UnitOfWork.SaveChangesAsync() > 0
                ? new OperationResult(OperationResultType.Success, $"角色“{names.ExpandAndToString()}”创建成功。")
                : OperationResult.NoChanged;
        }

        /// <summary>
        /// 更新角色信息信息
        /// </summary>
        /// <param name="dtos">包含更新信息的角色信息DTO信息</param>
        /// <returns>业务操作结果</returns>
        public async Task<OperationResult>UpdateRoles(params RoleInputDto[] dtos)
        {
            RoleRepository.UnitOfWork.TransactionEnabled = true;
            List<string> names = new List<string>();
            foreach (RoleInputDto dto in dtos)
            {
                Role role = await RoleManager.FindByIdAsync(dto.Id);
                if (role == null)
                {
                    return new OperationResult(OperationResultType.QueryNull);
                }
                role = dto.MapTo(role);
                IdentityResult result = await RoleManager.UpdateAsync(role);
                if (!result.Succeeded)
                {
                    return result.ToOperationResult();
                }
                names.Add(role.Name);
            }
            return await RoleRepository.UnitOfWork.SaveChangesAsync() > 0
                ? new OperationResult(OperationResultType.Success, $"角色“{names.ExpandAndToString()}”更新成功。")
                : OperationResult.NoChanged;
        }

        /// <summary>
        /// 删除角色信息信息
        /// </summary>
        /// <param name="ids">要删除的角色信息编号</param>
        /// <returns>业务操作结果</returns>
        public async Task<OperationResult> DeleteRoles(params int[] ids)
        {
            RoleRepository.UnitOfWork.TransactionEnabled = true;
            List<string> names = new List<string>();
            foreach (int id in ids)
            {
                Role role = await RoleManager.FindByIdAsync(id);
                if (role.IsSystem)
                {
                    return new OperationResult(OperationResultType.Error, "系统角色不能删除。");
                }
                IdentityResult result = await RoleManager.DeleteAsync(role);
                if (!result.Succeeded)
                {
                    return result.ToOperationResult();
                }
                names.Add(role.Name);
            }
            return await RoleRepository.UnitOfWork.SaveChangesAsync() > 0
                ? new OperationResult(OperationResultType.Success, $"角色“{names.ExpandAndToString()}”删除成功。")
                : OperationResult.NoChanged;
        }

        #endregion
    }
}
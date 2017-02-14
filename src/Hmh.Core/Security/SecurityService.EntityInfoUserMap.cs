// -----------------------------------------------------------------------
//  <copyright file="SecurityService.EntityInfoUserMap.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2016 OSharp. All rights reserved.
//  </copyright>
//  <site>http://www.osharp.org</site>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2016-03-04 19:50</last-date>
// -----------------------------------------------------------------------

using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

using Hmh.Core.Security.Dtos;
using Hmh.Core.Security.Models;
using OSharp.Utility.Data;


namespace Hmh.Core.Security
{
    public partial class SecurityService
    {
        #region Implementation of ISecurityContract

        /// <summary>
        /// 获取 用户功能映射信息查询数据集
        /// </summary>
        public IQueryable<FunctionUserMap> FunctionUserMaps
        {
            get { return FunctionUserMapRepository.Entities; }
        }

        /// <summary>
        /// 检查用户功能映射信息信息是否存在
        /// </summary>
        /// <param name="predicate">检查谓语表达式</param>
        /// <param name="id">更新的用户功能映射信息编号</param>
        /// <returns>用户功能映射信息是否存在</returns>
        public Task<bool> CheckFunctionUserMapExists(Expression<Func<FunctionUserMap, bool>> predicate, int id = 0)
        {
            return FunctionUserMapRepository.CheckExistsAsync(predicate, id);
        }

        /// <summary>
        /// 添加用户功能映射信息信息
        /// </summary>
        /// <param name="dtos">要添加的用户功能映射信息DTO信息</param>
        /// <returns>业务操作结果</returns>
        public async Task<OperationResult> CreateFunctionUserMaps(params FunctionUserMapInputDto[] dtos)
        {
            FunctionUserMapRepository.UnitOfWork.TransactionEnabled = true;
            foreach (FunctionUserMapInputDto dto in dtos)
            {
                OperationResult result = await SecurityManager.CreateFunctionUserMapAsync(dto);
                if (!result.Successed)
                {
                    return result;
                }
            }
            return await FunctionUserMapRepository.UnitOfWork.SaveChangesAsync() > 0
                ? new OperationResult(OperationResultType.Success, $"{dtos.Length} 个用户-功能映射信息创建成功")
                : OperationResult.NoChanged;
        }

        /// <summary>
        /// 更新用户功能映射信息信息
        /// </summary>
        /// <param name="dtos">包含更新信息的用户功能映射信息DTO信息</param>
        /// <returns>业务操作结果</returns>
        public async Task<OperationResult> UpdateFunctionUserMaps(params FunctionUserMapInputDto[] dtos)
        {
            FunctionUserMapRepository.UnitOfWork.TransactionEnabled = true;
            foreach (FunctionUserMapInputDto dto in dtos)
            {
                OperationResult result = await SecurityManager.UpdateFunctionUserMapAsync(dto);
                if (!result.Successed)
                {
                    return result;
                }
            }
            return await FunctionUserMapRepository.UnitOfWork.SaveChangesAsync() > 0
                ? new OperationResult(OperationResultType.Success, $"{dtos.Length} 个用户-功能映射信息更新成功")
                : OperationResult.NoChanged;
        }

        /// <summary>
        /// 删除用户功能映射信息信息
        /// </summary>
        /// <param name="ids">要删除的用户功能映射信息编号</param>
        /// <returns>业务操作结果</returns>
        public async Task<OperationResult> DeleteFunctionUserMaps(params int[] ids)
        {
            FunctionUserMapRepository.UnitOfWork.TransactionEnabled = true;
            foreach (int id in ids)
            {
                OperationResult result = await SecurityManager.DeleteFunctionUserMapAsync(id);
                if (!result.Successed)
                {
                    return result;
                }
            }
            return await FunctionUserMapRepository.UnitOfWork.SaveChangesAsync() > 0
                ? new OperationResult(OperationResultType.Success, $"{ids.Length} 个用户-功能映射信息删除成功")
                : OperationResult.NoChanged;
        }

        #endregion
    }
}
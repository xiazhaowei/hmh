// -----------------------------------------------------------------------
//  <copyright file="ISecurityContract.cs" company="OSharp开源团队">
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
using OSharp.Core.Security;
using Hmh.Core.Security.Dtos;
using Hmh.Core.Security.Models;
using OSharp.Utility.Data;


namespace Hmh.Core.Security
{
    /// <summary>
    /// 业务契约——安全模块
    /// </summary>
    public interface ISecurityContract : IScopeDependency
    {
        #region 功能信息业务

        /// <summary>
        /// 获取 功能信息查询数据集
        /// </summary>
        IQueryable<Function> Functions { get; }

        /// <summary>
        /// 检查功能信息信息是否存在
        /// </summary>
        /// <param name="predicate">检查谓语表达式</param>
        /// <param name="id">更新的功能信息编号</param>
        /// <returns>功能信息是否存在</returns>
        Task<bool> CheckFunctionExists(Expression<Func<Function, bool>> predicate, Guid id = default(Guid));

        /// <summary>
        /// 添加功能信息信息
        /// </summary>
        /// <param name="dtos">要添加的功能信息DTO信息</param>
        /// <returns>业务操作结果</returns>
        Task<OperationResult> CreateFunctions(params FunctionInputDto[] dtos);

        /// <summary>
        /// 更新功能信息信息
        /// </summary>
        /// <param name="dtos">包含更新信息的功能信息DTO信息</param>
        /// <returns>业务操作结果</returns>
        Task<OperationResult> UpdateFunctions(params FunctionInputDto[] dtos);

        /// <summary>
        /// 删除功能信息信息
        /// </summary>
        /// <param name="ids">要删除的功能信息编号</param>
        /// <returns>业务操作结果</returns>
        Task<OperationResult> DeleteFunctions(params Guid[] ids);

        #endregion

        #region 实体数据信息业务

        /// <summary>
        /// 获取 实体数据信息查询数据集
        /// </summary>
        IQueryable<EntityInfo> EntityInfos { get; }

        /// <summary>
        /// 更新实体数据信息信息
        /// </summary>
        /// <param name="dtos">包含更新信息的实体数据信息DTO信息</param>
        /// <returns>业务操作结果</returns>
        Task<OperationResult> UpdateEntityInfos(params EntityInfoInputDto[] dtos);

        #endregion
        
        #region 用户功能映射信息业务

        /// <summary>
        /// 获取 用户功能映射信息查询数据集
        /// </summary>
        IQueryable<FunctionUserMap> FunctionUserMaps { get; }

        /// <summary>
        /// 检查用户功能映射信息信息是否存在
        /// </summary>
        /// <param name="predicate">检查谓语表达式</param>
        /// <param name="id">更新的用户功能映射信息编号</param>
        /// <returns>用户功能映射信息是否存在</returns>
        Task<bool> CheckFunctionUserMapExists(Expression<Func<FunctionUserMap, bool>> predicate, int id = 0);

        /// <summary>
        /// 添加用户功能映射信息信息
        /// </summary>
        /// <param name="dtos">要添加的用户功能映射信息DTO信息</param>
        /// <returns>业务操作结果</returns>
        Task<OperationResult> CreateFunctionUserMaps(params FunctionUserMapInputDto[] dtos);

        /// <summary>
        /// 更新用户功能映射信息信息
        /// </summary>
        /// <param name="dtos">包含更新信息的用户功能映射信息DTO信息</param>
        /// <returns>业务操作结果</returns>
        Task<OperationResult> UpdateFunctionUserMaps(params FunctionUserMapInputDto[] dtos);

        /// <summary>
        /// 删除用户功能映射信息信息
        /// </summary>
        /// <param name="ids">要删除的用户功能映射信息编号</param>
        /// <returns>业务操作结果</returns>
        Task<OperationResult> DeleteFunctionUserMaps(params int[] ids);

        #endregion

    }
}
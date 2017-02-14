// -----------------------------------------------------------------------
//  <copyright file="SecurityService.Function.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2015 OSharp. All rights reserved.
//  </copyright>
//  <site>http://www.osharp.org</site>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2015-11-27 20:01</last-date>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

using OSharp.Core.Dependency;
using OSharp.Core.Mapping;
using OSharp.Core.Security;
using Hmh.Core.Security.Dtos;
using OSharp.Utility;
using OSharp.Utility.Data;
using OSharp.Utility.Extensions;


namespace Hmh.Core.Security
{
    public partial class SecurityService
    {
        /// <summary>
        /// 获取 功能信息查询数据集
        /// </summary>
        public IQueryable<Function> Functions
        {
            get { return FunctionRepository.Entities; }
        }

        /// <summary>
        /// 检查功能信息信息是否存在
        /// </summary>
        /// <param name="predicate">检查谓语表达式</param>
        /// <param name="id">更新的功能信息编号</param>
        /// <returns>功能信息是否存在</returns>
        public Task<bool> CheckFunctionExists(Expression<Func<Function, bool>> predicate, Guid id = default(Guid))
        {
            return FunctionRepository.CheckExistsAsync(predicate, id);
        }

        /// <summary>
        /// 添加功能信息信息
        /// </summary>
        /// <param name="dtos">要添加的功能信息DTO信息</param>
        /// <returns>业务操作结果</returns>
        public async Task<OperationResult> CreateFunctions(params FunctionInputDto[] dtos)
        {
            dtos.CheckNotNull(nameof(dtos));
            OperationResult result = await FunctionRepository.InsertAsync(dtos,
                async dto =>
                {
                    if (dto.Url.IsMissing())
                    {
                        throw new Exception("自定义功能的URL不能为空");
                    }
                    if (await FunctionRepository.CheckExistsAsync(m => m.Name == dto.Name))
                    {
                        throw new Exception($"名称为“{dto.Name}”的功能信息已存在");
                    }
                    if (dto.Url == null
                        && await
                            FunctionRepository.CheckExistsAsync(m => m.Area == dto.Area && m.Controller == dto.Controller && m.Action == dto.Action))
                    {
                        throw new Exception($"区域“{dto.Area}”控制器“{dto.Controller}”方法“{dto.Action}”的功能信息已存在");
                    }
                },
                (dto, entity) =>
                {
                    entity.IsCustom = true;
                    if (entity.Url.IsMissing())
                    {
                        entity.Url = null;
                    }
                    return Task.FromResult(entity);
                });
            if (result.Successed)
            {
                IFunctionHandler handler = ServiceProvider.GetService<IFunctionHandler>();
                handler.RefreshCache();
            }
            return result;
        }

        /// <summary>
        /// 更新功能信息信息
        /// </summary>
        /// <param name="dtos">包含更新信息的功能信息DTO信息</param>
        /// <returns>业务操作结果</returns>
        public async Task<OperationResult> UpdateFunctions(params FunctionInputDto[] dtos)
        {
            dtos.CheckNotNull(nameof(dtos));
            List<string> names = new List<string>();
            FunctionRepository.UnitOfWork.TransactionEnabled = true;
            foreach (FunctionInputDto dto in dtos)
            {
                if (await FunctionRepository.CheckExistsAsync(m => m.Name == dto.Name, dto.Id))
                {
                    return new OperationResult(OperationResultType.Error, $"名称为“{dto.Name}”的功能信息已存在");
                }
                Function entity = await FunctionRepository.GetByKeyAsync(dto.Id);
                if (entity == null)
                {
                    return new OperationResult(OperationResultType.QueryNull);
                }
                FunctionType oldType = entity.FunctionType;
                if (dto.DataLogEnabled && !dto.OperateLogEnabled && !entity.OperateLogEnabled && !entity.DataLogEnabled)
                {
                    dto.OperateLogEnabled = true;
                }
                else if (!dto.OperateLogEnabled && dto.DataLogEnabled && entity.OperateLogEnabled && entity.DataLogEnabled)
                {
                    dto.DataLogEnabled = false;
                }
                entity = dto.MapTo(entity);
                if (entity.Url.IsNullOrEmpty())
                {
                    entity.Url = null;
                }
                if (oldType != entity.FunctionType)
                {
                    entity.IsTypeChanged = true;
                }
                await FunctionRepository.UpdateAsync(entity);
                names.Add(entity.Name);
            }
            int count = await FunctionRepository.UnitOfWork.SaveChangesAsync();
            OperationResult result = count > 0
                ? new OperationResult(OperationResultType.Success, $"功能“{names.ExpandAndToString()}”更新成功")
                : new OperationResult(OperationResultType.NoChanged);
            if (result.ResultType == OperationResultType.Success)
            {
                IFunctionHandler handler = ServiceProvider.GetService<IFunctionHandler>();
                handler.RefreshCache();
            }
            return result;
        }

        /// <summary>
        /// 删除功能信息信息
        /// </summary>
        /// <param name="ids">要删除的功能信息编号</param>
        /// <returns>业务操作结果</returns>
        public async Task<OperationResult> DeleteFunctions(params Guid[] ids)
        {
            ids.CheckNotNull(nameof(ids));
            List<string> names = new List<string>();
            FunctionRepository.UnitOfWork.TransactionEnabled = true;
            foreach (Guid id in ids)
            {
                Function entity = await FunctionRepository.GetByKeyAsync(id);
                if (entity == null) 
                {
                    return new OperationResult(OperationResultType.QueryNull);
                }
                await FunctionRepository.DeleteAsync(entity);
                names.Add(entity.Name);
            }
            int count = await FunctionRepository.UnitOfWork.SaveChangesAsync();
            OperationResult result = count > 0
                ? new OperationResult(OperationResultType.Success, $"功能“{names.ExpandAndToString()}”删除成功")
                : new OperationResult(OperationResultType.NoChanged);
            if (result.ResultType == OperationResultType.Success)
            {
                IFunctionHandler handler = ServiceProvider.GetService<IFunctionHandler>();
                handler.RefreshCache();
            }
            return result;
        }
    }
}
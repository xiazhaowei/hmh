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

        #region Implementation of IIdentityContract
        /// <summary>
        /// 获取 查询数据集
        /// </summary>
        public IQueryable<UserBankCard> UserBankCards
        {
            get { return UserBankCardRepository.Entities; }
        }

        /// <summary>
        /// 检查信息是否存在
        /// </summary>
        /// <param name="predicate">检查谓语表达式</param>
        /// <param name="id">更新的信息编号</param>
        /// <returns>信息是否存在</returns>
        public bool CheckUserBankCardExists(Expression<Func<UserBankCard, bool>> predicate, int id = 0)
        {
            return UserBankCardRepository.CheckExists(predicate, id);
        }


        /// <summary>
        /// 添加信息
        /// </summary>
        /// <param name="inputDtos">要添加的信息DTO信息</param>
        /// <returns>业务操作结果</returns>
        public OperationResult AddUserBankCards(params UserBankCardInputDto[] inputDtos)
        {
            return UserBankCardRepository.Insert(inputDtos,
                dto =>
                {                    
                },
                (dto, entity) =>
                {
                    if(dto.UserId.HasValue && dto.UserId>0)
                    {
                        User user = UserRepository.GetByKey(dto.UserId.Value);
                        if(user==null)
                        {
                            throw new Exception("未找到用户");
                        }
                        entity.User = user;
                        user.BankCards.Add(entity);
                    }
                    return entity;
                });
        }

        /// <summary>
        /// 更新信息
        /// </summary>
        /// <param name="inputDtos">包含更新信息DTO信息</param>
        /// <returns>业务操作结果</returns>
        public OperationResult EditUserBankCards(params UserBankCardInputDto[] inputDtos)
        {
            return UserBankCardRepository.Update(inputDtos,
                (dto, entity) =>
                {                    
                },
                (dto, entity) =>
                {
                    return entity;
                });

        }
        /// <summary>
        /// 删除信息
        /// </summary>
        /// <param name="ids">要删除的信息编号</param>
        /// <returns>业务操作结果</returns>
        public OperationResult DeleteUserBankCards(params int[] ids)
        {
            return UserBankCardRepository.Delete(ids);
        }

        #endregion
    }
}
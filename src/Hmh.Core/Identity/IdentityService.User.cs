// -----------------------------------------------------------------------
//  <copyright file="IdentityService.User.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2015 OSharp. All rights reserved.
//  </copyright>
//  <site>http://www.osharp.org</site>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2015-12-04 17:14</last-date>
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
        /// 获取或设置 用户管理器
        /// </summary>
        public UserManager UserManager { get; set; }
        
        #region Implementation of IIdentityContract

        /// <summary>
        /// 获取 用户信息查询数据集
        /// </summary>
        public IQueryable<User> Users
        {
            get { return UserRepository.Entities; }
        }

        /// <summary>
        /// 检查用户信息信息是否存在
        /// </summary>
        /// <param name="predicate">检查谓语表达式</param>
        /// <param name="id">更新的用户信息编号</param>
        /// <returns>用户信息是否存在</returns>
        public Task<bool> CheckUserExists(Expression<Func<User, bool>> predicate, int id = 0)
        {
            return UserRepository.CheckExistsAsync(predicate, id);
        }

        /// <summary>
        /// 添加用户信息信息
        /// </summary>
        /// <param name="dtos">要添加的用户信息DTO信息</param>
        /// <returns>业务操作结果</returns>
        public async Task<OperationResult>CreateUsers(params UserInputDto[] dtos)
        {
            UserRepository.UnitOfWork.TransactionEnabled = true;
            List<string> names = new List<string>();
            foreach (UserInputDto dto in dtos)
            {
                User user = dto.MapTo<User>();                
                //推荐人
                if (dto.RecommendId > 0)
                {
                    User recommenduser = await UserManager.FindByIdAsync(dto.RecommendId);
                    if (recommenduser == null)
                    {
                        return new OperationResult(OperationResultType.QueryNull, "推荐人不存在");
                    }
                    user.Recommend = recommenduser;                    
                }
                                
                //用户扩展信息
                user.UserExtend = new UserExtend() {
                    User =user,
                    HCoin = 0,
                    RmbCoin = 0,
                    Birthday=DateTime.Now
                };


                IdentityResult result = dto.Password.IsMissing()
                    ? await UserManager.CreateAsync(user)
                    : await UserManager.CreateAsync(user, dto.Password);
                if (!result.Succeeded)
                {
                    return result.ToOperationResult();
                }
                names.Add(user.NickName);
            }
            return await UserRepository.UnitOfWork.SaveChangesAsync() > 0
                ? new OperationResult(OperationResultType.Success, $"用户“{names.ExpandAndToString()}”创建成功")
                : OperationResult.NoChanged;
        }

        /// <summary>
        /// 更新用户信息信息
        /// </summary>
        /// <param name="dtos">包含更新信息的用户信息DTO信息</param>
        /// <returns>业务操作结果</returns>
        public async Task<OperationResult>UpdateUsers(params UserInputDto[] dtos)
        {
            UserRepository.UnitOfWork.TransactionEnabled = true;
            List<string> names = new List<string>();
            foreach (UserInputDto dto in dtos)
            {
                User user = await UserManager.FindByIdAsync(dto.Id);
                if (user == null)
                {
                    return new OperationResult(OperationResultType.QueryNull);
                }
                user = dto.MapTo(user);
                IdentityResult result;
                if (!dto.Password.IsMissing())
                {
                    result = await UserManager.PasswordValidator.ValidateAsync(dto.Password);
                    if (!result.Succeeded)
                    {
                        return result.ToOperationResult();
                    }
                    user.PasswordHash = UserManager.PasswordHasher.HashPassword(dto.Password);
                }
                result = await UserManager.UpdateAsync(user);
                if (!result.Succeeded)
                {
                    return result.ToOperationResult();
                }
                names.Add(dto.NickName);
            }
            return await UserRepository.UnitOfWork.SaveChangesAsync() > 0
                ? new OperationResult(OperationResultType.Success, $"用户“{names.ExpandAndToString()}”更新成功")
                : OperationResult.NoChanged;
        }

        /// <summary>
        /// 更新用户基本信息
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task<OperationResult> UpdateUserBase(UserExtendInputDto dto)
        {
            UserRepository.UnitOfWork.TransactionEnabled = true;

            User user = await UserManager.FindByIdAsync(dto.Id);
            if(user==null)
            {
                return new OperationResult(OperationResultType.QueryNull);
            }
            user.NickName = dto.NickName;
            user.PhoneNumber = dto.PhoneNumber;
            user.UserExtend.Birthday = dto.Birthday;
            user.UserExtend.Sex = dto.Sex;

            IdentityResult result;
            result = await UserManager.UpdateAsync(user);
            if(!result.Succeeded)
            {
                return result.ToOperationResult();
            }
            return await UserRepository.UnitOfWork.SaveChangesAsync() > 0
                    ? new OperationResult(OperationResultType.Success, $"用户“{dto.NickName}”更新成功")
                    : OperationResult.NoChanged;
        }

        /// <summary>
        /// 更新用户密码
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task<OperationResult> UpdateUserPassword(UserPasswordInputDto dto)
        {
            UserRepository.UnitOfWork.TransactionEnabled = true;
            User user = await UserManager.FindByIdAsync(dto.Id);
            if (user == null)
            {
                return new OperationResult(OperationResultType.QueryNull);
            }
            IdentityResult result;
            if (!dto.Password.IsMissing())
            {
                result = await UserManager.PasswordValidator.ValidateAsync(dto.Password);
                if (!result.Succeeded)
                {
                    return result.ToOperationResult();
                }
                user.PasswordHash = UserManager.PasswordHasher.HashPassword(dto.Password);
            }
            result = await UserManager.UpdateAsync(user);
            if (!result.Succeeded)
            {
                return result.ToOperationResult();
            }

            return await UserRepository.UnitOfWork.SaveChangesAsync() > 0
                ? new OperationResult(OperationResultType.Success, $"用户密码更新成功")
                : OperationResult.NoChanged;
        }

        /// <summary>
        /// 删除用户信息信息
        /// </summary>
        /// <param name="ids">要删除的用户信息编号</param>
        /// <returns>业务操作结果</returns>
        public Task<OperationResult> DeleteUsers(params int[] ids)
        {
            return UserRepository.DeleteAsync(ids,
                null,
                async entity =>
                {
                    await UserExtendRepository.DeleteAsync(entity.UserExtend);
                    return entity;
                });
        }

        /// <summary>
        /// 提款申请
        /// </summary>
        /// <param name="widthdrawinfo"></param>
        /// <returns></returns>
        public async Task<OperationResult> Widthdraw(WidthdrawInfo widthdrawInfo)
        {
            UserRepository.UnitOfWork.TransactionEnabled = true;
            //验证用户
            User user = await UserManager.FindByIdAsync(widthdrawInfo.UserId);
            if (user == null)
            {
                return new OperationResult(OperationResultType.QueryNull);
            }
            //验证密码
            if (!await UserManager.CheckPasswordAsync(user, widthdrawInfo.Password))
            {                
                return new OperationResult(OperationResultType.Error, "密码错误");
            }
            //验证资金
            if(user.UserExtend.RmbCoin<widthdrawInfo.Amount)
            {
                return new OperationResult(OperationResultType.Error, "提现金额大于账户余额");
            }


            user.UserExtend.RmbCoin -= widthdrawInfo.Amount;

            //提款记录
            RmbCoinTransaction transaction = new RmbCoinTransaction() {
                Amount= -widthdrawInfo.Amount,
                Type=RmbTransactionType.Withdraw,
                State=TransactionState.UnConfirmed,
                Direction=TransactionDirection.Expend,
                RealAmount= -widthdrawInfo.Amount,
                CreatedTime=DateTime.Now,
                StreamId= DateTime.Now.ToString("yyMMddHHmmss") + new Random().GetRandomNumberString(4),//生成序列号
                Remark="提现",
                OtherSideInfo= widthdrawInfo.BankName,
                Fee=0,
                Preferential=0,
                User=user
            };
            await RmbCoinTransactionRepository.InsertAsync(transaction);

            user.RmbCoinTransactions.Add(transaction);

            IdentityResult result;
            result = await UserManager.UpdateAsync(user);
            if (!result.Succeeded)
            {
                return result.ToOperationResult();
            }
            return await UserRepository.UnitOfWork.SaveChangesAsync() > 0
                    ? new OperationResult(OperationResultType.Success, $"提款申请成功")
                    : OperationResult.NoChanged;

        }

        /// <summary>
        /// 转账
        /// </summary>
        /// <param name="transactionInfo"></param>
        /// <returns></returns>
        public async Task<OperationResult> Transaction(TransactionInfo transactionInfo)
        {
            UserRepository.UnitOfWork.TransactionEnabled = true;
            //验证用户
            User user = await UserManager.FindByIdAsync(transactionInfo.UserId);
            if (user == null)
            {
                return new OperationResult(OperationResultType.QueryNull);
            }
            //验证收款方账号
            User otherSizeUser = await UserManager.FindByNameAsync(transactionInfo.OtherSizeUserName);
            if(otherSizeUser==null)
            {
                return new OperationResult(OperationResultType.QueryNull);
            }
            //验证密码
            if (!await UserManager.CheckPasswordAsync(user, transactionInfo.Password))
            {
                return new OperationResult(OperationResultType.Error, "密码错误");
            }
            //验证资金
            if (user.UserExtend.RmbCoin < transactionInfo.Amount)
            {
                return new OperationResult(OperationResultType.Error, "提现金额大于账户余额");
            }


            user.UserExtend.RmbCoin -= transactionInfo.Amount;
            otherSizeUser.UserExtend.RmbCoin += transactionInfo.Amount;

            string streamId = DateTime.Now.ToString("yyMMddHHmmss") + new Random().GetRandomNumberString(4);//生成序列号
            //付款人转账记录
            RmbCoinTransaction transaction = new RmbCoinTransaction()
            {
                Amount = -transactionInfo.Amount,
                Type = RmbTransactionType.Transaction,
                State = TransactionState.Success,
                Direction = TransactionDirection.Expend,
                RealAmount = -transactionInfo.Amount,
                CreatedTime = DateTime.Now,
                StreamId = streamId,
                Remark = "转账",
                OtherSideInfo = transactionInfo.OtherSizeUserName,
                Fee = 0,
                Preferential = 0,
                User = user
            };
            await RmbCoinTransactionRepository.InsertAsync(transaction);
            user.RmbCoinTransactions.Add(transaction);

            //收款人转账记录
            RmbCoinTransaction transaction2 = new RmbCoinTransaction() {
                Amount = transactionInfo.Amount,
                Type = RmbTransactionType.Transaction,
                State = TransactionState.Success,
                Direction = TransactionDirection.InCome,
                RealAmount = transactionInfo.Amount,
                CreatedTime = DateTime.Now,
                StreamId = streamId,
                Remark = "转账",
                OtherSideInfo = user.UserName,
                Fee = 0,
                Preferential = 0,
                User = otherSizeUser
            };
            await RmbCoinTransactionRepository.InsertAsync(transaction2);
            otherSizeUser.RmbCoinTransactions.Add(transaction2);

            IdentityResult result;
            result = await UserManager.UpdateAsync(user);
            result = await UserManager.UpdateAsync(otherSizeUser);
            if (!result.Succeeded)
            {
                return result.ToOperationResult();
            }
            return await UserRepository.UnitOfWork.SaveChangesAsync() > 0
                    ? new OperationResult(OperationResultType.Success, $"转账成功")
                    : OperationResult.NoChanged;

        }

        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="loginInfo">用户登录信息</param>
        /// <param name="shouldLockout">是否启用登录锁定</param>
        /// <returns>业务操作结果</returns>
        public async Task<OperationResult<User>> Login(LoginInfo loginInfo, bool shouldLockout)
        {
            User user = await UserManager.FindByNameAsync(loginInfo.UserName);
            if (user == null)
            {
                return new OperationResult<User>(OperationResultType.Error, "用户不存在");
            }
            if (user.IsLocked)
            {
                return new OperationResult<User>(OperationResultType.Error, "用户已被冻结，无法登录");
            }
            if (await UserManager.IsLockedOutAsync(user.Id))
            {
                return new OperationResult<User>(OperationResultType.Error,
                    $"用户因密码错误次数过多而被锁定 {UserManager.DefaultAccountLockoutTimeSpan.Minutes} 分钟，请稍后重试");
            }
            if (!await UserManager.CheckPasswordAsync(user, loginInfo.Password))
            {
                if (shouldLockout)
                {
                    await UserManager.AccessFailedAsync(user.Id);
                    if (await UserManager.IsLockedOutAsync(user.Id))
                    {
                        return new OperationResult<User>(OperationResultType.Error,
                            $"用户因密码错误次数过多而被锁定 {UserManager.DefaultAccountLockoutTimeSpan.Minutes} 分钟，请稍后重试");
                    }
                    return new OperationResult<User>(OperationResultType.Error,
                        $"用户名或密码错误，您还有 {UserManager.MaxFailedAccessAttemptsBeforeLockout - user.AccessFailedCount} 次机会");
                }
                return new OperationResult<User>(OperationResultType.Error, "用户名或密码错误");
            }
            return new OperationResult<User>(OperationResultType.Success, "用户登录成功", user);
        }

        #endregion
    }
}
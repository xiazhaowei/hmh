using Hmh.Core.Settings.Dtos;
using Hmh.Core.Settings.Models;
using OSharp.Utility.Data;
using System;
using System.Linq;
using System.Linq.Expressions;
using OSharp.Utility.Extensions;

namespace Hmh.Core.Settings
{
    public partial class SettingsService
    {
        #region Implementation of ISettingsContract
        /// <summary>
        /// 获取 信息查询数据集
        /// </summary>
        public IQueryable<UserDistributionLevel> UserDistributionLevels
        {
            get { return UserDistributionLevelRepository.Entities; }
        }


        /// <summary>
        /// 检查店信息是否存在
        /// </summary>
        /// <param name="predicate">检查谓语表达式</param>
        /// <param name="id">更新的信息编号</param>
        /// <returns>信息是否存在</returns>
        public bool CheckUserDistributionLevelExists(Expression<Func<UserDistributionLevel, bool>> predicate, int id = 0)
        {
            return UserDistributionLevelRepository.CheckExists(predicate, id);
        }

        /// <summary>
        /// 添加店铺信息信息
        /// </summary>
        /// <param name="inputDtos">要添加的店铺信息DTO信息</param>
        /// <returns>业务操作结果</returns>
        public OperationResult AddUserDistributionLevels(params UserDistributionLevelInputDto[] inputDtos)
        {
            return UserDistributionLevelRepository.Insert(inputDtos,
            dto =>
            {
                if (UserDistributionLevelRepository.CheckExists(ss => ss.Name == dto.Name))
                {
                    throw new Exception("级别：{0}已经存在，不能添加同名设置".FormatWith(dto.Name));
                }                
            },
            (dto, entity) =>
            {                
                return entity;
            });
        }

        /// <summary>
        /// 更新信息信息
        /// </summary>
        /// <param name="inputDtos">包含更新信息的信息DTO信息</param>
        /// <returns>业务操作结果</returns>
        public OperationResult EditUserDistributionLevels(params UserDistributionLevelInputDto[] inputDtos)
        {
            return UserDistributionLevelRepository.Update(inputDtos,
            (dto, entity) =>
            {                
            },
            (dto, entity) =>
            {               
                return entity;
            });
        }


        /// <summary>
        /// 删除信息信息
        /// </summary>
        /// <param name="ids">要删除的信息编号</param>
        /// <returns>业务操作结果</returns>
        public OperationResult DeleteUserDistributionLevels(params int[] ids)
        {
            return UserDistributionLevelRepository.Delete(ids);
        }

        #endregion
    }
}

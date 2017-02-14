using Hmh.Core.Settings.Dtos;
using Hmh.Core.Settings.Models;
using OSharp.Core.Dependency;
using OSharp.Utility.Data;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace Hmh.Core.Settings
{
    /// <summary>
    /// 业务契约--系统设置模块
    /// </summary>
    public interface ISettingsContract : IScopeDependency
    {
        #region 系统参数业务
        /// <summary>
        /// 获取 信息查询数据集
        /// </summary>
        IQueryable<SystemSetting> SystemSettings { get; }

        /// <summary>
        /// 检查信息是否存在
        /// </summary>
        /// <param name="predicate">检查谓语表达式</param>
        /// <param name="id">更新的信息编号</param>
        /// <returns>信息是否存在</returns>
        bool CheckSystemSettingExists(Expression<Func<SystemSetting, bool>> predicate, int id = 0);

        /// <summary>
        /// 添加信息
        /// </summary>
        /// <param name="inputDtos">要添加的信息DTO信息</param>
        /// <returns>业务操作结果</returns>
        OperationResult AddSystemSettings(params SystemSettingInputDto[] inputDtos);

        /// <summary>
        /// 更新信息信息
        /// </summary>
        /// <param name="inputDtos">包含更新信息的信息DTO信息</param>
        /// <returns>业务操作结果</returns>
        OperationResult EditSystemSettings(params SystemSettingInputDto[] inputDtos);

        /// <summary>
        /// 删除信息信息
        /// </summary>
        /// <param name="ids">要删除的信息编号</param>
        /// <returns>业务操作结果</returns>
        OperationResult DeleteSystemSettings(params int[] ids);

        #endregion

        #region 三级分销业务
        /// <summary>
        /// 获取 信息查询数据集
        /// </summary>
        IQueryable<ShopDistributionLevel> ShopDistributionLevels { get; }

        /// <summary>
        /// 检查信息是否存在
        /// </summary>
        /// <param name="predicate">检查谓语表达式</param>
        /// <param name="id">更新的信息编号</param>
        /// <returns>信息是否存在</returns>
        bool CheckShopDistributionLevelExists(Expression<Func<ShopDistributionLevel, bool>> predicate, int id = 0);

        /// <summary>
        /// 添加信息
        /// </summary>
        /// <param name="inputDtos">要添加的信息DTO信息</param>
        /// <returns>业务操作结果</returns>
        OperationResult AddShopDistributionLevels(params ShopDistributionLevelInputDto[] inputDtos);

        /// <summary>
        /// 更新信息信息
        /// </summary>
        /// <param name="inputDtos">包含更新信息的信息DTO信息</param>
        /// <returns>业务操作结果</returns>
        OperationResult EditShopDistributionLevels(params ShopDistributionLevelInputDto[] inputDtos);

        /// <summary>
        /// 删除信息信息
        /// </summary>
        /// <param name="ids">要删除的信息编号</param>
        /// <returns>业务操作结果</returns>
        OperationResult DeleteShopDistributionLevels(params int[] ids);

        #endregion

        #region 八代分润业务
        /// <summary>
        /// 获取 信息查询数据集
        /// </summary>
        IQueryable<UserDistributionLevel> UserDistributionLevels { get; }

        /// <summary>
        /// 检查信息是否存在
        /// </summary>
        /// <param name="predicate">检查谓语表达式</param>
        /// <param name="id">更新的信息编号</param>
        /// <returns>信息是否存在</returns>
        bool CheckUserDistributionLevelExists(Expression<Func<UserDistributionLevel, bool>> predicate, int id = 0);

        /// <summary>
        /// 添加信息
        /// </summary>
        /// <param name="inputDtos">要添加的信息DTO信息</param>
        /// <returns>业务操作结果</returns>
        OperationResult AddUserDistributionLevels(params UserDistributionLevelInputDto[] inputDtos);

        /// <summary>
        /// 更新信息信息
        /// </summary>
        /// <param name="inputDtos">包含更新信息的信息DTO信息</param>
        /// <returns>业务操作结果</returns>
        OperationResult EditUserDistributionLevels(params UserDistributionLevelInputDto[] inputDtos);

        /// <summary>
        /// 删除信息信息
        /// </summary>
        /// <param name="ids">要删除的信息编号</param>
        /// <returns>业务操作结果</returns>
        OperationResult DeleteUserDistributionLevels(params int[] ids);

        #endregion
    }
}

using Hmh.Core.Shop.Dtos;
using Hmh.Core.Shop.Models;
using OSharp.Core.Dependency;
using OSharp.Utility.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Hmh.Core.Shop
{
    /// <summary>
    /// 业务契约--店铺模块
    /// </summary>
    public interface IShopContract : IScopeDependency
    {
        #region 店铺信息业务
        /// <summary>
        /// 获取信息查询数据集
        /// </summary>
        IQueryable<Hmh.Core.Shop.Models.Shop> Shops { get; }

        /// <summary>
        /// 检查信息信息是否存在
        /// </summary>
        /// <param name="predicate">检查谓语表达式</param>
        /// <param name="id">更新的信息编号</param>
        /// <returns>信息是否存在</returns>
        bool CheckShopExists(Expression<Func<Hmh.Core.Shop.Models.Shop, bool>> predicate, int id = 0);

        /// <summary>
        /// 添加信息
        /// </summary>
        /// <param name="inputDtos">要添加的DTO信息</param>
        /// <returns>业务操作结果</returns>
        OperationResult AddShops(params ShopInputDto[] inputDtos);

        /// <summary>
        /// 更新信息信息
        /// </summary>
        /// <param name="inputDtos">包含更新信息的信息DTO信息</param>
        /// <returns>业务操作结果</returns>
        OperationResult EditShops(params ShopInputDto[] inputDtos);
        /// <summary>
        /// 更新实体
        /// </summary>
        /// <param name="shop"></param>
        /// <returns></returns>
        int Edit(Shop.Models.Shop shop);
        /// <summary>
        /// 删除信息
        /// </summary>
        /// <param name="ids">要删除的编号</param>
        /// <returns>业务操作结果</returns>
        OperationResult DeleteShops(params int[] ids);

        #endregion

        #region 运费模板业务

        /// <summary>
        /// 获取信息查询数据集
        /// </summary>
        IQueryable<ExpressTemplate> ExpressTemplates { get; }

        /// <summary>
        /// 添加信息
        /// </summary>
        /// <param name="inputDtos">要添加的DTO信息</param>
        /// <returns>业务操作结果</returns>
        Task<OperationResult> AddExpressTemplates(params ExpressTemplateDto[] inputDtos);

        /// <summary>
        /// 更新信息信息
        /// </summary>
        /// <param name="inputDtos">包含更新信息的信息DTO信息</param>
        /// <returns>业务操作结果</returns>
        //OperationResult EditExpressTemplates(params ExpressTemplateDto[] inputDtos);
        
        /// <summary>
        /// 删除信息
        /// </summary>
        /// <param name="ids">要删除的编号</param>
        /// <returns>业务操作结果</returns>
        OperationResult DeleteExpressTemplates(params int[] ids);


        #endregion

        #region 合同信息业务

        /// <summary>
        /// 获取查询数据集
        /// </summary>
        IQueryable<Contract> Contracts { get; }

        /// <summary>
        /// 检查信息信息是否存在
        /// </summary>
        /// <param name="predicate">检查谓语表达式</param>
        /// <param name="id">更新的编号</param>
        /// <returns>信息是否存在</returns>
        bool CheckContractExists(Expression<Func<Contract, bool>> predicate, int id = 0);

        /// <summary>
        /// 添加信息信息
        /// </summary>
        /// <param name="inputDtos">要添加的信息DTO信息</param>
        /// <returns>业务操作结果</returns>
        OperationResult AddContracts(params ContractInputDto[] inputDtos);

        /// <summary>
        /// 更新信息信息
        /// </summary>
        /// <param name="inputDtos">包含更新信息的信息DTO信息</param>
        /// <returns>业务操作结果</returns>
        OperationResult EditContracts(params ContractInputDto[] inputDtos);

        /// <summary>
        /// 删除信息信息
        /// </summary>
        /// <param name="ids">要删除的信息编号</param>
        /// <returns>业务操作结果</returns>
        OperationResult DeleteContracts(params int[] ids);
        #endregion

        #region 合同级别信息业务

        /// <summary>
        /// 获取 查询数据集
        /// </summary>
        IQueryable<ContractLevel> ContractLevels { get; }

        /// <summary>
        /// 检查信息是否存在
        /// </summary>
        /// <param name="predicate">检查谓语表达式</param>
        /// <param name="id">更新的编号</param>
        /// <returns>信息是否存在</returns>
        bool CheckContractLevelExists(Expression<Func<ContractLevel, bool>> predicate, int id = 0);

        /// <summary>
        /// 添加级别信息
        /// </summary>
        /// <param name="inputDtos">要添加的信息DTO信息</param>
        /// <returns>业务操作结果</returns>
        OperationResult AddContractLevels(params ContractLevelInputDto[] inputDtos);

        /// <summary>
        /// 更新信息
        /// </summary>
        /// <param name="inputDtos">包含更新信息的信息DTO信息</param>
        /// <returns>业务操作结果</returns>
        OperationResult EditContractLevels(params ContractLevelInputDto[] inputDtos);

        /// <summary>
        /// 删除级别信息
        /// </summary>
        /// <param name="ids">要删除的信息编号</param>
        /// <returns>业务操作结果</returns>
        OperationResult DeleteContractLevels(params int[] ids);
        #endregion

        #region 合同付款记录业务
        /// <summary>
        /// 获取 查询数据集
        /// </summary>
        IQueryable<ContractPay> ContractPays { get; }

        /// <summary>
        /// 检查信息是否存在
        /// </summary>
        /// <param name="predicate">检查谓语表达式</param>
        /// <param name="id">更新信息编号</param>
        /// <returns>信息是否存在</returns>
        bool CheckContractPayExists(Expression<Func<ContractPay, bool>> predicate, int id = 0);

        /// <summary>
        /// 添加信息
        /// </summary>
        /// <param name="inputDtos">要添加信息DTO信息</param>
        /// <returns>业务操作结果</returns>
        OperationResult AddContractPays(params ContractPayInputDto[] inputDtos);

        /// <summary>
        /// 更新信息
        /// </summary>
        /// <param name="inputDtos">包含更新信息的DTO信息</param>
        /// <returns>业务操作结果</returns>
        OperationResult EditContractPays(params ContractPayInputDto[] inputDtos);

        /// <summary>
        /// 删除信息
        /// </summary>
        /// <param name="ids">要删除的编号</param>
        /// <returns>业务操作结果</returns>
        OperationResult DeleteContractPays(params int[] ids);

        #endregion

        #region 店铺认证业务
        /// <summary>
        /// 获取 查询数据集
        /// </summary>
        IQueryable<ShopPermit> ShopPermits { get; }

        /// <summary>
        /// 检查信息信息是否存在
        /// </summary>
        /// <param name="predicate">检查谓语表达式</param>
        /// <param name="id">更新的信息编号</param>
        /// <returns>信息是否存在</returns>
        bool CheckShopPermitExists(Expression<Func<ShopPermit, bool>> predicate, int id = 0);

        /// <summary>
        /// 添加合同信息信息
        /// </summary>
        /// <param name="inputDtos">要添加的合同信息DTO信息</param>
        /// <returns>业务操作结果</returns>
        OperationResult AddShopPermits(params ShopPermitInputDto[] inputDtos);

        /// <summary>
        /// 更新合同信息信息
        /// </summary>
        /// <param name="inputDtos">包含更新信息的合同信息DTO信息</param>
        /// <returns>业务操作结果</returns>
        OperationResult EditShopPermits(params ShopPermitInputDto[] inputDtos);

        /// <summary>
        /// 删除合同信息信息
        /// </summary>
        /// <param name="ids">要删除的合同信息编号</param>
        /// <returns>业务操作结果</returns>
        OperationResult DeleteShopPermits(params int[] ids);
        #endregion

        #region 地区信息业务

        /// <summary>
        /// 获取 地区查询数据集
        /// </summary>
        IQueryable<Region> Regions { get; }

        /// <summary>
        /// 检查地区信息是否存在
        /// </summary>
        /// <param name="predicate">检查谓语表达式</param>
        /// <param name="id">更新的合同信息编号</param>
        /// <returns>合同信息是否存在</returns>
        bool CheckRegionExists(Expression<Func<Region, bool>> predicate, int id = 0);

        /// <summary>
        /// 添加地区信息
        /// </summary>
        /// <param name="inputDtos">要添加的地区信息DTO信息</param>
        /// <returns>业务操作结果</returns>
        OperationResult AddRegions(params RegionInputDto[] inputDtos);

        /// <summary>
        /// 更新地区信息
        /// </summary>
        /// <param name="inputDtos">包含更新信息的地区DTO信息</param>
        /// <returns>业务操作结果</returns>
        OperationResult EditRegions(params RegionInputDto[] inputDtos);

        /// <summary>
        /// 删除地区信息
        /// </summary>
        /// <param name="ids">要删除的地区编号</param>
        /// <returns>业务操作结果</returns>
        OperationResult DeleteRegions(params int[] ids);

        #endregion
    }
}

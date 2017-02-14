using Hmh.Core.Goods.Dtos;
using Hmh.Core.Goods.Models;
using OSharp.Core.Dependency;
using OSharp.Utility.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Hmh.Core.Goods
{
    /// <summary>
    /// 业务契约--产品模块
    /// </summary>
    public interface IGoodsContract : IScopeDependency
    {
        #region 商品业务
        /// <summary>
        /// 获取信息查询数据集
        /// </summary>
        IQueryable<Models.Goods> Goodss { get; }

        /// <summary>
        /// 检查信息信息是否存在
        /// </summary>
        /// <param name="predicate">检查谓语表达式</param>
        /// <param name="id">更新的信息编号</param>
        /// <returns>信息是否存在</returns>
        bool CheckGoodsExists(Expression<Func<Models.Goods, bool>> predicate, int id = 0);

        /// <summary>
        /// 添加信息
        /// </summary>
        /// <param name="inputDtos">要添加的DTO信息</param>
        /// <returns>业务操作结果</returns>
        Task<OperationResult> AddGoodss(params GoodsInputDto[] inputDtos);

        /// <summary>
        /// 更新信息信息
        /// </summary>
        /// <param name="inputDtos">包含更新信息的信息DTO信息</param>
        /// <returns>业务操作结果</returns>
        OperationResult EditGoodss(params GoodsInputDto[] inputDtos);

        /// <summary>
        /// 删除信息
        /// </summary>
        /// <param name="ids">要删除的编号</param>
        /// <returns>业务操作结果</returns>
        OperationResult DeleteGoodss(params int[] ids);

        #endregion

        #region 商品评价业务
        /// <summary>
        /// 获取信息查询数据集
        /// </summary>
        IQueryable<Models.GoodsComment> GoodsComments { get; }

        /// <summary>
        /// 检查信息信息是否存在
        /// </summary>
        /// <param name="predicate">检查谓语表达式</param>
        /// <param name="id">更新的信息编号</param>
        /// <returns>信息是否存在</returns>
        bool CheckGoodsCommentExists(Expression<Func<Models.GoodsComment, bool>> predicate, int id = 0);

        /// <summary>
        /// 添加信息
        /// </summary>
        /// <param name="inputDtos">要添加的DTO信息</param>
        /// <returns>业务操作结果</returns>
        OperationResult AddGoodsComments(params GoodsCommentInputDto[] inputDtos);

        /// <summary>
        /// 更新信息信息
        /// </summary>
        /// <param name="inputDtos">包含更新信息的信息DTO信息</param>
        /// <returns>业务操作结果</returns>
        OperationResult EditGoodsComments(params GoodsCommentInputDto[] inputDtos);

        /// <summary>
        /// 删除信息
        /// </summary>
        /// <param name="ids">要删除的编号</param>
        /// <returns>业务操作结果</returns>
        OperationResult DeleteGoodsComments(params int[] ids);

        #endregion


        #region 发布分类业务

        /// <summary>
        /// 获取查询数据集
        /// </summary>
        IQueryable<Category> Categorys { get; }

        /// <summary>
        /// 检查信息信息是否存在
        /// </summary>
        /// <param name="predicate">检查谓语表达式</param>
        /// <param name="id">更新的编号</param>
        /// <returns>信息是否存在</returns>
        bool CheckCategoryExists(Expression<Func<Category, bool>> predicate, int id = 0);

        /// <summary>
        /// 添加信息信息
        /// </summary>
        /// <param name="inputDtos">要添加的信息DTO信息</param>
        /// <returns>业务操作结果</returns>
        OperationResult AddCategorys(params CategoryInputDto[] inputDtos);

        /// <summary>
        /// 更新信息信息
        /// </summary>
        /// <param name="inputDtos">包含更新信息的信息DTO信息</param>
        /// <returns>业务操作结果</returns>
        OperationResult EditCategorys(params CategoryInputDto[] inputDtos);

        /// <summary>
        /// 删除信息信息
        /// </summary>
        /// <param name="ids">要删除的信息编号</param>
        /// <returns>业务操作结果</returns>
        OperationResult DeleteCategorys(params int[] ids);
        #endregion

        #region 属性业务

        /// <summary>
        /// 获取 查询数据集
        /// </summary>
        IQueryable<Attr> Attrs { get; }

        /// <summary>
        /// 检查信息是否存在
        /// </summary>
        /// <param name="predicate">检查谓语表达式</param>
        /// <param name="id">更新的编号</param>
        /// <returns>信息是否存在</returns>
        bool CheckAttrExists(Expression<Func<Attr, bool>> predicate, int id = 0);

        /// <summary>
        /// 添加级别信息
        /// </summary>
        /// <param name="inputDtos">要添加的信息DTO信息</param>
        /// <returns>业务操作结果</returns>
        OperationResult AddAttrs(params AttrInputDto[] inputDtos);

        /// <summary>
        /// 更新信息
        /// </summary>
        /// <param name="inputDtos">包含更新信息的信息DTO信息</param>
        /// <returns>业务操作结果</returns>
        OperationResult EditAttrs(params AttrInputDto[] inputDtos);

        /// <summary>
        /// 删除级别信息
        /// </summary>
        /// <param name="ids">要删除的信息编号</param>
        /// <returns>业务操作结果</returns>
        OperationResult DeleteAttrs(params int[] ids);
        #endregion

        #region 规格 业务
        /// <summary>
        /// 获取 查询数据集
        /// </summary>
        IQueryable<GoodsSpecification> GoodsSpecifications { get; }

        /// <summary>
        /// 检查信息是否存在
        /// </summary>
        /// <param name="predicate">检查谓语表达式</param>
        /// <param name="id">更新信息编号</param>
        /// <returns>信息是否存在</returns>
        bool CheckGoodsSpecificationExists(Expression<Func<GoodsSpecification, bool>> predicate, int id = 0);

        /// <summary>
        /// 添加信息
        /// </summary>
        /// <param name="inputDtos">要添加信息DTO信息</param>
        /// <returns>业务操作结果</returns>
        OperationResult AddGoodsSpecifications(params GoodsSpecificationInputDto[] inputDtos);

        /// <summary>
        /// 更新信息
        /// </summary>
        /// <param name="inputDtos">包含更新信息的DTO信息</param>
        /// <returns>业务操作结果</returns>
        OperationResult EditGoodsSpecifications(params GoodsSpecificationInputDto[] inputDtos);

        /// <summary>
        /// 删除信息
        /// </summary>
        /// <param name="ids">要删除的编号</param>
        /// <returns>业务操作结果</returns>
        OperationResult DeleteGoodsSpecifications(params int[] ids);

        #endregion

        #region 颜色规格选项业务

        /// <summary>
        /// 获取 查询数据集
        /// </summary>
        IQueryable<GoodsColorSpecificationItem> GoodsColorSpecificationItems { get; }

        /// <summary>
        /// 检查信息是否存在
        /// </summary>
        /// <param name="predicate">检查谓语表达式</param>
        /// <param name="id">更新信息编号</param>
        /// <returns>信息是否存在</returns>
        bool CheckGoodsColorSpecificationItemExists(Expression<Func<GoodsColorSpecificationItem, bool>> predicate, int id = 0);

        /// <summary>
        /// 添加信息
        /// </summary>
        /// <param name="inputDtos">要添加信息DTO信息</param>
        /// <returns>业务操作结果</returns>
        OperationResult AddGoodsColorSpecificationItems(params GoodsColorSpecificationItemInputDto[] inputDtos);

        /// <summary>
        /// 更新信息
        /// </summary>
        /// <param name="inputDtos">包含更新信息的DTO信息</param>
        /// <returns>业务操作结果</returns>
        OperationResult EditGoodsColorSpecificationItems(params GoodsColorSpecificationItemInputDto[] inputDtos);

        /// <summary>
        /// 删除信息
        /// </summary>
        /// <param name="ids">要删除的编号</param>
        /// <returns>业务操作结果</returns>
        OperationResult DeleteGoodsColorSpecificationItems(params int[] ids);

        #endregion



        #region 展现分类业务
        /// <summary>
        /// 获取 查询数据集
        /// </summary>
        IQueryable<ShowCategory> ShowCategorys { get; }

        /// <summary>
        /// 检查信息信息是否存在
        /// </summary>
        /// <param name="predicate">检查谓语表达式</param>
        /// <param name="id">更新的信息编号</param>
        /// <returns>信息是否存在</returns>
        bool CheckShowCategoryExists(Expression<Func<ShowCategory, bool>> predicate, int id = 0);

        /// <summary>
        /// 添加合同信息信息
        /// </summary>
        /// <param name="inputDtos">要添加的合同信息DTO信息</param>
        /// <returns>业务操作结果</returns>
        OperationResult AddShowCategorys(params ShowCategoryInputDto[] inputDtos);

        /// <summary>
        /// 更新合同信息信息
        /// </summary>
        /// <param name="inputDtos">包含更新信息的合同信息DTO信息</param>
        /// <returns>业务操作结果</returns>
        OperationResult EditShowCategorys(params ShowCategoryInputDto[] inputDtos);

        /// <summary>
        /// 删除合同信息信息
        /// </summary>
        /// <param name="ids">要删除的合同信息编号</param>
        /// <returns>业务操作结果</returns>
        OperationResult DeleteShowCategorys(params int[] ids);
        #endregion

        
    }
}

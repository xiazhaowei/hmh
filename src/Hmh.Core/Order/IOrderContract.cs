using Hmh.Core.Order.Dtos;
using Hmh.Core.Order.Models;
using OSharp.Core.Dependency;
using OSharp.Utility.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Hmh.Core.Order
{
    /// <summary>
    /// 业务契约-订单模块
    /// </summary>
    public interface IOrderContract : IScopeDependency
    {
        

        #region 购物车业务
        /// <summary>
        /// 获取信息查询数据集
        /// </summary>
        IQueryable<CartGoods> CartGoodses { get; }

        /// <summary>
        /// 检查信息信息是否存在
        /// </summary>
        /// <param name="predicate">检查谓语表达式</param>
        /// <param name="id">更新的信息编号</param>
        /// <returns>信息是否存在</returns>
        bool CheckCartGoodses(Expression<Func<CartGoods, bool>> predicate, int id = 0);

        /// <summary>
        /// 添加信息
        /// </summary>
        /// <param name="inputDtos">要添加的DTO信息</param>
        /// <returns>业务操作结果</returns>
        Task<OperationResult> AddCartGoodses(params CartGoodsInputDto[] inputDtos);        

        /// <summary>
        /// 删除信息
        /// </summary>
        /// <param name="ids">要删除的编号</param>
        /// <returns>业务操作结果</returns>
        OperationResult DeleteCartGoodses(params int[] ids);

        #endregion

        #region 订单业务
        /// <summary>
        /// 获取信息查询数据集
        /// </summary>
        IQueryable<Models.Order> Orders { get; }

        /// <summary>
        /// 检查信息信息是否存在
        /// </summary>
        /// <param name="predicate">检查谓语表达式</param>
        /// <param name="id">更新的信息编号</param>
        /// <returns>信息是否存在</returns>
        bool CheckOrders(Expression<Func<Models.Order, bool>> predicate, int id = 0);

        /// <summary>
        /// 添加信息
        /// </summary>
        /// <param name="inputDtos">要添加的DTO信息</param>
        /// <returns>业务操作结果</returns>
        Task<OperationResult> AddOrders(params OrderInputDto[] inputDtos);

        /// <summary>
        /// 删除信息
        /// </summary>
        /// <param name="ids">要删除的编号</param>
        /// <returns>业务操作结果</returns>
        OperationResult DeleteOrders(params int[] ids);

        #endregion

    }
}

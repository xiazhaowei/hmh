using Hmh.Core.Goods.Dtos;
using Hmh.Core.Goods.Models;
using OSharp.Utility.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using OSharp.Utility.Extensions;

namespace Hmh.Core.Goods
{
    public partial class GoodsService
    {
        #region Implementation of IGoodsContract
        /// <summary>
        /// 获取 查询数据集
        /// </summary>
        public IQueryable<GoodsColorSpecificationItem> GoodsColorSpecificationItems
        {
            get { return GoodsColorSpecificationItemRepository.Entities; }
        }


        /// <summary>
        /// 检查信息信息是否存在
        /// </summary>
        /// <param name="predicate">检查谓语表达式</param>
        /// <param name="id">更新的信息编号</param>
        /// <returns>信息是否存在</returns>
        public bool CheckGoodsColorSpecificationItemExists(Expression<Func<GoodsColorSpecificationItem, bool>> predicate, int id = 0)
        {
            return GoodsColorSpecificationItemRepository.CheckExists(predicate, id);
        }

        /// <summary>
        /// 添加信息信息
        /// </summary>
        /// <param name="inputDtos">要添加的店铺信息DTO信息</param>
        /// <returns>业务操作结果</returns>
        public OperationResult AddGoodsColorSpecificationItems(params GoodsColorSpecificationItemInputDto[] inputDtos)
        {
            OperationResult result= GoodsColorSpecificationItemRepository.Insert(inputDtos,
            dto =>
            {                
                if (CategoryRepository.CheckExists(c => c.Name == dto.Name))
                {
                    throw new Exception("颜色：{0}已经存在，不能添加".FormatWith(dto.Name));
                }                
                               
            },
            (dto, entity) =>
            {                
                return entity;
            });
            
            return result;
        }

        /// <summary>
        /// 更新信息信息
        /// </summary>
        /// <param name="inputDtos">包含更新信息的店铺信息DTO信息</param>
        /// <returns>业务操作结果</returns>
        public OperationResult EditGoodsColorSpecificationItems(params GoodsColorSpecificationItemInputDto[] inputDtos)
        {
            OperationResult result= GoodsColorSpecificationItemRepository.Update(inputDtos,
            (dto, entity) =>
            {                
            },
            (dto, entity) =>
            {                
                return entity;
            });            
            return result;
        }


        /// <summary>
        /// 删除店铺信息信息
        /// </summary>
        /// <param name="ids">要删除的店铺信息编号</param>
        /// <returns>业务操作结果</returns>
        public OperationResult DeleteGoodsColorSpecificationItems(params int[] ids)
        {
            return GoodsColorSpecificationItemRepository.Delete(ids);
        }

        #endregion
    }
}

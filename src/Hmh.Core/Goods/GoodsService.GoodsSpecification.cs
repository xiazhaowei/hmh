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
        public IQueryable<GoodsSpecification> GoodsSpecifications
        {
            get { return GoodsSpecificationRepository.Entities; }
        }


        /// <summary>
        /// 检查信息信息是否存在
        /// </summary>
        /// <param name="predicate">检查谓语表达式</param>
        /// <param name="id">更新的信息编号</param>
        /// <returns>信息是否存在</returns>
        public bool CheckGoodsSpecificationExists(Expression<Func<GoodsSpecification, bool>> predicate, int id = 0)
        {
            return GoodsSpecificationRepository.CheckExists(predicate, id);
        }

        /// <summary>
        /// 添加信息信息
        /// </summary>
        /// <param name="inputDtos">要添加的店铺信息DTO信息</param>
        /// <returns>业务操作结果</returns>
        public OperationResult AddGoodsSpecifications(params GoodsSpecificationInputDto[] inputDtos)
        {
            OperationResult result= GoodsSpecificationRepository.Insert(inputDtos,
            dto =>
            {                
                if (CategoryRepository.CheckExists(c => c.Name == dto.Name))
                {
                    throw new Exception("分类：{0}已经存在，不能添加".FormatWith(dto.Name));
                }                
                               
            },
            (dto, entity) =>
            {
                if(dto.CategoryId.HasValue && dto.CategoryId.Value>0)
                {
                    Category category = CategoryRepository.GetByKey(dto.CategoryId.Value);
                    if(category == null)
                    {
                        throw new Exception("发布分类不存在");
                    }
                    category.GoodsSpecifications.Add(entity);                 
                    entity.Category = category;
                } 
                return entity;
            });
            
            return result;
        }

        /// <summary>
        /// 更新信息信息
        /// </summary>
        /// <param name="inputDtos">包含更新信息的店铺信息DTO信息</param>
        /// <returns>业务操作结果</returns>
        public OperationResult EditGoodsSpecifications(params GoodsSpecificationInputDto[] inputDtos)
        {
            OperationResult result= GoodsSpecificationRepository.Update(inputDtos,
            (dto, entity) =>
            {
                
            },
            (dto, entity) =>
            {                
                return entity;
            });

            if(result.ResultType==OperationResultType.Success && inputDtos.Count()==1)
            {
                GoodsSpecification goodsSpecification = GoodsSpecificationRepository.GetByKey(inputDtos[0].Id);
                if (goodsSpecification != null)
                    result.Data = goodsSpecification;
            }
            return result;
        }


        /// <summary>
        /// 删除店铺信息信息
        /// </summary>
        /// <param name="ids">要删除的店铺信息编号</param>
        /// <returns>业务操作结果</returns>
        public OperationResult DeleteGoodsSpecifications(params int[] ids)
        {
            return GoodsSpecificationRepository.Delete(ids);
        }

        #endregion
    }
}

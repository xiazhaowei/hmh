using Hmh.Core.Goods.Dtos;
using Hmh.Core.Goods.Models;
using OSharp.Utility.Data;
using System;
using System.Linq;
using System.Linq.Expressions;
using OSharp.Utility.Extensions;

namespace Hmh.Core.Goods
{
    public partial class GoodsService
    {
        #region Implementation of IGoodsContract
        /// <summary>
        /// 获取 查询数据集
        /// </summary>
        public IQueryable<ShowCategory> ShowCategorys 
        {
            get { return ShowCategoryRepository.Entities; }
        }


        /// <summary>
        /// 检查信息信息是否存在
        /// </summary>
        /// <param name="predicate">检查谓语表达式</param>
        /// <param name="id">更新的信息编号</param>
        /// <returns>信息是否存在</returns>
        public bool CheckShowCategoryExists(Expression<Func<ShowCategory, bool>> predicate, int id = 0)
        {
            return ShowCategoryRepository.CheckExists(predicate, id);
        }

        /// <summary>
        /// 添加信息信息
        /// </summary>
        /// <param name="inputDtos">要添加的店铺信息DTO信息</param>
        /// <returns>业务操作结果</returns>
        public OperationResult AddShowCategorys(params ShowCategoryInputDto[] inputDtos)
        {
            return ShowCategoryRepository.Insert(inputDtos,
            dto =>
            {
                if (ShowCategoryRepository.CheckExists(c => c.Name == dto.Name && c.Parent.Id==dto.ParentId))
                {
                    throw new Exception("分类：{0}已经存在，不能添加同名".FormatWith(dto.Name));
                }                
            },
            (dto, entity) =>
            {
                if(dto.ParentId.HasValue && dto.ParentId.Value>0)
                {
                    ShowCategory parentCategory = ShowCategoryRepository.GetByKey(dto.ParentId.Value);
                    if(parentCategory == null)
                    {
                        throw new Exception("父级不存在");
                    }
                    parentCategory.Children.Add(entity);
                    entity.Parent = parentCategory;
                } 
                return entity;
            });
        }

        /// <summary>
        /// 更新信息信息
        /// </summary>
        /// <param name="inputDtos">包含更新信息的店铺信息DTO信息</param>
        /// <returns>业务操作结果</returns>
        public OperationResult EditShowCategorys(params ShowCategoryInputDto[] inputDtos)
        {
            return ShowCategoryRepository.Update(inputDtos,
            (dto, entity) =>
            {
                
            },
            (dto, entity) =>
            {                
                return entity;
            });
        }


        /// <summary>
        /// 删除店铺信息信息
        /// </summary>
        /// <param name="ids">要删除的店铺信息编号</param>
        /// <returns>业务操作结果</returns>
        public OperationResult DeleteShowCategorys(params int[] ids)
        {
            return ShowCategoryRepository.Delete(ids);
        }

        #endregion
    }
}

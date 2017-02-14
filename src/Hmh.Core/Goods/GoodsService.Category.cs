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
        public IQueryable<Category> Categorys 
        {
            get { return CategoryRepository.Entities; }
        }


        /// <summary>
        /// 检查信息信息是否存在
        /// </summary>
        /// <param name="predicate">检查谓语表达式</param>
        /// <param name="id">更新的信息编号</param>
        /// <returns>信息是否存在</returns>
        public bool CheckCategoryExists(Expression<Func<Category, bool>> predicate, int id = 0)
        {
            return CategoryRepository.CheckExists(predicate, id);
        }

        /// <summary>
        /// 添加信息信息
        /// </summary>
        /// <param name="inputDtos">要添加的店铺信息DTO信息</param>
        /// <returns>业务操作结果</returns>
        public OperationResult AddCategorys(params CategoryInputDto[] inputDtos)
        {
            OperationResult result= CategoryRepository.Insert(inputDtos,
            dto =>
            {                
                               
                               
            },
            (dto, entity) =>
            {
                if(dto.ParentId.HasValue && dto.ParentId.Value>0)
                {
                    Category parentCategory = CategoryRepository.GetByKey(dto.ParentId.Value);
                    if(parentCategory == null)
                    {
                        throw new Exception("父级不存在");
                    }
                    parentCategory.Children.Add(entity);                 
                    entity.Parent = parentCategory;
                } 
                return entity;
            });


            //返回刚添加的数据 这里有问题
            //if(result.ResultType==OperationResultType.Success && inputDtos.Count()==1)
            //{
            //    Category category = Categorys.SingleOrDefault(c=>c.Name==inputDtos[0].Name );
            //    if (category != null)
            //        result.Data = category;
            //}
            return result;
        }

        /// <summary>
        /// 更新信息信息
        /// </summary>
        /// <param name="inputDtos">包含更新信息的店铺信息DTO信息</param>
        /// <returns>业务操作结果</returns>
        public OperationResult EditCategorys(params CategoryInputDto[] inputDtos)
        {
            OperationResult result= CategoryRepository.Update(inputDtos,
            (dto, entity) =>
            {
                
            },
            (dto, entity) =>
            {                
                return entity;
            });

            if(result.ResultType==OperationResultType.Success && inputDtos.Count()==1)
            {
                Category category = CategoryRepository.GetByKey(inputDtos[0].Id);
                if (category != null)
                    result.Data = category;
            }
            return result;
        }


        /// <summary>
        /// 删除店铺信息信息
        /// </summary>
        /// <param name="ids">要删除的店铺信息编号</param>
        /// <returns>业务操作结果</returns>
        public OperationResult DeleteCategorys(params int[] ids)
        {
            return CategoryRepository.Delete(ids);
        }

        #endregion
    }
}

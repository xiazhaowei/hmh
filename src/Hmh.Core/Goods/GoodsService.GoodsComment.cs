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
using Hmh.Core.Identity.Models;

namespace Hmh.Core.Goods
{
    public partial class GoodsService
    {
        #region Implementation of IGoodsContract
        /// <summary>
        /// 获取 查询数据集
        /// </summary>
        public IQueryable<Models.GoodsComment> GoodsComments
        {
            get { return GoodsCommentRepository.Entities; }
        }


        /// <summary>
        /// 检查信息信息是否存在
        /// </summary>
        /// <param name="predicate">检查谓语表达式</param>
        /// <param name="id">更新的信息编号</param>
        /// <returns>信息是否存在</returns>
        public bool CheckGoodsCommentExists(Expression<Func<Models.GoodsComment, bool>> predicate, int id = 0)
        {
            return GoodsCommentRepository.CheckExists(predicate, id);
        }

        /// <summary>
        /// 添加店铺信息信息
        /// </summary>
        /// <param name="inputDtos">要添加的店铺信息DTO信息</param>
        /// <returns>业务操作结果</returns>
        public OperationResult AddGoodsComments(params GoodsCommentInputDto[] inputDtos)
        {
            OperationResult result = GoodsCommentRepository.Insert(inputDtos,
            dto =>
            {                

            },
            (dto, entity) =>
            {
                if (dto.GoodsId.HasValue && dto.GoodsId.Value > 0)
                {
                    Goods.Models.Goods goods = GoodsRepository.GetByKey(dto.GoodsId.Value);
                    if (goods == null)
                    {
                        throw new Exception("商品不存在");
                    }
                    goods.GoodsComments.Add(entity);
                    entity.Goods = goods;
                }

                if(dto.UserId.HasValue && dto.UserId.Value>0)
                {
                    Identity.Models.User user = UserRepository.GetByKey(dto.UserId.Value);
                    if(user==null)
                    {
                        throw new Exception("用户不存在");
                    }
                    entity.User = user;
                }

                return entity;
            });

            return result;
        }

        /// <summary>
        /// 更新店铺信息信息
        /// </summary>
        /// <param name="inputDtos">包含更新信息的店铺信息DTO信息</param>
        /// <returns>业务操作结果</returns>
        public OperationResult EditGoodsComments(params GoodsCommentInputDto[] inputDtos)
        {
            return GoodsCommentRepository.Update(inputDtos,
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
        public OperationResult DeleteGoodsComments(params int[] ids)
        {
            return GoodsCommentRepository.Delete(ids);
        }

        #endregion
    }
}

using System;
using Hmh.Core.Shop.Dtos;
using Hmh.Core.Shop.Models;
using OSharp.Utility.Data;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using OSharp.Utility.Extensions;
using Hmh.Core.Identity.Models;

namespace Hmh.Core.Shop
{
    public partial class ShopService
    {
        #region Implementation of IShopContract
        /// <summary>
        /// 获取 合同查询数据集
        /// </summary>
        public IQueryable<ShopPermit> ShopPermits
        {
            get { return ShopPermitRepository.Entities; }
        }


        /// <summary>
        /// 检查合同信息信息是否存在
        /// </summary>
        /// <param name="predicate">检查谓语表达式</param>
        /// <param name="id">更新的合同信息编号</param>
        /// <returns>合同信息是否存在</returns>
        public bool CheckShopPermitExists(Expression<Func<ShopPermit, bool>> predicate, int id = 0)
        {
            return ShopPermitRepository.CheckExists(predicate, id);
        }


        /// <summary>
        /// 添加合同信息信息
        /// </summary>
        /// <param name="inputDtos">要添加的合同信息DTO信息</param>
        /// <returns>业务操作结果</returns>
        public OperationResult AddShopPermits(params ShopPermitInputDto[] inputDtos)
        {
            
            return ShopPermitRepository.Insert(inputDtos,
            dto =>
            {
                //认证为待审核状态
                dto.State = ShopPermitState.Verifying;
            },
            (dto, entity) => {
                if(dto.ShopId.HasValue && dto.ShopId.Value>0)
                {
                    Models.Shop shop = ShopRepository.GetByKey(dto.ShopId.Value);
                    if(shop==null)
                    {
                        throw new Exception("所属店铺不存在");
                    }                   
                    shop.ShopPermit = entity;
                    entity.Shop = shop;
                }
                else
                {
                    throw new Exception("没有归属店铺");
                }
                return entity;
            });
        }

        /// <summary>
        /// 更信息
        /// </summary>
        /// <param name="inputDtos">包含更新信息的DTO信息</param>
        /// <returns>业务操作结果</returns>
        public OperationResult EditShopPermits(params ShopPermitInputDto[] inputDtos)
        {
            return ShopPermitRepository.Update(inputDtos,
                (dto, entity) => {
                                        
                },
                (dto, entity) => { 
                    return entity;
                });
        }

        /// <summary>
        /// 删除信息信息
        /// </summary>
        /// <param name="ids">要删除的信息编号</param>
        /// <returns>业务操作结果</returns>
        public OperationResult DeleteShopPermits(params int[] ids)
        {
            return ShopPermitRepository.Delete(ids);
        }

        #endregion
    }
}

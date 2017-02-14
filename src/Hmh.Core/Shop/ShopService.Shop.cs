using Hmh.Core.Shop.Dtos;
using Hmh.Core.Shop.Models;
using OSharp.Utility.Data;
using System;
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
        /// 获取 信息查询数据集
        /// </summary>
        public IQueryable<Hmh.Core.Shop.Models.Shop> Shops 
        {
            get { return ShopRepository.Entities; }
        }


        /// <summary>
        /// 检查信息信息是否存在
        /// </summary>
        /// <param name="predicate">检查谓语表达式</param>
        /// <param name="id">更新的信息编号</param>
        /// <returns>信息是否存在</returns>
        public bool CheckShopExists(Expression<Func<Hmh.Core.Shop.Models.Shop, bool>> predicate, int id = 0)
        {
            return ShopRepository.CheckExists(predicate, id);
        }

        /// <summary>
        /// 添加信息信息
        /// </summary>
        /// <param name="inputDtos">要添加的信息DTO信息</param>
        /// <returns>业务操作结果</returns>
        public OperationResult AddShops(params ShopInputDto[] inputDtos)
        {
            return ShopRepository.Insert(inputDtos,
            dto =>
            {
                if (ShopRepository.CheckExists(shop => shop.Name == dto.Name))
                {
                    throw new Exception("店铺名称：{0}已经存在，不能添加同名店铺".FormatWith(dto.Name));
                }                
            },
            (dto, entity) =>
            {
                if(dto.UserId.HasValue && dto.UserId.Value>0)
                {
                    User user = UserRepository.GetByKey(dto.UserId.Value);
                    if(user==null)
                    {
                        throw new Exception("店铺开店人不存在");
                    }
                    if (ShopRepository.CheckExists(shop => shop.User.Id==dto.UserId.Value))
                    {
                        throw new Exception("您已经拥有店铺");
                    }
                    user.Shop = entity;
                    entity.User = user;
                }
                else
                {
                    throw new Exception("店铺不能没有开店人");
                }

                if(dto.RegionId.HasValue && dto.RegionId.Value > 0)
                {
                    Region region = RegionRepository.GetByKey(dto.RegionId.Value);
                    if(region!=null)
                    {
                        entity.Region = region;
                    }
                    else
                    {
                        throw new Exception("地区不存在");
                    }
                }

                return entity;
            });
        }

        /// <summary>
        /// 更新信息信息
        /// </summary>
        /// <param name="inputDtos">包含信息的店铺信息DTO信息</param>
        /// <returns>业务操作结果</returns>
        public OperationResult EditShops(params ShopInputDto[] inputDtos)
        {
            return ShopRepository.Update(inputDtos,
            (dto, entity) =>
            {                
                if (ShopRepository.CheckExists(shop => shop.Name == dto.Name, dto.Id))
                {
                    throw new Exception("店铺名称：{0}已经存在，不能添加同名店铺".FormatWith(dto.Name));
                }                
            },
            (dto, entity) =>
            {
                if(!dto.UserId.HasValue || dto.UserId==0)
                {
                    entity.User = null;
                }
                else if(entity.User!=null && entity.User.Id!=dto.UserId)
                {
                    User user = UserRepository.GetByKey(dto.UserId.Value);
                    if(user==null)
                    {
                        throw new Exception("指定的开店人不存在");
                    }
                    entity.User = user;                  
                }                
                return entity;
            });
        }

        /// <summary>
        /// 更新实体
        /// </summary>
        /// <param name="shop"></param>
        /// <returns></returns>
        public int Edit(Shop.Models.Shop shop)
        {
            return ShopRepository.Update(shop);
        }

        /// <summary>
        /// 删除信息信息
        /// </summary>
        /// <param name="ids">要删除的信息编号</param>
        /// <returns>业务操作结果</returns>
        public OperationResult DeleteShops(params int[] ids)
        {
            return ShopRepository.Delete(ids);
        }

        #endregion
    }
}

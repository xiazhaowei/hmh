using Hmh.Core.Shop.Dtos;
using Hmh.Core.Shop.Models;
using OSharp.Utility.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Hmh.Core.Shop
{
    public partial class ShopService
    {
        #region Implementation of IShopContract
        /// <summary>
        /// 获取 地区查询数据集
        /// </summary>
        public IQueryable<Region> Regions 
        {
            get { return RegionRepository.Entities; }
        }

        /// <summary>
        /// 检查地区信息是否存在
        /// </summary>
        /// <param name="predicate">检查谓语表达式</param>
        /// <param name="id">更新的信息编号</param>
        /// <returns>信息是否存在</returns>
        public bool CheckRegionExists(Expression<Func<Region, bool>> predicate, int id = 0)
        {
            return RegionRepository.CheckExists(predicate, id);
        }


        /// <summary>
        /// 添加合同级别信息
        /// </summary>
        /// <param name="inputDtos">要添加的信息DTO信息</param>
        /// <returns>业务操作结果</returns>
        public OperationResult AddRegions(params RegionInputDto[] inputDtos)
        {
            return RegionRepository.Insert(inputDtos,
                dto =>
                {
                    if(RegionRepository.CheckExists(r=>r.Province==dto.Province && r.City==dto.City && r.County==dto.County && r.Street==dto.Street))
                    {
                        throw new Exception("地区，已经存在，不能重复添加");
                    }
                },
                (dto, entity) =>
                {
                    return entity;
                });
        }

        /// <summary>
        /// 更新信息
        /// </summary>
        /// <param name="inputDtos">包含更新信息DTO信息</param>
        /// <returns>业务操作结果</returns>
        public OperationResult EditRegions(params RegionInputDto[] inputDtos)
        {
            return RegionRepository.Update(inputDtos,
                (dto, entity) =>
                {
                    if (RegionRepository.CheckExists(r => r.Province == dto.Province && r.City == dto.City && r.County == dto.County && r.Street == dto.Street))
                    {
                        throw new Exception("地区，已经存在，不能重复添加");
                    }
                },
                (dto, entity) =>
                {
                    return entity;
                });

        }
        /// <summary>
        /// 删除信息
        /// </summary>
        /// <param name="ids">要删除的信息编号</param>
        /// <returns>业务操作结果</returns>
        public OperationResult DeleteRegions(params int[] ids)
        {
            return RegionRepository.Delete(ids);
        }

        #endregion
    }
}

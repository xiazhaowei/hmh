using System;
using Hmh.Core.Shop.Dtos;
using Hmh.Core.Shop.Models;
using OSharp.Utility.Data;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Hmh.Core.Identity.Models;


namespace Hmh.Core.Shop
{
    public partial class ShopService
    {
        #region Implementation of IShopContract
        /// <summary>
        /// 获取 合同查询数据集
        /// </summary>
        public IQueryable<Contract> Contracts
        {
            get { return ContractRepository.Entities; }
        }


        /// <summary>
        /// 检查合同信息信息是否存在
        /// </summary>
        /// <param name="predicate">检查谓语表达式</param>
        /// <param name="id">更新的合同信息编号</param>
        /// <returns>合同信息是否存在</returns>
        public bool CheckContractExists(Expression<Func<Contract, bool>> predicate, int id = 0)
        {
            return ContractRepository.CheckExists(predicate, id);
        }


        /// <summary>
        /// 添加合同信息信息
        /// </summary>
        /// <param name="inputDtos">要添加的合同信息DTO信息</param>
        /// <returns>业务操作结果</returns>
        public OperationResult AddContracts(params ContractInputDto[] inputDtos)
        {
            return ContractRepository.Insert(inputDtos,
            dto =>
            {               

            },
            (dto, entity) => {
                if (dto.ShopId.HasValue && dto.ShopId.Value > 0)
                {
                    Hmh.Core.Shop.Models.Shop shop = ShopRepository.GetByKey(dto.ShopId.Value);
                    if (shop == null)
                    {
                        throw new Exception("合同所属店铺不存在");
                    }
                    Contract usingContract = shop.Contracts.FirstOrDefault(c => c.State == ContractState.Using);
                    if (usingContract != null && dto.BeginTime < usingContract.EndTime)
                    {
                        throw new Exception("新合同和执行中的合同时间重合");
                    }
                    shop.CurrentContract = entity;
                    shop.Contracts.Add(entity);
                    entity.Shop = shop;
                }
                else
                {
                    throw new Exception("合同没有归属店铺");
                }                
                return entity;
            });
        }

        /// <summary>
        /// 更新合同信息信息
        /// </summary>
        /// <param name="inputDtos">包含更新信息的合同信息DTO信息</param>
        /// <returns>业务操作结果</returns>
        public OperationResult EditContracts(params ContractInputDto[] inputDtos)
        {
            return ContractRepository.Update(inputDtos,
                (dto, entity) => {
                    if(entity.State==ContractState.Settlling||entity.State==ContractState.Finish)
                    {
                        throw new Exception("合同处于清算阶段或已结束，不能编辑");
                    }
                },
                (dto, entity) => {
                    if (dto.ShopId.HasValue && dto.ShopId.Value > 0)
                    {
                        Hmh.Core.Shop.Models.Shop shop = ShopRepository.GetByKey(dto.ShopId.Value);
                        if (shop == null)
                        {
                            throw new Exception("合同所属店铺不存在");
                        }                      

                        Contract usingContract = shop.Contracts.FirstOrDefault(c => c.State == ContractState.Using);
                        if (usingContract != null && dto.BeginTime < usingContract.EndTime)
                        {
                            throw new Exception("新合同和执行中的合同时间重合");
                        }
                        shop.CurrentContract = entity;
                        shop.Contracts.Add(entity);
                        entity.Shop = shop;
                    }
                    else
                    {
                        throw new Exception("合同没有归属店铺");
                    }                   

                    return entity;
                });
        }

        /// <summary>
        /// 删除合同信息信息
        /// </summary>
        /// <param name="ids">要删除的合同信息编号</param>
        /// <returns>业务操作结果</returns>
        public OperationResult DeleteContracts(params int[] ids)
        {
            return ContractRepository.Delete(ids);
        }

        #endregion
    }
}

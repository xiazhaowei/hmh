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
        #region Implementation of IShopContractPay
        /// <summary>
        /// 获取 查询数据集
        /// </summary>
        public IQueryable<ContractPay> ContractPays
        {
            get { return ContractPayRepository.Entities; }
        }


        /// <summary>
        /// 检查信息是否存在
        /// </summary>
        /// <param name="predicate">检查谓语表达式</param>
        /// <param name="id">更新的信息编号</param>
        /// <returns>信息是否存在</returns>
        public bool CheckContractPayExists(Expression<Func<ContractPay, bool>> predicate, int id = 0)
        {
            return ContractPayRepository.CheckExists(predicate, id);
        }


        /// <summary>
        /// 添加信息信息
        /// </summary>
        /// <param name="inputDtos">要添加的信息DTO信息</param>
        /// <returns>业务操作结果</returns>
        public OperationResult AddContractPays(params ContractPayInputDto[] inputDtos)
        {
            return ContractPayRepository.Insert(inputDtos,
            dto =>
            {                
                
            },
            (dto, entity) => {
                if (dto.ContractId.HasValue && dto.ContractId.Value > 0)
                {
                    Contract contract = ContractRepository.GetByKey(dto.ContractId.Value);
                    if (contract == null)
                    {
                        throw new Exception("所属合同不存在");
                    }
                    contract.ContractPay=entity;
                    entity.Contract = contract;
                }
                else
                {
                    throw new Exception("付款不能没有归属合同");
                }                
                return entity;
            });
        }

        /// <summary>
        /// 更新合同信息信息
        /// </summary>
        /// <param name="inputDtos">包含更新信息的合同信息DTO信息</param>
        /// <returns>业务操作结果</returns>
        public OperationResult EditContractPays(params ContractPayInputDto[] inputDtos)
        {
            return ContractPayRepository.Update(inputDtos,
                (dto, entity) => {
                    
                },
                (dto, entity) => {
                    if (dto.ContractId.HasValue && dto.ContractId.Value > 0)
                    {
                        Contract contract = ContractRepository.GetByKey(dto.ContractId.Value);
                        if (contract == null)
                        {
                            throw new Exception("所属合同不存在");
                        }
                        contract.ContractPay=entity;
                        entity.Contract = contract;
                    }
                    else
                    {
                        throw new Exception("没有归属合同");
                    }                   
                    
                    return entity;
                });
        }

        /// <summary>
        /// 删除信息
        /// </summary>
        /// <param name="ids">要删除的编号</param>
        /// <returns>业务操作结果</returns>
        public OperationResult DeleteContractPays(params int[] ids)
        {
            return ContractPayRepository.Delete(ids);
        }

        #endregion
    }
}

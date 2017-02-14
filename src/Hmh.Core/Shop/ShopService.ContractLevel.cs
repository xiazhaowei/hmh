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
        /// 获取 合同级别查询数据集
        /// </summary>
        public IQueryable<ContractLevel> ContractLevels 
        {
            get { return ContractLevelRepository.Entities; }
        }

        /// <summary>
        /// 检查合同级别信息是否存在
        /// </summary>
        /// <param name="predicate">检查谓语表达式</param>
        /// <param name="id">更新的合同信息编号</param>
        /// <returns>合同信息是否存在</returns>
        public bool CheckContractLevelExists(Expression<Func<ContractLevel, bool>> predicate, int id = 0)
        {
            return ContractLevelRepository.CheckExists(predicate, id);
        }


        /// <summary>
        /// 添加合同级别信息
        /// </summary>
        /// <param name="inputDtos">要添加的合同信息DTO信息</param>
        /// <returns>业务操作结果</returns>
        public OperationResult AddContractLevels(params ContractLevelInputDto[] inputDtos)
        {
            return ContractLevelRepository.Insert(inputDtos,
                dto =>
                {
                    if(ContractLevelRepository.CheckExists(cl=>cl.Name==dto.Name && cl.InitalFee==dto.InitalFee))
                    {
                        throw new Exception("合同级别：{0}，已经存在，不能重复添加");
                    }
                },
                (dto, entity) =>
                {
                    return entity;
                });
        }

        /// <summary>
        /// 更新合同级别信息
        /// </summary>
        /// <param name="inputDtos">包含更新信息的合同信息DTO信息</param>
        /// <returns>业务操作结果</returns>
        public OperationResult EditContractLevels(params ContractLevelInputDto[] inputDtos)
        {
            return ContractLevelRepository.Update(inputDtos,
                (dto, entity) =>
                {
                    if (ContractLevelRepository.CheckExists(cl => cl.Name == dto.Name && cl.InitalFee == dto.InitalFee))
                    {
                        throw new Exception("合同级别：{0}，已经存在，不能重复添加");
                    }
                },
                (dto, entity) =>
                {
                    return entity;
                });

        }
        /// <summary>
        /// 删除合同级别信息
        /// </summary>
        /// <param name="ids">要删除的合同信息编号</param>
        /// <returns>业务操作结果</returns>
        public OperationResult DeleteContractLevels(params int[] ids)
        {
            return ContractLevelRepository.Delete(ids);
        }

        #endregion
    }
}

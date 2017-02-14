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
using OSharp.Core.Mapping;

namespace Hmh.Core.Shop
{
    public partial class ShopService
    {
        #region Implementation of IShopContract
        /// <summary>
        /// 获取 信息查询数据集
        /// </summary>
        public IQueryable<ExpressTemplate> ExpressTemplates
        {
            get { return ExpressTemplateRepository.Entities; }
        }        

        /// <summary>
        /// 添加信息信息
        /// </summary>
        /// <param name="inputDtos">要添加的信息DTO信息</param>
        /// <returns>业务操作结果</returns>
        public async Task<OperationResult> AddExpressTemplates(params ExpressTemplateDto[] inputDtos)
        {
            ExpressTemplateRepository.UnitOfWork.TransactionEnabled = true;
            List<string> names = new List<string>();
            foreach (ExpressTemplateDto dto in inputDtos)
            {
                //ExpressTemplate expressTemplate = dto.MapTo<ExpressTemplate>();
                ExpressTemplate expressTemplate = new ExpressTemplate() {
                    Name=dto.Name,
                    DeliverAddress=dto.DeliverAddress,
                    DeliverTime=dto.DeliverTime,
                    IsFree=dto.IsFree,
                    Count=dto.Count,
                    Price=dto.Price,
                    CountAdd=dto.CountAdd,
                    PriceAdd=dto.PriceAdd
                };

                //店铺
                if (dto.ShopId.HasValue && dto.ShopId.Value > 0)
                {
                    Shop.Models.Shop shop = await ShopRepository.GetByKeyAsync(dto.ShopId.Value);
                    if (shop == null)
                    {
                        return new OperationResult(OperationResultType.QueryNull, "店铺不存在");
                    }
                    expressTemplate.Shop = shop;
                    shop.ExpressTemplates.Add(expressTemplate);
                }                


                //特殊地区
                if (dto.SpecialExpressAddresses.Count > 0)
                {
                    foreach (Hmh.Core.Shop.Dtos.SpecialExpressAddress specialExpressAddressDto in dto.SpecialExpressAddresses)
                    {
                        //Hmh.Core.Shop.Models.SpecialExpressAddress specialExpressAddress = specialExpressAddressDto.MapTo<Hmh.Core.Shop.Models.SpecialExpressAddress>();
                        Hmh.Core.Shop.Models.SpecialExpressAddress specialExpressAddress = new Hmh.Core.Shop.Models.SpecialExpressAddress() {
                            Address=specialExpressAddressDto.Address,
                            Count=specialExpressAddressDto.Count,
                            Price=specialExpressAddressDto.Price,
                            CountAdd=specialExpressAddressDto.CountAdd,
                            PriceAdd=specialExpressAddressDto.PriceAdd
                        };

                        await SpecialExpressAddressRepository.InsertAsync(specialExpressAddress);
                        

                        specialExpressAddress.ExpressTemplate = expressTemplate;

                        expressTemplate.SpecialExpressAddresses.Add(specialExpressAddress);
                    }

                }


                int id = await ExpressTemplateRepository.InsertAsync(expressTemplate);
                //if (!(id > 0))
                //    return new OperationResult(OperationResultType.Error, "添加失败");
                names.Add(expressTemplate.Name);
            }
            return await ExpressTemplateRepository.UnitOfWork.SaveChangesAsync() > 0
                ? new OperationResult(OperationResultType.Success, $"模板“{names.ExpandAndToString()}”创建成功")
                : OperationResult.NoChanged;
        }

        
        /// <summary>
        /// 删除信息信息
        /// </summary>
        /// <param name="ids">要删除的信息编号</param>
        /// <returns>业务操作结果</returns>
        public OperationResult DeleteExpressTemplates(params int[] ids)
        {
            return ExpressTemplateRepository.Delete(ids);
        }

        #endregion
    }
}

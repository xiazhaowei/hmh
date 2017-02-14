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
        public IQueryable<Models.Goods> Goodss 
        {
            get { return GoodsRepository.Entities; }
        }


        /// <summary>
        /// 检查信息信息是否存在
        /// </summary>
        /// <param name="predicate">检查谓语表达式</param>
        /// <param name="id">更新的信息编号</param>
        /// <returns>信息是否存在</returns>
        public bool CheckGoodsExists(Expression<Func<Models.Goods, bool>> predicate, int id = 0)
        {
            return GoodsRepository.CheckExists(predicate, id);
        }

        /// <summary>
        /// 添加店铺信息信息
        /// </summary>
        /// <param name="inputDtos">要添加的店铺信息DTO信息</param>
        /// <returns>业务操作结果</returns>
        public async Task<OperationResult> AddGoodss(params GoodsInputDto[] inputDtos)
        {
            GoodsRepository.UnitOfWork.TransactionEnabled = true;
            List<string> names = new List<string>();
            foreach (GoodsInputDto dto in inputDtos)
            {                
                Models.Goods goods = new Models.Goods()//商品其他属性的绑定
                {
                    Name = dto.Name,
                    GoodsPics=dto.GoodsPics,
                    Description = dto.Description,
                    Detail = dto.Detail,
                    Price = dto.Price,
                    Stock = dto.Stock,
                    GoodsNumber = dto.GoodsNumber,
                    BarCode = dto.BarCode,
                    CreatedTime = DateTime.Now,
                    BeginTime=DateTime.Now,
                    IsGuarantee=dto.IsGuarantee,
                    IsReceipt=dto.IsReceipt,
                    IsSevenDayReplacement=dto.IsSevenDayReplacement,
                    IsReplacement=dto.IsReplacement,                    
                    IsCommend=dto.IsCommend,
                    IsLocked=false
                };

                //店铺
                if (dto.ShopId.HasValue && dto.ShopId.Value > 0)
                {
                    Shop.Models.Shop shop = await ShopRepository.GetByKeyAsync(dto.ShopId.Value);
                    if (shop == null)
                    {
                        return new OperationResult(OperationResultType.QueryNull, "店铺不存在");
                    }
                    goods.Shop = shop;
                    shop.Goodses.Add(goods);
                }

                //分类
                if(dto.CategoryId.HasValue && dto.CategoryId.Value>0)
                {
                    Category category = await CategoryRepository.GetByKeyAsync(dto.CategoryId.Value);
                    if(category==null)
                    {
                        return new OperationResult(OperationResultType.QueryNull, "发布不存在");
                    }
                    goods.Category = category;
                    category.Goodses.Add(goods);
                }

                //运费模板
                if (dto.ExpressTemplateId.HasValue && dto.ExpressTemplateId.Value > 0)
                {
                    Shop.Models.ExpressTemplate expressTemplate = await ExpressTemplateRepository.GetByKeyAsync(dto.ExpressTemplateId.Value);
                    if (expressTemplate == null)
                    {
                        return new OperationResult(OperationResultType.QueryNull, "运费模板不存在");
                    }
                    goods.ExpressTemplate = expressTemplate;
                }

                //商品属性
                if (dto.GoodsAttrs.Count > 0)
                {
                    foreach (Dtos.GoodsAttr goodsAttrDto in dto.GoodsAttrs)
                    {
                        //Hmh.Core.Shop.Models.SpecialExpressAddress specialExpressAddress = specialExpressAddressDto.MapTo<Hmh.Core.Shop.Models.SpecialExpressAddress>();
                        Models.GoodsAttr goodsAttr = new Models.GoodsAttr()
                        {
                            AttrName = goodsAttrDto.AttrName,
                            AttrValue = goodsAttrDto.AttrValue                            
                        };

                        await GoodsAttrRepository.InsertAsync(goodsAttr);

                        goodsAttr.Goods = goods;
                        goods.GoodsAttrs.Add(goodsAttr);
                    }

                }

                //商品Sku
                if(dto.Skus.Count>0)
                {
                    foreach(Dtos.Sku skuDto in dto.Skus)
                    {
                        if(skuDto.Names.Count!=skuDto.Values.Count)
                        {
                            return new OperationResult(OperationResultType.Error, "规格名称和值不对应");
                        }

                        Models.Sku sku = new Models.Sku() { 
                            Names=skuDto.Names.ExpandAndToString(),
                            Values=skuDto.Values.ExpandAndToString(),                         
                            Price=skuDto.Price,
                            Stock=skuDto.Stock,
                            GoodsNumber=skuDto.GoodsNumber,
                            BarCode=skuDto.BarCode,
                            SkuPic=skuDto.SkuPic
                        };                        

                        await SkuRepository.InsertAsync(sku);
                        sku.Goods = goods;
                        goods.Skus.Add(sku);
                    }
                }


                int id = await GoodsRepository.InsertAsync(goods);
                //if (!(id > 0))
                //    return new OperationResult(OperationResultType.Error, "添加失败");
                names.Add(goods.Name);
            }
            return await GoodsRepository.UnitOfWork.SaveChangesAsync() > 0
                ? new OperationResult(OperationResultType.Success, $"商品“{names.ExpandAndToString()}”创建成功")
                : OperationResult.NoChanged;
        }

        /// <summary>
        /// 更新店铺信息信息
        /// </summary>
        /// <param name="inputDtos">包含更新信息的店铺信息DTO信息</param>
        /// <returns>业务操作结果</returns>
        public OperationResult EditGoodss(params GoodsInputDto[] inputDtos)
        {
            return GoodsRepository.Update(inputDtos,
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
        public OperationResult DeleteGoodss(params int[] ids)
        {
            return GoodsRepository.Delete(ids);
        }

        #endregion
    }
}

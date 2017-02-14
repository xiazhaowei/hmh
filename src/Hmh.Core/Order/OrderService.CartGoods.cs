using Hmh.Core.Order.Dtos;
using Hmh.Core.Order.Models;
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

namespace Hmh.Core.Order
{
    public partial class OrderService
    {
        #region Implementation of IOrderContract
        /// <summary>
        /// 获取 查询数据集
        /// </summary>
        public IQueryable<Models.CartGoods> CartGoodses
        {
            get { return CartGoodsRepository.Entities; }
        }


        /// <summary>
        /// 检查信息信息是否存在
        /// </summary>
        /// <param name="predicate">检查谓语表达式</param>
        /// <param name="id">更新的信息编号</param>
        /// <returns>信息是否存在</returns>
        public bool CheckCartGoodses(Expression<Func<CartGoods, bool>> predicate, int id = 0)
        {
            return CartGoodsRepository.CheckExists(predicate, id);
        }

        /// <summary>
        /// 添加信息信息
        /// </summary>
        /// <param name="inputDtos">要添加的店铺信息DTO信息</param>
        /// <returns>业务操作结果</returns>
        public async Task<OperationResult> AddCartGoodses(params CartGoodsInputDto[] inputDtos)
        {
            
            CartGoodsRepository.UnitOfWork.TransactionEnabled = true;
            List<string> names = new List<string>();
            foreach (CartGoodsInputDto dto in inputDtos)
            {
                CartGoods cartGoods = dto.MapTo<CartGoods>();

                if (dto.GoodsId.HasValue && dto.GoodsId.Value > 0)
                {
                    Goods.Models.Goods goods = GoodsRepository.GetByKey(dto.GoodsId.Value);
                    if (goods == null)
                    {
                        throw new Exception("商品不存在");
                    }
                    cartGoods.Goods = goods;
                }

                if (dto.UserId.HasValue && dto.UserId.Value > 0)
                {
                    User user = UserRepository.GetByKey(dto.UserId.Value);
                    if (user == null)
                    {
                        throw new Exception("用户不存在");
                    }
                    cartGoods.User = user;
                }

                if (dto.SkuId.HasValue && dto.SkuId.Value > 0)
                {
                    Goods.Models.Sku sku = SkuRepository.GetByKey(dto.SkuId.Value);
                    if (sku == null)
                    {
                        throw new Exception("Sku不存在");
                    }
                    cartGoods.Sku = sku;
                }

                //判断商品的重复性，如果是相同商品相同规格只改变购物数量
                CartGoods repeatCartGoods = CartGoodsRepository.Entities.SingleOrDefault(cg => cg.User.Id == dto.UserId.Value && cg.Goods.Id == dto.GoodsId.Value && cg.Sku.Id == dto.SkuId.Value);
                if(repeatCartGoods==null)
                {
                    await CartGoodsRepository.InsertAsync(cartGoods);                    
                }
                else
                {
                    repeatCartGoods.BuyCount += dto.BuyCount;
                    await CartGoodsRepository.UpdateAsync(repeatCartGoods);
                }

                names.Add(cartGoods.Name);
            }

            return await CartGoodsRepository.UnitOfWork.SaveChangesAsync() > 0
                            ? new OperationResult(OperationResultType.Success, $"“{names.ExpandAndToString()}”创建成功")
                            : OperationResult.NoChanged;
        }

       


        /// <summary>
        /// 删除信息信息
        /// </summary>
        /// <param name="ids">要删除的店铺信息编号</param>
        /// <returns>业务操作结果</returns>
        public OperationResult DeleteCartGoodses(params int[] ids)
        {
            return CartGoodsRepository.Delete(ids);
        }

        #endregion
    }
}

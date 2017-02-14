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
        public IQueryable<Models.Order> Orders
        {
            get { return OrderRepository.Entities; }
        }


        /// <summary>
        /// 检查信息信息是否存在
        /// </summary>
        /// <param name="predicate">检查谓语表达式</param>
        /// <param name="id">更新的信息编号</param>
        /// <returns>信息是否存在</returns>
        public bool CheckOrders(Expression<Func<Models.Order, bool>> predicate, int id = 0)
        {
            return OrderRepository.CheckExists(predicate, id);
        }

        /// <summary>
        /// 添加信息信息
        /// </summary>
        /// <param name="inputDtos">要添加的店铺信息DTO信息</param>
        /// <returns>业务操作结果</returns>
        public async Task<OperationResult> AddOrders(params OrderInputDto[] inputDtos)
        {
            //开启事务
            OrderRepository.UnitOfWork.TransactionEnabled = true;
            List<string> names = new List<string>();
            foreach (OrderInputDto dto in inputDtos)
            {
                //auto mapper maybe fileter 不能通过自动映射 数据集合也会映射
                //Models.Order order = dto.MapTo<Models.Order>();

                Models.Order order = new Models.Order()
                {
                    ShopName = dto.ShopName,
                    OrderNumber = dto.OrderNumber,
                    Amount = dto.Amount,
                    HPayAmount = dto.HPayAmount,
                    RealAmount = dto.RealAmount,
                    Preferential = dto.Preferential,
                    ExpressFee = dto.ExpressFee,
                    Remark = dto.Remark,
                    State = dto.State
                };

                //店铺
                if (dto.ShopId.HasValue && dto.ShopId.Value > 0)
                {
                    Shop.Models.Shop shop = await ShopRepository.GetByKeyAsync(dto.ShopId.Value);
                    if (shop == null)
                    {
                        return new OperationResult(OperationResultType.QueryNull, "店铺不存在");
                    }
                    order.Shop = shop;
                    shop.Orders.Add(order);
                }

                //用户
                if(dto.UserId.HasValue && dto.UserId.Value>0)
                {
                    User user = await UserRepository.GetByKeyAsync(dto.UserId.Value);
                    if(user==null)
                    {
                        return new OperationResult(OperationResultType.QueryNull,"用户不存在");
                    }
                    order.User = user;
                    user.Orders.Add(order);
                }

                //配送信息
                order.OrderExpress=new OrderExpress()
                {
                    Name = dto.OrderExpress.Name,
                    Region = dto.OrderExpress.Region,
                    DetailAddress = dto.OrderExpress.DetailAddress,
                    Zip = dto.OrderExpress.Zip,
                    Mobile = dto.OrderExpress.Mobile                   
                };

                //商品
                if (dto.OrderGoodses.Count > 0)
                {
                    foreach (OrderGoodsInputDto orderGoodsDto in dto.OrderGoodses)
                    {
                        //auto mapper maybe filter 
                        OrderGoods orderGoods = orderGoodsDto.MapTo<OrderGoods>();                        

                        //处理商品
                        if(orderGoodsDto.GoodsId.HasValue && orderGoodsDto.GoodsId.Value>0)
                        {
                            Goods.Models.Goods goods = await GoodsRepository.GetByKeyAsync(orderGoodsDto.GoodsId.Value);
                            if(goods==null)
                            {
                                throw new Exception("商品不存在");
                            }
                            orderGoods.Goods = goods;
                        }
                        await OrderGoodsRepository.InsertAsync(orderGoods);

                        orderGoods.Order = order;
                        order.OrderGoodses.Add(orderGoods);                        
                    }
                }

                int id = await OrderRepository.InsertAsync(order);                
                names.Add(order.OrderNumber);
            }
            return await OrderRepository.UnitOfWork.SaveChangesAsync() > 0
                ? new OperationResult(OperationResultType.Success, $"订单“{names.ExpandAndToString()}”创建成功")
                : OperationResult.NoChanged;
        }

       


        /// <summary>
        /// 删除信息信息
        /// </summary>
        /// <param name="ids">要删除的店铺信息编号</param>
        /// <returns>业务操作结果</returns>
        public OperationResult DeleteOrders(params int[] ids)
        {
            return OrderRepository.Delete(ids);
        }

        #endregion
    }
}

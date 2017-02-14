
using Hmh.Core.Identity.Models;
using Hmh.Core.Shop.Models;
using OSharp.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hmh.Core.Order.Models;

namespace Hmh.Core.Order
{
    /// <summary>
    /// 业务实现-店铺模块
    /// </summary>
    public partial class OrderService:IOrderContract
    {
        /// <summary>
        /// 获取设置 用户信息仓储操作对象
        /// </summary>
        public IRepository<User, int> UserRepository { protected get; set; }
        /// <summary>
        /// 获取设置 用户信息仓储操作对象
        /// </summary>
        public IRepository<Models.Order, int> OrderRepository { protected get; set; }

        /// <summary>
        /// 获取设置 用户信息仓储操作对象
        /// </summary>
        public IRepository<OrderExpress, int> OrderExpressRepository { protected get; set; }

        /// <summary>
        /// 获取或设置 仓储操作对象
        /// </summary>
        public IRepository<Goods.Models.Goods, int> GoodsRepository { protected get; set; }

        /// <summary>
        /// 获取或设置 仓储操作对象
        /// </summary>
        public IRepository<CartGoods, int> CartGoodsRepository { protected get; set; }

        /// <summary>
        /// 获取或设置 仓储操作对象
        /// </summary>
        public IRepository<OrderGoods,int> OrderGoodsRepository { protected get; set; }
        /// <summary>
        /// 获取或设置 仓储操作对象
        /// </summary>
        public IRepository<Shop.Models.Shop, int> ShopRepository { protected get; set; }
        

        /// <summary>
        /// 获取设置 仓储操作对象
        /// </summary>
        public IRepository<Goods.Models.Sku, int> SkuRepository { protected get; set; }
        
    }
}

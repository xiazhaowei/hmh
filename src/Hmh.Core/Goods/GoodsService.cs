
using Hmh.Core.Identity.Models;
using Hmh.Core.Shop.Models;
using OSharp.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hmh.Core.Goods.Models;

namespace Hmh.Core.Goods
{
    /// <summary>
    /// 业务实现-店铺模块
    /// </summary>
    public partial class GoodsService:IGoodsContract
    {
        /// <summary>
        /// 获取设置 用户信息仓储操作对象
        /// </summary>
        public IRepository<User, int> UserRepository { protected get; set; }

        /// <summary>
        /// 获取或设置 仓储操作对象
        /// </summary>
        public IRepository<Models.Goods, int> GoodsRepository { protected get; set; }

        /// <summary>
        /// 获取或设置 仓储操作对象
        /// </summary>
        public IRepository<Models.GoodsComment,int> GoodsCommentRepository { protected get; set; }

        /// <summary>
        /// 获取或设置 仓储操作对象
        /// </summary>
        public IRepository<Shop.Models.Shop, int> ShopRepository { protected get; set; }
        /// <summary>
        /// 获取或设置 仓储操作对象
        /// </summary>
        public IRepository<Category, int> CategoryRepository { protected get; set; }

        /// <summary>
        /// 获取或设置 仓储操作对象
        /// </summary>
        public IRepository<Shop.Models.ExpressTemplate, int> ExpressTemplateRepository { protected get; set; }
        /// <summary>
        /// 获取或设置 仓储操作对象
        /// </summary>
        public IRepository<Attr, int> AttrRepository { protected get; set; }

        /// <summary>
        /// 获取设置 仓储操作对象
        /// </summary>
        public IRepository<GoodsSpecification, int> GoodsSpecificationRepository { protected get; set; }

        /// <summary>
        /// 获取设置 仓储操作对象
        /// </summary>
        public IRepository<GoodsColorSpecificationItem, int> GoodsColorSpecificationItemRepository { protected get; set; }

        /// <summary>
        /// 获取设置 仓储操作对象
        /// </summary>
        public IRepository<ShowCategory, int> ShowCategoryRepository { protected get; set; }

        /// <summary>
        /// 获取设置 仓储操作对象
        /// </summary>
        public IRepository<Sku, int> SkuRepository { protected get; set; }

        /// <summary>
        /// 获取设置 仓储操作对象
        /// </summary>
        public IRepository<GoodsAttr, int> GoodsAttrRepository { protected get; set; }
    }
}

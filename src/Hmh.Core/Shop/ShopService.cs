
using Hmh.Core.Identity.Models;
using Hmh.Core.Shop.Models;
using OSharp.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hmh.Core.Shop
{
    /// <summary>
    /// 业务实现-店铺模块
    /// </summary>
    public partial class ShopService:IShopContract
    {
        /// <summary>
        /// 获取或设置 店铺信息仓储操作对象
        /// </summary>
        public IRepository<Hmh.Core.Shop.Models.Shop, int> ShopRepository { protected get; set; }


        /// <summary>
        /// 获取或设置 合同信息仓储操作对象
        /// </summary>
        public IRepository<Contract, int> ContractRepository { protected get; set; }


        /// <summary>
        /// 获取或设置 合同信息仓储操作对象
        /// </summary>
        public IRepository<ShopPermit, int> ShopPermitRepository { protected get; set; }

        /// <summary>
        /// 获取设置 合同级别信息仓储操作对象
        /// </summary>
        public IRepository<ContractLevel, int> ContractLevelRepository { protected get; set; }

        /// <summary>
        /// 获取或设置 运费模板信息仓储操作对象
        /// </summary>
        public IRepository<ExpressTemplate, int> ExpressTemplateRepository { protected get; set; }
        /// <summary>
        /// 获取或设置 特殊运费模板信息仓储操作对象
        /// </summary>
        public IRepository<SpecialExpressAddress, int> SpecialExpressAddressRepository { protected get; set; }

        /// <summary>
        /// 获取设置 信息仓储操作对象
        /// </summary>
        public IRepository<ContractPay, int> ContractPayRepository { protected get; set; }
        /// <summary>
        /// 获取设置 用户信息仓储操作对象
        /// </summary>
        public IRepository<User, int> UserRepository { protected get; set; }

        /// <summary>
        /// 获取设置 地区信息仓储操作对象
        /// </summary>
        public IRepository<Region, int> RegionRepository { protected get; set; }
    }
}

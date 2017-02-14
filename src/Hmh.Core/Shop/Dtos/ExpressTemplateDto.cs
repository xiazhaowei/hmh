using OSharp.Core.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hmh.Core.Shop.Dtos
{
    public class ExpressTemplateDto: IInputDto<int>
    {
        /// <summary>
        /// 获取或设置 主键，唯一标识
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 获取或设置 店铺Id
        /// </summary>
        public int? ShopId { get; set; }

       
        public string Name { get; set; }

        /// <summary>
        /// 获取或设置 发货地址
        /// </summary>
        public string DeliverAddress { get; set; }

        /// <summary>
        /// 获取或设置 发货时间
        /// </summary>
        public string DeliverTime { get; set; }

        /// <summary>
        /// 获取或设置 是否包邮
        /// </summary>
        public bool IsFree { get; set; }

        /// <summary>
        /// 获取或设置  件内
        /// </summary>
        public int Count { get; set; }

        /// <summary>
        /// 获取或设置 运费
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// 获取或设置 续件
        /// </summary>
        public int CountAdd { get; set; }

        /// <summary>
        /// 获取或设置 续费
        /// </summary>
        public decimal PriceAdd { get; set; }

        /// <summary>
        /// 获取或设置 特殊地区
        /// </summary>
        public ICollection<SpecialExpressAddress> SpecialExpressAddresses { get; set; } 

        public ExpressTemplateDto()
        {
            SpecialExpressAddresses = new List<SpecialExpressAddress>();
        }
    }
   

    public class SpecialExpressAddress :IInputDto<int>
    {
        /// <summary>
        /// 获取或设置 主键，唯一标识
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 获取或设置 地区
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// 获取或设置  件内
        /// </summary>
        public int Count { get; set; }

        /// <summary>
        /// 获取或设置 运费
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// 获取或设置 续件
        /// </summary>
        public int CountAdd { get; set; }

        /// <summary>
        /// 获取或设置 续费
        /// </summary>
        public decimal PriceAdd { get; set; }
    }
}

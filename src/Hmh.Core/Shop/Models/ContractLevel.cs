using OSharp.Core.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hmh.Core.Shop.Models
{
    /// <summary>
    /// 实体类---合同基本
    /// </summary>
    [Description("店铺-合同级别信息")]
    public class ContractLevel : EntityBase<int>
    {        

        /// <summary>
        /// 级别名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 获取设置 加盟费
        /// </summary>
        [Required]
        public decimal InitalFee { get; set; }

        /// <summary>
        /// 获取设置  H币消费限制
        /// </summary>
        [Required]
        public decimal HCoinLimit { get; set; }

        
    }
}

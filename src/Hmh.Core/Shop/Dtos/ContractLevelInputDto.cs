using OSharp.Core.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hmh.Core.Shop.Dtos
{
    public class ContractLevelInputDto:IInputDto<int>
    {
        /// <summary>
        /// 获取或设置 主键，唯一标识
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 级别名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 获取设置 加盟费
        /// </summary>
        [Required]
        public int InitalFee { get; set; }

        /// <summary>
        /// 获取设置  H币消费限制
        /// </summary>
        [Required]
        public int HCoinLimit { get; set; }
    }
}

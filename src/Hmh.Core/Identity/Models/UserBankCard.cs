using OSharp.Core.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hmh.Core.Identity.Models
{
    [Description("认证-银行卡信息")]
    public class UserBankCard:EntityBase<int>,ICreatedTime
    {

        /// <summary>
        /// 获取设置 用户信息
        /// </summary>
        public virtual User User { get; set; }

        /// <summary>
        /// 获取设置 持卡人姓名
        /// </summary>
        [Required][StringLength(20)]
        public string UserName { get; set; }

        /// <summary>
        /// 获取设置 银行名 工行|支付宝
        /// </summary>
        [Required][StringLength(20)]
        public string BankName { get; set; }

        /// <summary>
        /// 获取设置 卡号
        /// </summary>
        [Required][StringLength(20)]
        public string CardNumber { get; set; }

        /// <summary>
        /// 获取或设置 信息创建时间
        /// </summary>
        public DateTime CreatedTime { get; set; }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hmh.Core.Identity.Dtos
{
    public class TransactionInfo
    {
        public int UserId { get; set; }
        /// <summary>
        /// 获取或设置 提款金额
        /// </summary>
        public decimal Amount { get; set; }

        public string Password { get; set; }

        /// <summary>
        /// 对方账户信息 
        /// </summary>
        public string OtherSizeUserName { get; set; }
    }
}

using Hmh.Core.Identity.Models;
using OSharp.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hmh.Core.ModelConfigurations.Identity
{
    public class UserBankCardConfiguration : EntityConfigurationBase<UserBankCard, Int32>
    {
        public UserBankCardConfiguration()
        {
            UserBankCardConfigurationAppend();
        }

        void UserBankCardConfigurationAppend()
        {
            HasRequired(ub => ub.User).WithMany(us => us.BankCards);
        }
    }
}

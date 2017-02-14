using Hmh.Core.Identity.Models;
using OSharp.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hmh.Core.ModelConfigurations.Identity
{
    public class HCoinTransactionConfiguration : EntityConfigurationBase<HCoinTransaction, Int32>
    {
        public HCoinTransactionConfiguration()
        {
            HCoinTransactionConfigurationAppend();
        }

        void HCoinTransactionConfigurationAppend()
        {
            HasRequired(ub => ub.User).WithMany(us => us.HCoinTransactions);
        }
    }
}

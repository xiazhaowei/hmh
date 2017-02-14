using Hmh.Core.Identity.Models;
using OSharp.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hmh.Core.ModelConfigurations.Identity
{
    public class RmbCoinTransactionConfiguration : EntityConfigurationBase<RmbCoinTransaction, Int32>
    {
        public RmbCoinTransactionConfiguration()
        {
            RmbCoinTransactionConfigurationAppend();
        }

        void RmbCoinTransactionConfigurationAppend()
        {
            HasRequired(ub => ub.User).WithMany(us => us.RmbCoinTransactions);
        }
    }
}

using Hmh.Core.Shop.Models;
using OSharp.Data.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Hmh.Core.ModelConfigurations.Shop
{
    public class ContractPayConfiguration : EntityConfigurationBase<ContractPay, Int32>
    {
        public ContractPayConfiguration()
        {
            ContractPayConfigurationAppend();
        }

        void ContractPayConfigurationAppend()
        {
            Property(ud => ud.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            HasRequired(cp => cp.Contract).WithOptional(c => c.ContractPay);
        }
    }
}

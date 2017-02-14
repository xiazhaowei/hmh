using Hmh.Core.Shop.Models;
using OSharp.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Hmh.Core.ModelConfigurations.Shop
{
    public class ContractConfiguration : EntityConfigurationBase<Contract, Int32>
    {
        public ContractConfiguration()
        {
            ContractConfigurationAppend();
        }

        void ContractConfigurationAppend()
        {
            HasRequired(c => c.Shop).WithMany(s => s.Contracts);            
        }
    }
}

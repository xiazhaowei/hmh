using Hmh.Core.Shop.Models;
using OSharp.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hmh.Core.ModelConfigurations.Shop
{
    public class ContractLevelConfigration:EntityConfigurationBase<ContractLevel, Int32>
    {
        public ContractLevelConfigration()
        {
            ContractLevelConfigurationAppend();
        }

        void ContractLevelConfigurationAppend()
        {

        }
    }
}

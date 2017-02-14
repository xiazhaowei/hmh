using Hmh.Core.Shop.Models;
using OSharp.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Hmh.Core.ModelConfigurations.Shop
{
    public class ExpressTemplateConfiguration : EntityConfigurationBase<ExpressTemplate, Int32>
    {
        public ExpressTemplateConfiguration()
        {
            ExpressTemplateConfigurationAppend();
        }

        void ExpressTemplateConfigurationAppend()
        {
            HasRequired(c => c.Shop).WithMany(s => s.ExpressTemplates);            
        }
    }

    public class SpecialExpressAddressConfiguration : EntityConfigurationBase<SpecialExpressAddress, Int32>
    {
        public SpecialExpressAddressConfiguration()
        {
            SpecialExpressAddressConfigurationAppend();
        }

        void SpecialExpressAddressConfigurationAppend()
        {
            HasRequired(c => c.ExpressTemplate).WithMany(s => s.SpecialExpressAddresses).WillCascadeOnDelete(true);
        }
    }
}

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
    public class ShopPermitConfiguration : EntityConfigurationBase<ShopPermit, Int32>
    {
        public ShopPermitConfiguration()
        {
            ShopPermitConfigurationAppend();
        }

        void ShopPermitConfigurationAppend()
        {
            Property(sp => sp.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            HasRequired(sp => sp.Shop).WithOptional(sp=>sp.ShopPermit);
        }
    }
}

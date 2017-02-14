using OSharp.Data.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Hmh.Core.ModelConfigurations.Shop
{
    public class ShopConfiguration : EntityConfigurationBase<Hmh.Core.Shop.Models.Shop, Int32>
    {
        public ShopConfiguration()
        {
            ShopConfigurationAppend();
        }

        void ShopConfigurationAppend()
        {
            Property(s => s.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            HasRequired(m => m.User).WithOptional(u=>u.Shop);
        }
    }
}

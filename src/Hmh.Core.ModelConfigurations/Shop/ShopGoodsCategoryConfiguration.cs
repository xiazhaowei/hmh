using OSharp.Data.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Hmh.Core.ModelConfigurations.Shop
{
    public class ShopGoodsCategoryConfiguration : EntityConfigurationBase<Hmh.Core.Shop.Models.ShopGoodsCategory, Int32>
    {
        public ShopGoodsCategoryConfiguration()
        {
            ShopGoodsCategoryConfigurationAppend();
        }

        void ShopGoodsCategoryConfigurationAppend()
        {
            HasRequired(a => a.Shop).WithMany(s => s.ShopGoodsCategoryes);
        }
    }
}

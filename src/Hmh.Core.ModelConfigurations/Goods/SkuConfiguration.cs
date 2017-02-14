using Hmh.Core.Goods.Models;
using OSharp.Data.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Hmh.Core.ModelConfigurations.Shop
{
    public class SkuConfiguration : EntityConfigurationBase<Goods.Models.Sku, Int32>
    {
        public SkuConfiguration()
        {
            SkuConfigurationAppend();
        }

        void SkuConfigurationAppend()
        {
            HasRequired(sku=> sku.Goods).WithMany(g => g.Skus);
        }
    }
}

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
    public class GoodsConfiguration : EntityConfigurationBase<Goods.Models.Goods, Int32>
    {
        public GoodsConfiguration()
        {
            GoodsConfigurationAppend();
        }

        void GoodsConfigurationAppend()
        {
            HasRequired(g => g.Category).WithMany(c => c.Goodses);
            HasRequired(g => g.Shop).WithMany(s => s.Goodses);
            HasMany(g => g.Skus).WithRequired(sku => sku.Goods).WillCascadeOnDelete(true);
            HasMany(g => g.GoodsAttrs).WithRequired(ga => ga.Goods).WillCascadeOnDelete(true);
            HasRequired(g => g.ExpressTemplate);
        }
    }
}

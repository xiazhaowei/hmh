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
    public class GoodsSpecificationConfiguration : EntityConfigurationBase<Goods.Models.GoodsSpecification, Int32>
    {
        public GoodsSpecificationConfiguration()
        {
            GoodsSpecificationConfigurationAppend();
        }

        void GoodsSpecificationConfigurationAppend()
        {
            HasRequired(a => a.Category).WithMany(c => c.GoodsSpecifications);
        }
    }
}

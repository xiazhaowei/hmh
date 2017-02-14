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
    public class GoodsAttrConfiguration : EntityConfigurationBase<Goods.Models.GoodsAttr, Int32>
    {
        public GoodsAttrConfiguration()
        {
            GoodsAttrConfigurationAppend();
        }

        void GoodsAttrConfigurationAppend()
        {
            HasRequired(ga => ga.Goods).WithMany(g => g.GoodsAttrs);
        }
    }
}

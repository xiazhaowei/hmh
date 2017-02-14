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
    public class GoodsColorSpecificationItemConfiguration : EntityConfigurationBase<Goods.Models.GoodsColorSpecificationItem, Int32>
    {
        public GoodsColorSpecificationItemConfiguration()
        {
            GoodsColorSpecificationItemConfigurationAppend();
        }

        void GoodsColorSpecificationItemConfigurationAppend()
        {
           
        }
    }
}

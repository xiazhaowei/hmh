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
    public class CartGoodsConfiguration : EntityConfigurationBase<Order.Models.CartGoods, Int32>
    {
        public CartGoodsConfiguration()
        {
            CartGoodsConfigurationAppend();
        }

        void CartGoodsConfigurationAppend()
        {
            HasRequired(cg => cg.User).WithMany(u => u.CartGoodses); 
            HasRequired(cg=>cg.Goods);           
        }
    }
}

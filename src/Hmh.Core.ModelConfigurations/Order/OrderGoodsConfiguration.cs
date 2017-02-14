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
    public class OrderGoodsConfiguration : EntityConfigurationBase<Order.Models.OrderGoods, Int32>
    {
        public OrderGoodsConfiguration()
        {
            OrderGoodsConfigurationAppend();
        }

        void OrderGoodsConfigurationAppend()
        {
            HasRequired(og => og.Order).WithMany(o => o.OrderGoodses);         
                      
            HasRequired(og=>og.Goods);
        }
    }
}

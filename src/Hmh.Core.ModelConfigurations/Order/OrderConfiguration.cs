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
    public class OrderConfiguration : EntityConfigurationBase<Order.Models.Order, Int32>
    {
        public OrderConfiguration()
        {
            OrderConfigurationAppend();
        }

        void OrderConfigurationAppend()
        {
            HasRequired(o => o.User).WithMany(u => u.Orders);
            HasRequired(o => o.Shop).WithMany(s => s.Orders);
            HasMany(o => o.OrderGoodses).WithRequired(og=>og.Order).WillCascadeOnDelete(true);           
            
        }
    }
}

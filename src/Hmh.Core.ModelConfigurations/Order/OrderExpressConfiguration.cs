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
    public class OrderExpressConfiguration : EntityConfigurationBase<Order.Models.OrderExpress, Int32>
    {
        public OrderExpressConfiguration()
        {
            OrderExpressConfigurationAppend();
        }

        void OrderExpressConfigurationAppend()
        {
            Property(s => s.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            HasRequired(og => og.Order).WithRequiredDependent(o => o.OrderExpress);            
        }
    }
}

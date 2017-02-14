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
    public class AttrConfiguration : EntityConfigurationBase<Goods.Models.Attr, Int32>
    {
        public AttrConfiguration()
        {
            AttrConfigurationAppend();
        }

        void AttrConfigurationAppend()
        {
            HasRequired(a => a.Category).WithMany(c => c.Attrs);
        }
    }
}

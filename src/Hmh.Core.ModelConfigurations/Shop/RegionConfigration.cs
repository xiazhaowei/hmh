using Hmh.Core.Shop.Models;
using OSharp.Data.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hmh.Core.ModelConfigurations.Shop
{
    public class RegionConfigration:EntityConfigurationBase<Region, Int32>
    {
        public RegionConfigration()
        {
            RegionConfigurationAppend();
        }

        void RegionConfigurationAppend()
        {
            Property(s => s.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
        }
    }
}

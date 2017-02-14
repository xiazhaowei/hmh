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
    public class ShowCategoryConfiguration : EntityConfigurationBase<Goods.Models.ShowCategory, Int32>
    {
        public ShowCategoryConfiguration()
        {
            ShowCategoryConfigurationAppend();
        }

        void ShowCategoryConfigurationAppend()
        {
            HasOptional(c => c.Parent).WithMany(c => c.Children);
        }
    }
}

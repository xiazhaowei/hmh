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
    public class SearchKeyConfiguration : EntityConfigurationBase<Goods.Models.SearchKey, Int32>
    {
        public SearchKeyConfiguration()
        {
            SearchKeyConfigurationAppend();
        }

        void SearchKeyConfigurationAppend()
        {
            
        }
    }
}

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
    public class GoodsCommentConfiguration : EntityConfigurationBase<Goods.Models.GoodsComment, Int32>
    {
        public GoodsCommentConfiguration()
        {
            GoodsCommentConfigurationAppend();
        }

        void GoodsCommentConfigurationAppend()
        {
            HasRequired(ga => ga.Goods).WithMany(g => g.GoodsComments);
        }
    }
}

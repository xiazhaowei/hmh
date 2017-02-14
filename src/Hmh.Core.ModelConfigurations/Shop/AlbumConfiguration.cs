using OSharp.Data.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Hmh.Core.ModelConfigurations.Shop
{
    public class AlbumConfiguration : EntityConfigurationBase<Hmh.Core.Shop.Models.Album, Int32>
    {
        public AlbumConfiguration()
        {
            AlbumConfigurationAppend();
        }

        void AlbumConfigurationAppend()
        {
            HasRequired(a => a.Shop).WithMany(s => s.Albums);
        }
    }
}

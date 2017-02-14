using Hmh.Core.Identity.Models;
using OSharp.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hmh.Core.ModelConfigurations.Identity
{
    public class CollectConfiguration : EntityConfigurationBase<Collect, Int32>
    {
        public CollectConfiguration()
        {
            CollectConfigurationAppend();
        }

        void CollectConfigurationAppend()
        {
            
        }
    }
}

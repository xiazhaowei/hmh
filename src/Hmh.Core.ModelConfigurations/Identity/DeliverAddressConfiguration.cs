using Hmh.Core.Identity.Models;
using OSharp.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hmh.Core.ModelConfigurations.Identity
{
    public class DeliverAddressConfiguration : EntityConfigurationBase<DeliverAddress, Int32>
    {
        public DeliverAddressConfiguration()
        {
            DeliverAddressConfigurationAppend();
        }

        void DeliverAddressConfigurationAppend()
        {
            HasRequired(dv => dv.User).WithMany(us => us.DeliverAddresses);
        }
    }
}

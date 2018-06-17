using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PasswordKnightRest
{
    public class BreachLookupModel : TableEntity
    {
        public String Domain { get; set; }
        public String Title { get; set; }
        public String Description { get; set; }

        public BreachLookupModel()
        {
        }
    }
}

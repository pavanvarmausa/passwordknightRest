using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PasswordKnightRest
{
    public class BreachEntity : TableEntity
    {
        public BreachEntity() { }

        public BreachEntity(string breachName, string email, string domain)
        {
            Domain = domain;
            PartitionKey = breachName;
            RowKey = email;
        }

        public String Domain;
        public String Breach => PartitionKey;
        public String Email => RowKey;
    }
}

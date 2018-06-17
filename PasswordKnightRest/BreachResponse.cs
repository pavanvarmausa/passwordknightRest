using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PasswordKnightRest
{
    public class BreachResponse
    {
        public string Title { get; set; }
            public string Name { get; set; }
            public string Domain { get; set; }
            public string Description { get; set; }
            public List<string> DataClasses { get; set; }
            public Boolean isVerified { get; set; }

        public BreachResponse(string name, string domain, string description)
        {
            Name = name;
            Domain = domain;
            Description = description;
            DataClasses = new List<string>(new string[] { "Email", "Passwords", "Private Data" });
            Title = name;
            isVerified = true;
        }
        
    }
}

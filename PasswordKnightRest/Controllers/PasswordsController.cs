using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;

namespace PasswordKnightRest.Controllers
{
    [Produces("application/json")]
    [Route("api/Passwords")]
    public class PasswordsController : Controller
    {
        // GET: api/Passwords
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Passwords/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(String id)
        {
            CloudStorageAccount storageAccount =
                 CloudStorageAccount.
             Parse("DefaultEndpointsProtocol=https;AccountName=appserversdiag232;AccountKey=yXX7DE/aWLwQFTtx9Pc1PXRLs+iv2RSDT9lowMQqh9O2T8DwIaB1DRjuTeBp9x8uWPTx61sy5rMI+rWUyqj/rA ==");
            

            // Create the table client.
            CloudTableClient tableClient = storageAccount.CreateCloudTableClient();
            // Get a reference to a table named "peopleTable".
            CloudTable cloudTable = tableClient.GetTableReference("ClearTextPasswordsTable");

            //TableQuery<DynamicTableEntity> projectionQuery = new TableQuery<DynamicTableEntity>().Select(new string[] { "Email" });

            //EntityResolver<string> resolver = (pk, rk, ts, props, etag) => props.ContainsKey("Email") ? props["Email"].StringValue : null;
            String response = "";

            
                var retrievedResult = cloudTable.ExecuteAsync(TableOperation.Retrieve("password",
                                                                                  id));
                if (retrievedResult.Result.HttpStatusCode != StatusCodes.Status404NotFound)
                {
                    response = "Exists";
                }

            Request.HttpContext.Response.Headers.Add("Access-Control-Allow-Origin", "*");

            return response;
        }
        
        // POST: api/Passwords
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }
        
        // PUT: api/Passwords/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }
        
        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}

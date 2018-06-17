using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using Newtonsoft.Json;

namespace PasswordKnightRest.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        // GET api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(String id)
        {
            CloudStorageAccount storageAccount =
                 CloudStorageAccount.
             Parse("DefaultEndpointsProtocol=https;AccountName=appserversdiag232;AccountKey=yXX7DE/aWLwQFTtx9Pc1PXRLs+iv2RSDT9lowMQqh9O2T8DwIaB1DRjuTeBp9x8uWPTx61sy5rMI+rWUyqj/rA ==");

            List<String> breachMasterList = new List<string>(new string[] { "Adobe", "Myspace" });

            // Create the table client.
            CloudTableClient tableClient = storageAccount.CreateCloudTableClient();
            // Get a reference to a table named "peopleTable".
            CloudTable cloudTable = tableClient.GetTableReference("EmailBreachTable");
            CloudTable lookupCloudTable = tableClient.GetTableReference("BreachLookupTable");

            //TableQuery<DynamicTableEntity> projectionQuery = new TableQuery<DynamicTableEntity>().Select(new string[] { "Email" });

            //EntityResolver<string> resolver = (pk, rk, ts, props, etag) => props.ContainsKey("Email") ? props["Email"].StringValue : null;
            List<BreachResponse> response = new List<BreachResponse>();

            Parallel.ForEach(breachMasterList, breach =>
            {
                var retrievedResult = cloudTable.ExecuteAsync(TableOperation.Retrieve(breach,
                                                                                  id));

                

                if (retrievedResult.Result.HttpStatusCode != StatusCodes.Status404NotFound)
                {
                    //var breaches = new[]{
                    //    new { breachName = "Adobe", breachDescription = "Adobe Description" } };

                    var breachLookup = lookupCloudTable.ExecuteAsync(TableOperation.Retrieve<BreachLookupModel>("Breach", breach));
                    BreachLookupModel dataobject = (BreachLookupModel)breachLookup.Result.Result;

                    if (dataobject != null)
                    {
                        response.Add(new BreachResponse(dataobject.RowKey, dataobject.Domain, dataobject.Description));
                    }
                    //Json(breaches);
                }
            });

            if (response.Count == 0) {
                    Request.HttpContext.Response.StatusCode = StatusCodes.Status404NotFound;
            }

            Request.HttpContext.Response.Headers.Add("Access-Control-Allow-Origin", "*");

            return JsonConvert.SerializeObject(response);
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}

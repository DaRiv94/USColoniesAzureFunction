using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Microsoft.Azure.WebJobs.Host;


namespace ColonialUnitedStates
{
    public static class USColonyApi
    {

        [FunctionName("AddUSColony")]
        public static async Task<IActionResult> AddUSColony(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "add")]HttpRequest req,
            [Table("UsColonies",  Connection = "AzureWebJobsStorage")] IAsyncCollector<USColonyTableEntity> USColonyTable,
            TraceWriter log)
        {
            log.Info("adding a new todo USColony");
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var input = JsonConvert.DeserializeObject<USColonyAddEntity>(requestBody);

            var UScolony = new USColony()
            {
                Id = input.NumberToJoinTheUnion.ToString(),
                Name = input.Name,
                Capital=input.Capital,
                YearEstablished= input.YearEstablished,
                NumberToJoinTheUnion=input.NumberToJoinTheUnion,

            };
            await USColonyTable.AddAsync(UScolony.ToTableEntity());
            return new OkObjectResult(UScolony);
        }




        [FunctionName("GetUSColonyById")]
        public static IActionResult GetUsColonyById(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "uscolony/{id}")]HttpRequest req,
            [Table("UsColonies", "USColonies", "{id}", Connection = "AzureWebJobsStorage")] USColonyTableEntity usColony,
            TraceWriter log, string id)
        {
            log.Info("Getting todo item by id");
            if (usColony == null)
            {
                log.Info($"Item {id} not found");
                return new NotFoundResult();
            }
            return new OkObjectResult(usColony.ToUSColony());
        }


    }
}

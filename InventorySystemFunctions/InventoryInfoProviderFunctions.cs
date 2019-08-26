using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Text;
using Microsoft.EntityFrameworkCore;
using InventorySystemFunctions.Entities.Implementations;
using System.Linq;
using InventorySystemFunctions.Models;

namespace InventorySystemFunctions
{
    public static class InventoryInfoProviderFunctions
    {
        [FunctionName("InventoryInfoProviderFunctions")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("Inventory Info Provider Service Started ... " + DateTime.Now.ToString());

            var result = default(IActionResult);

            try
            {
                var inventoryId = int.Parse(req.Query["inventoryId"]);

                var encodedConnectionString = Environment.GetEnvironmentVariable("InventoriesDbConnectionString");
                var connectionString = Encoding.ASCII.GetString(
                    Convert.FromBase64String(encodedConnectionString));

                var contextOptionsBuilder = new DbContextOptionsBuilder<InventoryContext>();

                contextOptionsBuilder.UseSqlServer(connectionString);

                using (var context = new InventoryContext(contextOptionsBuilder.Options))
                {
                    var filteredInventory =
                        context
                        .Inventories
                        .Where(inventory => inventory.ProductId.Equals(inventoryId))
                        .FirstOrDefault();

                    if (filteredInventory == default(Inventory))
                        result = new NotFoundResult();
                    else
                        result = new OkObjectResult(filteredInventory);
                }
            }
            catch (Exception exceptionObject)
            {
                log.LogError(exceptionObject, exceptionObject.Message);

                result = new BadRequestResult();
            }

            log.LogInformation("Inventory Info Provider Service Completed ... " + DateTime.Now.ToString());

            return result;

        }
    }
}

using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using func_snpasswordreset_kamal.Models;

namespace func_snpasswordreset_kamal
{
    public static class LanidPasswordResetMock
    {
        [FunctionName("LanidPasswordResetMock")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            string lanid = req.Query["Lanid"];
            string password = req.Query["password"];
            string domain = req.Query["domain"];

            var response = new SNPasswordResetResponse();
            response.Response = "Success";
            response.ErrorMessage = null;
            response.ErrorCode = null;

            return new OkObjectResult(domain);
        }
    }
}

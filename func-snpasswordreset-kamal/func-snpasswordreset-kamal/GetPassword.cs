using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using func_snpasswordreset.Helper;
using func_snpasswordreset_kamal.Entity;
using func_snpasswordreset_kamal.Helper;
using func_snpasswordreset_kamal.Models;

namespace func_snpasswordreset_kamal
{
    public static class GetPassword
    {
        [FunctionName("GetPassword")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            var response = new PasswordResponse();
            try
            {
                log.LogInformation("C# HTTP trigger function processed a request.");

                string token = req.Query["token"];
                string email = req.Query["email"];

                var storageConnectionString = Environment.GetEnvironmentVariable("AzureTableConnectionString");
                var tableName = Environment.GetEnvironmentVariable("AzureTableName");
                var key = Environment.GetEnvironmentVariable("EncryptionKey");

                var record = await AzureTableHelper.GetEntity<LanIdPassword>(storageConnectionString, tableName, email, token);

                var password = CryptographyHelper.DecryptString(key, record.EncryptedPassword);

                
                response.Password = password;

                return new OkObjectResult(response);
            }
            catch(Exception ex)
            {
                response.ErrorMessage = ex.Message;

                return new OkObjectResult(response);
            }
          
        }
    }
}

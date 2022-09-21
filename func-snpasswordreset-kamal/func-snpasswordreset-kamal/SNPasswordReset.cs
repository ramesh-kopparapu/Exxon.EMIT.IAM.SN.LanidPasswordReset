using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using func_snpasswordreset_kamal.Helper;
using func_snpasswordreset_kamal.Models;
using System.Net.Http;
using func_snpasswordreset_kamal.Entity;
using func_snpasswordreset.Helper;
using System.Linq;
using func_snpasswordreset_kamal.Business;

namespace func_snpasswordreset_kamal
{
    public static class SNPasswordReset
    {
        [FunctionName("snpasswordreset")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "Get","post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");
            PasswordResetResponse responseMessage = new PasswordResetResponse();
            //string lanid = req.Query["name"];
            //return new OkObjectResult(lanid);
            try
            {
                var Url = Environment.GetEnvironmentVariable("LanidPasswordResetMock");
                var key = Environment.GetEnvironmentVariable("EncryptionKey");
                var storageConnectionString = Environment.GetEnvironmentVariable("AzureTableConnectionString");
                var tableName = Environment.GetEnvironmentVariable("AzureTableName");

                string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
                PasswordResetRequest data = JsonConvert.DeserializeObject<PasswordResetRequest>(requestBody);
                var lanid = data.LanId.Split('/');

                var randomPassword = GenerateRandomPasswordHelper.GetPassword(15);
                var httpClient = new HttpClient();
                var scheme = req.Scheme;

                Url = Url + "password=" + randomPassword + "&lanid=" + lanid.First() + "&domain=" + lanid.Last();
                HttpResponseMessage httpResponseMessage = await httpClient.GetAsync(Url);
                string json = await httpResponseMessage.Content.ReadAsStringAsync();
                SNPasswordResetResponse response = JsonConvert.DeserializeObject<SNPasswordResetResponse>(requestBody);

                if (response.ErrorCode == null)
                {
                    var encryptedPassword = CryptographyHelper.EncryptString(key, randomPassword);

                    string token = Guid.NewGuid().ToString().Replace("-", string.Empty).Replace("+", string.Empty);
                    LanIdPassword lanIdPassword = new LanIdPassword(data.Recipient, token)
                    {
                        EncryptedPassword = encryptedPassword,
                        Recipient = data.Recipient,
                        IsViewed = false
                    };

                    var azureResponse = await AzureTableHelper.AddEntity<LanIdPassword>(storageConnectionString, tableName, lanIdPassword);
                  
                    if (azureResponse.IsError)
                    {
                        responseMessage.IsSuccess = false;
                        responseMessage.ErrorMessage = "Error while saving record in azure table";
                    }
                    else
                    {
                        var notificationResponse = await NotificationBusiness.SendNotification(data.Recipient, token);

                    }
                }

                return new OkObjectResult(responseMessage);
            }
            catch (Exception ex)
            {
                responseMessage.IsSuccess = false;
                responseMessage.ErrorMessage = ex.Message;

                return new OkObjectResult(responseMessage);
            }
        }
    }
}

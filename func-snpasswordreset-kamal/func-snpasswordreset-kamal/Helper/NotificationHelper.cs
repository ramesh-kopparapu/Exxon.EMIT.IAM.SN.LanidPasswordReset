using func_snpasswordreset_kamal.Models;
using Microsoft.Identity.Client;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace func_snpasswordreset_kamal.Helper
{
    public class NotificationHelper
    {
        public static async System.Threading.Tasks.Task<bool> SendEmailAsync(EmailModel emailModel )
        {
            var clientId = Environment.GetEnvironmentVariable("ClientId");
            var clientSecret = Environment.GetEnvironmentVariable("ClientSecret");
            var cloudEmailApi = Environment.GetEnvironmentVariable("CloudEmailApi");
            var scope = Environment.GetEnvironmentVariable("Scope");
            var accessTokenUrl = Environment.GetEnvironmentVariable("GetAccessToken");

            var httpClient = new HttpClient();
            var values = new Dictionary<string, string>();
            values.Add("client_id", clientId);
            values.Add("client_secret", clientSecret);
            values.Add("grant_type", "client_credentials");
            values.Add("scope", scope);

            dynamic content = new FormUrlEncodedContent(values);

            HttpResponseMessage tokenresponse = await httpClient.PostAsync(accessTokenUrl,content);
            var tokenContent = tokenresponse.Content.ReadAsStringAsync().Result;
            var result = JsonConvert.DeserializeObject<TokenResponse>(tokenContent);

            var data =JsonConvert.SerializeObject(emailModel);
            httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", result.access_token);
            HttpResponseMessage httpResponseMessage = await httpClient.PostAsync(cloudEmailApi, new StringContent(data, Encoding.UTF8, "application/json"));
            string json = await httpResponseMessage.Content.ReadAsStringAsync();
            return httpResponseMessage.IsSuccessStatusCode;
        }
    }
}

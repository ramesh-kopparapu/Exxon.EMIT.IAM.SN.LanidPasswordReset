using func_invitationtracking_acc.Helper;
using func_snpasswordreset_kamal.Helper;
using func_snpasswordreset_kamal.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace func_snpasswordreset_kamal.Business
{
    public class NotificationBusiness
    {
        public static async Task<bool> SendNotification(string recipient, string token)
        {
            string template = ResourceHelper.ReturnTemplate("Notification.txt");
            var webAppUrl = Environment.GetEnvironmentVariable("WebAppURL");
            webAppUrl = webAppUrl.Replace("[token]", token);
            template = template.Replace("[Subject]", "Lan ID Password Reset");
            template = template.Replace("[webappurl]", webAppUrl);
            var emailBody = new EmailBody();
            emailBody.ContentType = "text/html";
            emailBody.Content = template;
            var email = new EmailModel();
            email.Subject = "Lan ID Password Reset";
            email.From = "IAM@Exxonmobil.com";
            email.ToRecipients.Add(recipient);
            email.Body = emailBody;
            return await NotificationHelper.SendEmailAsync(email);
        }
    }
}

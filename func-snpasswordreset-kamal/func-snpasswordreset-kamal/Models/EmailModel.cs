using System;
using System.Collections.Generic;
using System.Text;

namespace func_snpasswordreset_kamal.Models
{
    public class EmailModel
    {
        public string Subject { get; set; }
        public EmailBody Body { get; set; }
        public string From { get; set; }
        public List<string> ToRecipients { get; set; } = new List<string>();
        public List<string> CcRecipients { get; set; } = new List<string>();
        public List<string> BccRecipients { get; set; } = new List<string>();
    }

    public class EmailBody
    {
        public string ContentType { get; set; }
        public string Content { get; set; }
    }
}

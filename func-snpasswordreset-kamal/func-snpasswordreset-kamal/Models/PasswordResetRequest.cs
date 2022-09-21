using System;
using System.Collections.Generic;
using System.Text;

namespace func_snpasswordreset_kamal.Models
{
    public class PasswordResetRequest
    {
        public string Recipient { get; set; }
        public string LanId { get; set; }
    }
}

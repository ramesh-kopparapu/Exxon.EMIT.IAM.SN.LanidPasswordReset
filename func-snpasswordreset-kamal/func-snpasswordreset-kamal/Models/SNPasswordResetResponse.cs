using System;
using System.Collections.Generic;
using System.Text;

namespace func_snpasswordreset_kamal.Models
{
    public class SNPasswordResetResponse
    {
        public string Response { get; set; }
        public string ErrorMessage { get; set; }
        public long? ErrorCode { get; set; }
    }
}

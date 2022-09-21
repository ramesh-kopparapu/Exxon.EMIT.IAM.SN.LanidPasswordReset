using System;
using System.Collections.Generic;
using System.Text;

namespace func_snpasswordreset_kamal.Models
{
    public class PasswordResetResponse
    {
        public bool IsSuccess { get; set; } = true;
        public string ErrorMessage { get; set; } = null;
    }

    public class PasswordResponse
    {
        public string Password { get; set; }
        public string ErrorMessage { get; set; } = null;
    }
}

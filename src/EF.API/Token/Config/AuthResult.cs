using System;
using System.Collections.Generic;

namespace EF.API.Token.Config
{
    public class AuthResult
    {
        public string Token { get; set; }
        public bool Success { get; set; }
        public DateTime TokenExpires { get; set; }
        public List<string> Errors { get; set; }
    }
}

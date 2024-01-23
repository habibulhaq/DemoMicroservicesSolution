using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JwtAuthenticationManager.Contracts
{
    public class AuthenticationResponse
    {
        public int ExpiresIn { get; set; }
        public string Username { get; set; }
        public string JwtToken { get; set; }
    }
}

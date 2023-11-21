using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hims_Security_API.Model
{
    public class AuthenticationRequest
    {
        public string username { get; set; }
        public string Password { get; set; }
    }
}

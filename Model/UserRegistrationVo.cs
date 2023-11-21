using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hims_Security_API.Model
{
    public class UserRegistrationVo
    {
        public string Role { get; set; }
        public string Name { get; set; }
        public Int64 MobileNo { get; set; }
        public string Password { get; set; }
    }
}

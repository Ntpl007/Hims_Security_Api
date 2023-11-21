using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hims_Security_API.Model
{
    public class JwtAuthResponse
    {
        public string token { get; set; }
        public string User_Name { get; set; }
        public int User_Id { get; set; }
        public int Expires_In { get; set; }
        public string Facility_Name { get; set; }
        public string Organization_Name { get; set; }
        public int Organization_Id { get; set; }
        public int Facility_Id { get; set; }


    }
}

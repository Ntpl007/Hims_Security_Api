using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hims_Security_API.Model
{
    public class AddUserVo
    {
        public string First_Name { get; set; }
        public string Last_Name { get; set; }
        public string User_Name { get; set; }
        public Int64 Mobile_Number { get; set; }
        public string Password { get; set; }
        public byte[] Encrypted_Password { get; set; } = null;
        public DateTime? Created_Date { get; set; } = null;
        public string CreatedBy { get; set; } = null;
        public int UserRole { get; set; }
        public int facility_id { get; set; }
        public int? Speciality { get; set; }
        public bool? isReferDoctor { get; set; }
        
        public int Organization_id { get; set; }
        public string FacilityList { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hims_Security_API.Model
{
    public class AddOrg_facilityVo
    {
       public int Sno { get; set; }
       public string Organization { get; set; }
       public string Facility { get; set; }
       public string Address { get; set; }
       public string FacilityAddress { get; set; }
       public string CreatedBy { get; set; }
       public string organizationimage { get; set; }
    }
}

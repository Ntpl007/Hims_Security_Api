using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hims_Security_API.Model
{
    public class UpdateFacilityVo
    {
        public int FacilityMappingId { get; set; }
        public string Facility { get; set; }
        public string Address { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hims_Security_API.Model
{
    public class UpdateFacilitiesForUserVo
    {
        public int FacilityListId { get; set; }
        public int DefaultFacilityId { get; set; }
        public int UserId { get; set; }
    }
}

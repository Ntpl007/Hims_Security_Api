using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hims_Security_API.Model
{
    public class UpdateOrganizationVo
    {
        public int OrganizationId { get; set; }
        public string Organization { get; set; }
        public string Address { get; set; }
    }
}

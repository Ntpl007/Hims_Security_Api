using System;
using System.Collections.Generic;

#nullable disable

namespace Hims_Security_API.SecurityDB
{
    public partial class TblAdmOccupationMaster
    {
        public int Id { get; set; }
        public string Occupation { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
    }
}

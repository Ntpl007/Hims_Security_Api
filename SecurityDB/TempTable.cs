using System;
using System.Collections.Generic;

#nullable disable

namespace Hims_Security_API.SecurityDB
{
    public partial class TempTable
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public int? Count { get; set; }
        public int? DoctorId { get; set; }
    }
}

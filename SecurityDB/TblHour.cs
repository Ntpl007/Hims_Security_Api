using System;
using System.Collections.Generic;

#nullable disable

namespace Hims_Security_API.SecurityDB
{
    public partial class TblHour
    {
        public int Id { get; set; }
        public string Hours { get; set; }
        public int? Htype { get; set; }
    }
}

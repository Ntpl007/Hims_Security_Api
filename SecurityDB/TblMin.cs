using System;
using System.Collections.Generic;

#nullable disable

namespace Hims_Security_API.SecurityDB
{
    public partial class TblMin
    {
        public int Id { get; set; }
        public string Minutes { get; set; }
        public int? Period { get; set; }
    }
}

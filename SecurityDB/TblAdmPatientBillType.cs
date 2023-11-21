using System;
using System.Collections.Generic;

#nullable disable

namespace Hims_Security_API.SecurityDB
{
    public partial class TblAdmPatientBillType
    {
        public int BillTypeId { get; set; }
        public string BillType { get; set; }
        public int? StatusId { get; set; }
        public string CreatedBy { get; set; }
    }
}

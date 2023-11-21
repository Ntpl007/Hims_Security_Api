using System;
using System.Collections.Generic;

#nullable disable

namespace Hims_Security_API.SecurityDB
{
    public partial class TblEncounterBillingPaymentsAgainstCahrgeItemLink
    {
        public string PaymentBillingEntryId { get; set; }
        public string PaymentAgainstBillingEntryId { get; set; }
        public int ChargeItemId { get; set; }
        public long EncounterId { get; set; }
        public decimal? PaidAmount { get; set; }
    }
}

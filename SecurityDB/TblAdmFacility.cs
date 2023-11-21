using System;
using System.Collections.Generic;

#nullable disable

namespace Hims_Security_API.SecurityDB
{
    public partial class TblAdmFacility
    {
        public TblAdmFacility()
        {
            TblAppointments = new HashSet<TblAppointment>();
            TblProviderScheduleTemplates = new HashSet<TblProviderScheduleTemplate>();
        }

        public int FacilityId { get; set; }
        public string FacilityName { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string Createdby { get; set; }

        public virtual ICollection<TblAppointment> TblAppointments { get; set; }
        public virtual ICollection<TblProviderScheduleTemplate> TblProviderScheduleTemplates { get; set; }
    }
}

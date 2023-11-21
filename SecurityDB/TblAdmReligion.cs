using System;
using System.Collections.Generic;

#nullable disable

namespace Hims_Security_API.SecurityDB
{
    public partial class TblAdmReligion
    {
        public TblAdmReligion()
        {
            TblAppointmentPatientTemps = new HashSet<TblAppointmentPatientTemp>();
            TblPatients = new HashSet<TblPatient>();
        }

        public int ReligionId { get; set; }
        public string ReligionName { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreationDate { get; set; }

        public virtual ICollection<TblAppointmentPatientTemp> TblAppointmentPatientTemps { get; set; }
        public virtual ICollection<TblPatient> TblPatients { get; set; }
    }
}

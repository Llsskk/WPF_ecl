using System;
using System.Collections.Generic;

#nullable disable

namespace ecl
{
    public partial class Company
    {
        public int Id { get; set; }
        public string NameFull { get; set; }
        public string NameShort { get; set; }
        public int NumberReg { get; set; }
        public DateTime DataReg { get; set; }
        public int PersonsId { get; set; }
        public int OrgRegistrationId { get; set; }
        public int OrgLegFormId { get; set; }

        public virtual OrgLegForm OrgLegForm { get; set; }
        public virtual OrgRegistration OrgRegistration { get; set; }
        public virtual Person Person { get; set; }
    }
}

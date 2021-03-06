using System;
using System.Collections.Generic;

#nullable disable

namespace ecl
{
    public partial class OrgLegForm
    {
        public OrgLegForm()
        {
            Companies = new HashSet<Company>();
        }

        public int Id { get; set; }
        public string NameFull { get; set; }
        public string NameShort { get; set; }

        public virtual ICollection<Company> Companies { get; set; }
    }
}

using System;
using System.Collections.Generic;

#nullable disable

namespace ecl
{
    public partial class Person
    {
        public Person()
        {
            Companies = new HashSet<Company>();
        }

        public int Id { get; set; }
        public int Shifer { get; set; }
        public int Inn { get; set; }
        public string Type { get; set; }
        public DateTime? Data { get; set; }

        public virtual ICollection<Company> Companies { get; set; }
    }
}

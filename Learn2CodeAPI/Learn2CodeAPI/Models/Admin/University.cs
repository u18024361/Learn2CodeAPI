using Learn2CodeAPI.Models.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Learn2CodeAPI.Models.Admin
{
    public class University : BaseEntity
    {
        public University() { }
        public string UniversityName { get; set; }

        public ICollection<Degree> Degree { get; set; }

    }
}

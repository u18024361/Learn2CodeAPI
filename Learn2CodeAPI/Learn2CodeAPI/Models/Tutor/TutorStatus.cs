using Learn2CodeAPI.Models.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Learn2CodeAPI.Models.Tutor
{
    public class TutorStatus : BaseEntity
    {
        public string TutorStatusDesc { get; set; }

        public ICollection<Tutor> Tutors{ get; set; }
    }
}

using Learn2CodeAPI.Models.Admin;
using Learn2CodeAPI.Models.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Learn2CodeAPI.Models.Tutor
{
    public class TutorModule : BaseEntity
    {
        public int TutorId { get; set; }
        public Tutor Tutor { get; set; }
        public int ModuleId { get; set; }
        public Module Module { get; set; }
    }
}

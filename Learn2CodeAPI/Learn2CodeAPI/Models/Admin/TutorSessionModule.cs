using Learn2CodeAPI.Models.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Learn2CodeAPI.Models.Admin
{
    public class TutorSessionModule : BaseEntity
    {
        public int ModuleId { get; set; }
        public Module Module { get; set; }
        public int TutorSessionId { get; set; }
        public TutorSession TutorSession { get; set; }
    }
}

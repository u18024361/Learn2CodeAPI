using Learn2CodeAPI.Models.Admin;
using Learn2CodeAPI.Models.Generic;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Learn2CodeAPI.Models.Tutor
{
    public class TutorSessionModuleTutor : BaseEntity
    {
        public int TutorId { get; set; }

        [ForeignKey("TutorId")]
        public Tutor Tutor { get; set; }

        public int TutorSessionId { get; set; }

        [ForeignKey("TutorSessionId")]
        public TutorSession TutorTutorSession { get; set; }

        public int ModuleId { get; set; }

        [ForeignKey("ModuleId")]
        public Module Module { get; set; }
    }
}

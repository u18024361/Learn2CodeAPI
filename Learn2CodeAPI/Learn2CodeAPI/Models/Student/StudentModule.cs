using Learn2CodeAPI.Models.Admin;
using Learn2CodeAPI.Models.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Learn2CodeAPI.Models.Student
{
    public class StudentModule : BaseEntity
    {
        public int StudentId { get; set; }
        public Student Students { get; set; }
        public int ModuleId { get; set; }
        public Module Tutor { get; set; }
    }
}

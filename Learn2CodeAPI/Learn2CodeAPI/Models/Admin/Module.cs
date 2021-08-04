using Learn2CodeAPI.Models.Generic;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Learn2CodeAPI.Models.Student;
using Learn2CodeAPI.Models.Tutor;

namespace Learn2CodeAPI.Models.Admin
{
    public class Module : BaseEntity
    {
        public Module() { }
        public string ModuleCode { get; set; }

        public ICollection<StudentModule> StudentModule { get; set; }

        public ICollection<TutorSessionModule> TutorSessionModule { get; set; }
        public ICollection<TutorSessionModuleTutor> TutorSessionModuleTutor { get; set; }

        public int DegreeId { get; set; }

        [ForeignKey("DegreeId")]
        public Degree Degree { get; set; }

        public ICollection<TutorModule> TutorModule { get; set; }
        public ICollection<BookingInstance> BookingInstance { get; set; }
    }
}

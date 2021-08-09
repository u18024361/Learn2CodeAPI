using Learn2CodeAPI.Models.Generic;
using Learn2CodeAPI.Models.Student;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Learn2CodeAPI.Models.Tutor
{
    public class CourseEnrol : BaseEntity
    {
        public int StudentId { get; set; }

        [ForeignKey("StudentId")]
        public Student.Student Student { get; set; }

        public string Date { get; set; }
        public ICollection<CourseEnrolLine> CourseEnrolLine { get; set; }
    }
}

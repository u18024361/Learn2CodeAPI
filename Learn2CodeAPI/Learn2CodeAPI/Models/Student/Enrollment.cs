using Learn2CodeAPI.Models.Generic;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Learn2CodeAPI.Models.Student
{
    public class Enrollment : BaseEntity
    {
        public int StudentId { get; set; }

        [ForeignKey("StudentId")]
        public Student Student { get; set; }

        public string Date { get; set; }
        public ICollection<EnrolLine> EnrolLine { get; set; }
        public ICollection<Ticket> Ticket { get; set; }
    }
}

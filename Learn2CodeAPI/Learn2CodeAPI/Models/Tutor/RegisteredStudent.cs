using Learn2CodeAPI.Models.Generic;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Learn2CodeAPI.Models.Tutor
{
    public class RegisteredStudent :BaseEntity
    {
        public int StudentId { get; set; }
        public Student.Student Student  { get; set; }

        public bool Attended { get; set; }
        public int BookingInstanceId { get; set; }
        public BookingInstance BookingInstance { get; set; }

    }
}

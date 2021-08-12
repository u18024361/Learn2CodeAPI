using Learn2CodeAPI.Models.Generic;
using Learn2CodeAPI.Models.Tutor;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Learn2CodeAPI.Models.Student
{
    public class Booking : BaseEntity
    {
        public int StudentId { get; set; }

        [ForeignKey("StudentId")]
        public Student Student { get; set; }
        public ICollection<BookingInstance> bookinginstances { get; set; }
    }
}

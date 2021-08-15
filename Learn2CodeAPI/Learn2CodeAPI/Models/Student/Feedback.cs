using Learn2CodeAPI.Models.Generic;
using Learn2CodeAPI.Models.Tutor;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Learn2CodeAPI.Models.Student
{
    public class Feedback : BaseEntity
    {
        public int Friendliness { get; set; }

        public int Timliness { get; set; }

        public int Ability { get; set; }

        public string Description { get; set; }

        public int StudentId { get; set; }
        [ForeignKey("StudentId")]
        public Student Student { get; set; }

        public int BookingInstanceId { get; set; }
        [ForeignKey("BookingInstanceId")]
        public BookingInstance BookingInstance { get; set; }
    }
}

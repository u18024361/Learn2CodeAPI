using Learn2CodeAPI.Models.Generic;
using Learn2CodeAPI.Models.Student;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Learn2CodeAPI.Models.Tutor
{
    public class Basket : BaseEntity
    {
        public int StudentId { get; set; }

        [ForeignKey("StudentId")]
        public Student.Student Student { get; set; }
        public int Quantity { get; set; }

        public double TotalPrice { get; set; }

        public ICollection<CourseBasketLine> CourseBasketLine { get; set; }
        public ICollection<SubScriptionBasketLine> SubScriptionBasketLine { get; set; }
    }
}

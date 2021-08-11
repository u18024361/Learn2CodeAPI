using Learn2CodeAPI.Models.Generic;
using Learn2CodeAPI.Models.Student;
using Learn2CodeAPI.Models.Tutor;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Learn2CodeAPI.Models.Admin
{
    public class CourseSubCategory : BaseEntity
    {
        public string CourseSubCategoryName { get; set; }

        public string Description { get; set; }
        public double price { get; set; }

        public int CourseFolderId { get; set; }

        [ForeignKey("CourseFolderId")]
        public CourseFolder courseFolder{ get; set; }

        public ICollection<CourseContent> CourseContent { get; set; }
        public ICollection<CourseBasketLine> CourseBasketLine { get; set; }
        public ICollection<CourseEnrolLine> CourseEnrolLine { get; set; }
    }
}

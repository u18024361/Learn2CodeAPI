using Learn2CodeAPI.Models.Admin;
using Learn2CodeAPI.Models.Generic;
using Learn2CodeAPI.Models.Tutor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Learn2CodeAPI.Models.Student
{
    public class CourseEnrolLine : BaseEntity
    {
        public int CourseEnrolId { get; set; }
        public CourseEnrol CourseEnrol { get; set; }
        public int CourseSubCategoryId { get; set; }
        public CourseSubCategory CourseSubCategory { get; set; }

    }
}

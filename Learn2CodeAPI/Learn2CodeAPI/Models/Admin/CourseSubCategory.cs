using Learn2CodeAPI.Models.Generic;
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
    }
}

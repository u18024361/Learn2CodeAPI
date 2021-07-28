using Learn2CodeAPI.Dtos.GenericDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Learn2CodeAPI.Dtos.AdminDto
{
    public class CoursSubCategoryDto : BaseDto
    {
        public string CourseSubCategoryName { get; set; }

        public string Description { get; set; }
        public double price { get; set; }

        public int CourseFolderId { get; set; }
    }
}

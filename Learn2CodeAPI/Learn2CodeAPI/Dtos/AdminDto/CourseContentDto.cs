using Learn2CodeAPI.Dtos.GenericDto;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Learn2CodeAPI.Dtos.AdminDto
{
    public class CourseContentDto : BaseDto
    {
        public IFormFile Content { get; set; }
        
        public int CourseSubCategoryId { get; set; }

        public int ContentTypeId { get; set; }

        
    }
}

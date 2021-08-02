using Learn2CodeAPI.Dtos.GenericDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Learn2CodeAPI.Dtos.AdminDto
{
    public class CourseContentDto : BaseDto
    {
        public byte[] File { get; set; }
        public int CourseSubCategoryId { get; set; }

        public int ContentTypeId { get; set; }

        
    }
}

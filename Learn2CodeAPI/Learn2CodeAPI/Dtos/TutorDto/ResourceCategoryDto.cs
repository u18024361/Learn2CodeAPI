using Learn2CodeAPI.Dtos.GenericDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Learn2CodeAPI.Dtos.TutorDto
{
    public class ResourceCategoryDto : BaseDto
    {
        public ResourceCategoryDto() { }

        public string ResourceCategoryName { get; set; }
    }
}

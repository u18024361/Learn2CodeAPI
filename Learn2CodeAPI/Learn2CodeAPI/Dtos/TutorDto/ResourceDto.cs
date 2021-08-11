using Learn2CodeAPI.Dtos.GenericDto;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Learn2CodeAPI.Dtos.TutorDto
{
    public class ResourceDto : BaseDto
    {
        public IFormFile ResoucesName { get; set; }

        public string ResourceDescription { get; set; }

        public int ResourceCategoryId { get; set; }
        
        public int ModuleId { get; set; }
       
    }
}

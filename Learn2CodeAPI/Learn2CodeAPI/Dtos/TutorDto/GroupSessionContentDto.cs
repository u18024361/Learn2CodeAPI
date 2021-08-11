using Learn2CodeAPI.Dtos.GenericDto;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Learn2CodeAPI.Dtos.TutorDto
{
    public class GroupSessionContentDto : BaseDto
    {
        public IFormFile Notes { get; set; }
        public IFormFile Recording { get; set; }
        public int SessionContentCategoryId { get; set; }
        public int BookingInstanceId { get; set; }

        
    }
}

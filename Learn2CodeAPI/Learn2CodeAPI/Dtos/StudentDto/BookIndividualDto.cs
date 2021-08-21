using Learn2CodeAPI.Dtos.GenericDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Learn2CodeAPI.Dtos.StudentDto
{
    public class BookIndividualDto : BaseDto
    {
        public int StudentId { get; set; }
        public int BookingInstanceId { get; set; }
        public int ModuleId { get; set; }
        public string Description { get; set; }
    }
}

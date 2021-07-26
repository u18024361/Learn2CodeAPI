using Learn2CodeAPI.Dtos.GenericDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Learn2CodeAPI.Dtos.AdminDto
{
    public class CourseFolderDto : BaseDto
    {
        public CourseFolderDto() { }
        public string CourseFolderName { get; set; }

        public int AdminId { get; set; }
    }
}

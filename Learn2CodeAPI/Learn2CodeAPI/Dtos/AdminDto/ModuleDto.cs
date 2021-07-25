using Learn2CodeAPI.Dtos.GenericDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Learn2CodeAPI.Dtos.AdminDto
{
    public class ModuleDto : BaseDto
    {
        public string ModuleCode { get; set; }

        public int DegreeId { get; set; }
    }
}

using Learn2CodeAPI.Dtos.GenericDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Learn2CodeAPI.Dtos.AdminDto
{
    public class SessionContentCategoryDto : BaseDto
    {
        public SessionContentCategoryDto() { }
        public string SessionContentCategoryName { get; set; }

        public int AdminId { get; set; }
    }
}

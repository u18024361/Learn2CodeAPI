using Learn2CodeAPI.Dtos.GenericDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Learn2CodeAPI.Dtos.LoginDto
{
    public class ChangePasswordDto 
    {
        public string Id { get; set; }
        public string Password { get; set; }
    }
}

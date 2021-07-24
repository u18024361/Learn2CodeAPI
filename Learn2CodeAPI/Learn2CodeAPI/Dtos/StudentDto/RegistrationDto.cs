using Learn2CodeAPI.Dtos.GenericDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Learn2CodeAPI.Dtos.StudentDto
{
    public class RegistrationDto 
    {
        public string StudentName { get; set; }
        public string StudentSurname { get; set; }
        public string StudentCell { get; set; }

        public string UserName{ get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public int ModuleId { get; set; }
    }
}

using Learn2CodeAPI.Models.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Learn2CodeAPI.Dtos.AdminDto
{
    public class CreateTutorDto : BaseEntity
    {
        public string TutorName { get; set; }
        public string TutorSurname { get; set; }
        public string TutorCell { get; set; }
        public string TutorAbout { get; set; }

        public string UserName { get; set; }
        public string Password { get; set; }

        public string TutorPhoto { get; set; }
        public string TutorEmail { get; set; }

        public int FileId { get; set; }

        public int ModuleId { get; set; }
        public string UserId { get; set; }

        public int TutorStatusId { get; set; }
    }
}

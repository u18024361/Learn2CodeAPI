using Learn2CodeAPI.Models.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Learn2CodeAPI.Dtos.AdminDto
{
    public class TutorDto : BaseEntity
    {
        public string TutorName { get; set; }
        public string TutorSurname { get; set; }
        public string TutorCell { get; set; }
        public string TutorAbout { get; set; }

        public string TutorPhoto { get; set; }
        public string TutorEmail { get; set; }

        public int FileId { get; set; }
        
        public int TutorStatusId { get; set; }
       
    }
}

using Learn2CodeAPI.Dtos.GenericDto;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Learn2CodeAPI.Dtos.TutorDto
{
    public class TutorDtoo : BaseDto
    {
        public string TutorName { get; set; }
        public string TutorSurname { get; set; }
        public string TutorCell { get; set; }
        public string TutorAbout { get; set; }

        public IFormFile TutorPhoto { get; set; }

        public IFormFile File { get; set; }
        public string TutorEmail { get; set; }

        public int FileId { get; set; }

        public int ModuleId { get; set; }


        public int TutorStatusId { get; set; }
        

        
    }
}

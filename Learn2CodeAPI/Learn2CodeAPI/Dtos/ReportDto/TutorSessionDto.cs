using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Learn2CodeAPI.Dtos.ReportDto
{
    public class TutorSessionDto
    {
        public DateTime Date { get; set; }
        public string TutorName { get; set; }
        public string TutorSurname { get; set; }
        public string TutorEmail { get; set; }
        public string ModuleCode { get; set; }
        public string Title { get; set; }
    }
}

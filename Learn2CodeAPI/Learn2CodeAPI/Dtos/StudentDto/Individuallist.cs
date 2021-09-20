using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Learn2CodeAPI.Dtos.StudentDto
{
    public class Individuallist
    {
        public string title { get; set; }
        public string endTime { get; set; }
        public string startTime { get; set; }
        public string tutorName { get; set; }
        public string tutorSurname { get; set; }
        public DateTime date { get; set; }
        public int id { get; set; }
        public string moduleCode { get; set; }
        public string link { get; set; }

    }
}

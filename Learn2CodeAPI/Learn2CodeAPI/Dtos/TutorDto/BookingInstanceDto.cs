using Learn2CodeAPI.Dtos.GenericDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Learn2CodeAPI.Dtos.TutorDto
{
    public class BookingInstanceDto : BaseDto
    {
        public int SessionTimeId { get; set; }

        public int ModuleId { get; set; }

        public int TutorSessionId { get; set; }
        public int BookingStatusId { get; set; }

        public int TutorId { get; set; }

        public DateTime Date { get; set; }

        public string Title { get; set; }

        public string Link { get; set; }

        public string Description { get; set; }

        public bool AttendanceTaken { get; set; }

        public bool ContentUploaded { get; set; }
    }
}


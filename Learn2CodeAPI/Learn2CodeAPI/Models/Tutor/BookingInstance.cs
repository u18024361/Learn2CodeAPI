using Learn2CodeAPI.Models.Admin;
using Learn2CodeAPI.Models.Generic;
using Learn2CodeAPI.Models.Student;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Learn2CodeAPI.Models.Tutor
{
    public class BookingInstance : BaseEntity
    {
        public int SessionTimeId { get; set; }

        [ForeignKey("SessionTimeId")]
        public SessionTime SessionTime { get; set; }

        public int ModuleId { get; set; }

        [ForeignKey("ModuleId")]
        public Module Module { get; set; }

        public int BookingStatusId { get; set; }

        [ForeignKey("BookingStatusId")]
        public BookingStatus BookingStatus { get; set; }

        public int TutorSessionId { get; set; }

        [ForeignKey("TutorSessionId")]
        public TutorSession TutorSession { get; set; }

        public int TutorId { get; set; }

        [ForeignKey("TutorId")]
        public Tutor Tutor { get; set; }

        public string Date { get; set; }

        public string Title { get; set; }

        public string Link { get; set; }

        public string Description { get; set; }

        public bool AttendanceTaken { get; set; }

        public bool ContentUploaded { get; set; }

        public ICollection<RegisteredStudent> RegisteredStudent { get; set; }

        public int? BookingId { get; set; }

        [ForeignKey("BookingId")]
        public Booking Booking { get; set; }

        public int? TicketId { get; set; }

        [ForeignKey("TicketId")]
        public Ticket Ticket { get; set; }
    }
}

using Learn2CodeAPI.Models.Admin;
using Learn2CodeAPI.Models.Generic;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Learn2CodeAPI.Models.Student
{
    public class Ticket : BaseEntity
    {
        public int EnrollmentId { get; set; }
        [ForeignKey("EnrollmentId")]
        public Enrollment Enrollment { get; set; }

        public int SubscriptionId { get; set; }
        [ForeignKey("SubscriptionId")]
        public Subscription Subscription { get; set; }

        public int TutorSessionId { get; set; }
        [ForeignKey("TutorSessionId")]
        public TutorSession TutorSession { get; set; }

        public int ModuleId { get; set; }
        [ForeignKey("ModuleId")]
        public Module Module { get; set; }

        public int TicketStatusId { get; set; }
        [ForeignKey("TicketStatusId")]
        public TicketStatus TicketStatus { get; set; }

        public DateTime ExpDate { get; set; }
    }
}

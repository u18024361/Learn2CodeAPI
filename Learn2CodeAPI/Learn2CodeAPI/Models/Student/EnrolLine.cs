using Learn2CodeAPI.Models.Admin;
using Learn2CodeAPI.Models.Generic;
using System;

namespace Learn2CodeAPI.Models.Student
{
    public class EnrolLine : BaseEntity
    {
        public int EnrollmentId { get; set; }
        public Enrollment Enrollment { get; set; }
        public int ModuleId { get; set; }
        public Module Module { get; set; }
        public int SubscriptionId { get; set; }
        public Subscription Subscription { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int TicketQuantity { get; set; }
    }
}

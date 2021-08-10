using Learn2CodeAPI.Models.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Learn2CodeAPI.Models.Student
{
    public class TicketStatus : BaseEntity
    {
        public bool ticketStatus { get; set; }

        public ICollection<Ticket> Ticket { get; set; }
    }
}

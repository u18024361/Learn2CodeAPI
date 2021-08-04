using Learn2CodeAPI.Models.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Learn2CodeAPI.Models.Tutor
{
    public class BookingStatus : BaseEntity
    {
        public string bookingStatus { get; set; }

        public ICollection<BookingInstance> BookingInstance { get; set; }
    }
}

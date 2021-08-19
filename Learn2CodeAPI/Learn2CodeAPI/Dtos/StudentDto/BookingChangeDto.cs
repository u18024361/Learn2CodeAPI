using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Learn2CodeAPI.Dtos.StudentDto
{
    public class BookingChangeDto
    {
        public string Message { get; set; }
        public int SessionTimeId { get; set; }

        public int StudentId { get; set; }

        public DateTime date { get; set; }

        //bookinginstanceid
        public int Id { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Learn2CodeAPI.Dtos.StudentDto
{
    public class CheckoutDto
    {
        public int StudentId { get; set; }
        public int BasketId { get; set; }
        public string Message { get; set; }
    }
}

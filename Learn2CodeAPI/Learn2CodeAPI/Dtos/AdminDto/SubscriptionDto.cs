using Learn2CodeAPI.Dtos.GenericDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Learn2CodeAPI.Dtos.AdminDto
{
    public class SubscriptionDto : BaseDto
    {
        public int AdminId { get; set; }

       
        public string SubscriptionName { get; set; }

        public int Duration { get; set; }

        public double price { get; set; }

        public int Quantity { get; set; }

        public int TutorSessionId { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Learn2CodeAPI.Dtos.StudentDto
{
    public class SubscriptionBuyDto 
    {
        public int BasketId { get; set; }
        public int SubscriptionId { get; set; }

        public int ModuleId { get; set; }
    }
}

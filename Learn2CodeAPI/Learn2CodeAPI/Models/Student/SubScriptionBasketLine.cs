using Learn2CodeAPI.Models.Admin;
using Learn2CodeAPI.Models.Generic;
using Learn2CodeAPI.Models.Tutor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Learn2CodeAPI.Models.Student
{
    public class SubScriptionBasketLine : BaseEntity
    {
        public int BasketId { get; set; }
        public Basket Basket { get; set; }
        public int SubscriptionId { get; set; }
        public Subscription Subscription { get; set; }

        public int ModuleId { get; set; }
        public Module Module { get; set; }
    }
}

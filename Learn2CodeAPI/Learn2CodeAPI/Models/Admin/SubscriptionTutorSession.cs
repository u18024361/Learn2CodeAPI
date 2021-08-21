using Learn2CodeAPI.Models.Generic;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Learn2CodeAPI.Models.Admin
{
    public class SubscriptionTutorSession : BaseEntity
    {
        public int SubscriptionId { get; set; }
        public Subscription Subscription { get; set; }
        public int TutorSessionId { get; set; }
       
        public TutorSession TutorSession { get; set; }

        public int Quantity { get; set; }
    }
}

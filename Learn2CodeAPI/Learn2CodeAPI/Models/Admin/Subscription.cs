﻿using Learn2CodeAPI.Models.Generic;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Learn2CodeAPI.Models.Admin
{
    public class Subscription : BaseEntity
    {
        public int AdminId { get; set; }

        [ForeignKey("AdminId")]
        public Admin admin { get; set; }

        public string SubscriptionName { get; set; }

        public int Duration { get; set; }

        public double price { get; set; }

        public ICollection<SubscriptionTutorSession> SubscriptionTutorSession { get; set; }
    }
}

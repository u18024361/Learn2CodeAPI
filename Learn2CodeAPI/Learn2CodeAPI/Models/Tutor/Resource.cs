using Learn2CodeAPI.Models.Admin;
using Learn2CodeAPI.Models.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Learn2CodeAPI.Models.Tutor
{
    public class Resource : BaseEntity
    {
        public byte[] ResoucesName { get; set; }

        public string ResourceDescription { get; set; }

        public int ResourceCategoryId { get; set; }
        public ResourceCategory ResourceCategory { get; set; }
        public int ModuleId { get; set; }
        public Module Module { get; set; }


    }
}

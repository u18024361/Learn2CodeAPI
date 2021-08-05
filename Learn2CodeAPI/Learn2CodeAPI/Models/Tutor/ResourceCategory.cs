using Learn2CodeAPI.Models.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Learn2CodeAPI.Models.Tutor
{
    public class ResourceCategory : BaseEntity
    {
        public ResourceCategory() { }

        public string ResourceCategoryName { get; set; }

        public ICollection<Resource> Resource { get; set; }
    }
}

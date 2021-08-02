using Learn2CodeAPI.Models.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Learn2CodeAPI.Models.Admin
{
    public class ContentType : BaseEntity
    {
        public string ContentTypeName { get; set; }

        public ICollection<CourseContent> CourseContent { get; set; }
    }
}

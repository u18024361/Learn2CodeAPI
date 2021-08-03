using Learn2CodeAPI.Models.Generic;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Learn2CodeAPI.Models.Admin
{
    public class CourseFolder : BaseEntity
    {
        public CourseFolder() { }
        public string CourseFolderName { get; set; }

        public int AdminId { get; set; }

        [ForeignKey("AdminId")]
        public Admin admin { get; set; }

       

    }
}

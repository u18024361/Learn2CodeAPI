using Learn2CodeAPI.Models.Generic;
using Learn2CodeAPI.Models.Login.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Learn2CodeAPI.Models.Admin
{
    public class Admin : BaseEntity
    {
        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public AppUser Identity { get; set; }

        public ICollection<CourseFolder> courseFolder { get; set; }
    }
}

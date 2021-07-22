using Learn2CodeAPI.Models.Generic;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Learn2CodeAPI.Models.Admin
{
    public class Degree : BaseEntity
    {
        public Degree() { }
        public string DegreeName { get; set; }

        public int UniversityId { get; set; }

        [ForeignKey("UniversityId")]
        public University University{ get; set; }

        public ICollection<Module> Module { get; set; }
    }
}

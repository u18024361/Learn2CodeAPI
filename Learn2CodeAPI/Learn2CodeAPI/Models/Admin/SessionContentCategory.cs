using Learn2CodeAPI.Models.Generic;
using Learn2CodeAPI.Models.Tutor;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Learn2CodeAPI.Models.Admin
{
    public class SessionContentCategory : BaseEntity
    {
        public SessionContentCategory() { }
        public string SessionContentCategoryName { get; set; }

        public int AdminId { get; set; }

        [ForeignKey("AdminId")]
        public Admin admin { get; set; }

        public ICollection<GroupSessionContent> GroupSessionContent { get; set; }
    }
}

using Learn2CodeAPI.Models.Generic;
using Learn2CodeAPI.Models.Login.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Learn2CodeAPI.Models.Tutor
{
    public class Tutor : BaseEntity
    {
        public string TutorName { get; set; }
        public string TutorSurname { get; set; }
        public string TutorCell { get; set; }
        public string TutorAbout { get; set; }

        public string TutorPhoto { get; set; }
        public string TutorEmail { get; set; }

        public int FileId { get; set; }
        [ForeignKey("FileId")]
        public File File { get; set; }

        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public AppUser Identity { get; set; }

        public int TutorStatusId { get; set; }
        [ForeignKey("TutorStatusId")]
        public TutorStatus TutorStatus { get; set; }

        public ICollection<Message> message { get; set; }
    }
}

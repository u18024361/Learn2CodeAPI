using Learn2CodeAPI.Models.Admin;
using Learn2CodeAPI.Models.Generic;
using Learn2CodeAPI.Models.Login.Identity;
using Learn2CodeAPI.Models.Tutor;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Learn2CodeAPI.Models.Student
{
    public class Student: BaseEntity
    {
        public string StudentName { get; set; }
        public string StudentSurname { get; set; }

        public string StudentCell { get; set; }

        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public AppUser Identity { get; set; }

        public ICollection<StudentModule> StudentModule { get; set; }
        public ICollection<Message> message { get; set; }
    }
}

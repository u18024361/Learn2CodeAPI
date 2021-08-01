using Learn2CodeAPI.Models.Generic;
using Learn2CodeAPI.Models.Login.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Learn2CodeAPI.Models.Tutor
{
    public class Message : BaseEntity
    {
        public string SenderId { get; set; }

        public string ReceiverId { get; set; }
        public string MessageSent { get; set; }
        public string TimeStamp { get; set; }

        public int TutorId { get; set; }
        [ForeignKey("TutorId")]
        public Tutor tutor { get; set; }

        public int StudentId { get; set; }
        [ForeignKey("StudentId")]
        public Student.Student student { get; set; }



    }
}

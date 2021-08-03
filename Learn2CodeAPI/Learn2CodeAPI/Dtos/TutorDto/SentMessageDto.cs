using Learn2CodeAPI.Models.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Learn2CodeAPI.Dtos.TutorDto
{
    public class SentMessageDto 
    {
        public string SenderId { get; set; }

        public string ReceiverId { get; set; }
        public string MessageSent { get; set; }
        public string TimeStamp { get; set; }

        public string UserId { get; set; }

        public string StudentName { get; set; }
        public string StudentSurname { get; set; }
    }
}

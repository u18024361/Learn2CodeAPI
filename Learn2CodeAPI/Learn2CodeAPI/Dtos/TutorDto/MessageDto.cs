using Learn2CodeAPI.Models.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Learn2CodeAPI.Dtos.TutorDto
{
    public class MessageDto : BaseEntity
    {
        public string SenderId { get; set; }

        public string ReceiverId { get; set; }
        public string MessageSent { get; set; }
       

        public string UserId { get; set; }
       

    }
}

using Learn2CodeAPI.Models.Generic;
using Learn2CodeAPI.Models.Student;
using Learn2CodeAPI.Models.Tutor;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Learn2CodeAPI.Models.Admin
{
    public class TutorSession : BaseEntity
    {
        public int SessionTypeId { get; set; }

        [ForeignKey("SessionTypeId")]
        public SessionType SessionType { get; set; }

        public ICollection<TutorSessionModule> TutorSessionModule { get; set; }
        public ICollection<SubscriptionTutorSession> SubscriptionTutorSession { get; set; }
        public ICollection<TutorSessionModuleTutor> TutorSessionModuleTutor { get; set; }
        public ICollection<BookingInstance> BookingInstance { get; set; }
        public ICollection<Ticket> Ticket { get; set; }
    }
}

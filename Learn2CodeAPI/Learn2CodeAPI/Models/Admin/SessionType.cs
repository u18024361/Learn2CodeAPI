using Learn2CodeAPI.Models.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Learn2CodeAPI.Models.Admin
{
   
        public class SessionType : BaseEntity
        {
            public bool IsGroup { get; set; }

            public string SessionTypeName { get; set; }

            public ICollection<TutorSession> TutorSession { get; set; }
        }
    
}

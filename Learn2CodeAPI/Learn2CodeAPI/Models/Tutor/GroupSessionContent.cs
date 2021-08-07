using Learn2CodeAPI.Models.Admin;
using Learn2CodeAPI.Models.Generic;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Learn2CodeAPI.Models.Tutor
{
    public class GroupSessionContent : BaseEntity
    {
        public byte[] Notes { get; set; }
        public byte[] Recording { get; set; }
        public string RecordingName { get; set; }
        public string NotesName { get; set; }
        public int SessionContentCategoryId { get; set; }

        [ForeignKey("SessionContentCategoryId")]
        public SessionContentCategory SessionContentCategory { get; set; }

        public int BookingInstanceId { get; set; }

        [ForeignKey("BookingInstanceId")]
        public BookingInstance BookingInstance { get; set; }
    }
}

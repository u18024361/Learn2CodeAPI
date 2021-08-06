using Learn2CodeAPI.Models.Generic;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Learn2CodeAPI.Models.Tutor
{
    public class File : BaseEntity
    {
        public byte[] FileName { get; set; }
       
    }
}

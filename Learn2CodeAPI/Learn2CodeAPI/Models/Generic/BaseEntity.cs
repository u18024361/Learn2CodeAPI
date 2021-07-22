using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Learn2CodeAPI.Models.Generic
{
    public abstract class BaseEntity
    {
        public BaseEntity()
        {

        }

        [Key]
        public int Id { get; set; }
    }
}

using Learn2CodeAPI.Models.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CsvHelper.Configuration;
using CsvHelper.Configuration.Attributes;
using System.ComponentModel.DataAnnotations;

namespace Learn2CodeAPI.Models.Admin
{
    public class Payment
    {   
        [Key]
        [Ignore]
        public int  Id { get; set; }
        [Name("Original Amount")]
        public string Amount { get; set; }
    }
}

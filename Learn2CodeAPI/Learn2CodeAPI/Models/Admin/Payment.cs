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
        [Name("Amount Paid")]
        public string PaymentAmount { get; set; }
        [Name("Customer (fullname)")]
        public string FullName { get; set; }
        [Name("Transaction Date")]
        public string PaymentDate { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BookTest1MVC.Models
{
    public class ReturnBook
    {
        public int OrderID { get; set; }
        [DisplayName("false=cash, true=card")]
        public bool? PaymentMethod { get; set; }

    }
}
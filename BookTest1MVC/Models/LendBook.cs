using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BookTest1MVC.Models
{
    public class LendBook
    {
        [Required]
        public int BookID { get; set; }
        [Required]
        public DateTime EstimatedReturnTime { get; set; }

    }
}
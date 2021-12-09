using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BookTest1MVC.Models
{
    [Table("BookInfo", Schema = "Book")]
    public class BookInfo
    {
        [Key]
        public int BookID { get; set; }
        public string BookName { get; set; }
        public string BookAuthor { get; set; }
        public int BookInventory { get; set; }
        public DateTime BookCollectionTime { get; set; }
        public bool? BookeBorrow { get; set; }

        //public ICollection<Enrollment> Enrollments { get; set; }
    }
}
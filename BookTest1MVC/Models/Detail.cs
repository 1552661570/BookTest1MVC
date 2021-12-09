using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BookTest1MVC.Models
{
    [Table("Detail", Schema = "User")]
    public class Detail
    {
        [Key]
        public int UserID { get; set; }
        public string? UserName { get; set; } = string.Empty;
        public string? UserPassword { get; set; } = string.Empty;
        public string? FirstName { get; set; } = string.Empty;
        public string? MiddleName { get; set; } = string.Empty;
        public string? LastName { get; set; } = string.Empty;
        public string? UserMail { get; set; } = string.Empty;
        public int UserNumber { get; set; }
        public string? UserAddress { get; set; } = string.Empty;
        public bool? SelectPriv { get; set; }
        public bool? BorrowPriv { get; set; }
        public DateTime CreationTime { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BookTest1MVC.Models
{
    [Table("BorrowOrder", Schema = "Book")]
    public class BorrowOrder
    {
        [Key]
        public int OrderId { get; set; }
		public int? BookId { get; set; }
		public int? UserId { get; set; }
		public bool? OrderStatus { get; set; }
		public DateTime? BorrowTime { get; set; }
		public DateTime? EstimatedReturnTime { get; set; }
		public DateTime? ActualReturnTime { get; set; }
		public decimal? BorrowBooksCost { get; set; }
		public int? OverdueDays { get; set; }
		public decimal? PenaltyAmount { get; set; }
		public decimal? TotalCost { get; set; }
		public bool? PaymentMethod	{ get; set; }

	}
}
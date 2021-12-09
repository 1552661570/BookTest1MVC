using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using BookTest1MVC.Models;

namespace BookTest1MVC.Data
{
    public class BookTest1MVCContext : DbContext
    {
        public BookTest1MVCContext (DbContextOptions<BookTest1MVCContext> options)
            : base(options)
        {
        }

        public DbSet<BookTest1MVC.Models.Detail> Detail { get; set; }

        public DbSet<BookTest1MVC.Models.BookInfo> BookInfo { get; set; }

        public DbSet<BookTest1MVC.Models.BorrowOrder> BorrowOrder { get; set; }
    }
}

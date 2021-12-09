using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BookTest1MVC.Data;
using BookTest1MVC.Models;
using Microsoft.Data.SqlClient;

namespace BookTest1MVC.Controllers
{
    public class BookInfoesController : Controller
    {
        private readonly BookTest1MVCContext _context;

        public BookInfoesController(BookTest1MVCContext context)
        {
            _context = context;
        }

        // GET: BookInfoes
        //public async Task<IActionResult> Index()
        //{
        //    return View(await _context.BookInfo.ToListAsync());
        //}

        public async Task<IActionResult> Index(string searchString)
        {
            var books = from m in _context.BookInfo
                        select m;

            if (!String.IsNullOrEmpty(searchString))
            {
                books = books.Where(s => s.BookName.Contains(searchString) || s.BookAuthor.Contains(searchString));
            }

            return View(await books.ToListAsync());
        }

        // GET: BookInfoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bookInfo = await _context.BookInfo
                .FirstOrDefaultAsync(m => m.BookID == id);
            if (bookInfo == null)
            {
                return NotFound();
            }

            return View(bookInfo);
        }

        // GET: BookInfoes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: BookInfoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BookID,BookName,BookAuthor,BookInventory,BookCollectionTime,BookeBorrow")] BookInfo bookInfo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(bookInfo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(bookInfo);
        }

        // GET: BookInfoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bookInfo = await _context.BookInfo.FindAsync(id);
            if (bookInfo == null)
            {
                return NotFound();
            }
            return View(bookInfo);
        }

        public IActionResult proc_BorrowOrder_Lend()
        {
            return View("Lend");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> proc_BorrowOrder_Lend(int id, [Bind("EstimatedReturnTime")] LendBook LendBook)
        {
            //if (id == mull)
            //{
            //    return NotFound();
            //}

            //if (ModelState.IsValid)
            //{
            //    var returnText = "Error";
            //    try
            //    {
            //        returnText = _context.Database.ExecuteSqlRaw("EXECUTE Book.proc_BorrowOrder_Lend {0},{1},{2}", TempData["PageRoleID"], LendBook.BookID, LendBook.EstimatedReturnTime).ToString();
            //        await _context.SaveChangesAsync();
            //    }
            //    catch (DbUpdateConcurrencyException)
            //    {
            //        if (!BookInfoExists(LendBook.BookID))
            //        {
            //            return NotFound();
            //        }
            //        else
            //        {
            //            throw;
            //        }
            //    }
            //    return View("/BookInfoes/Index");
            //    //return Content("<script language='javascript' type='text/javascript'>alert('"+ returnText + "');window.location.href= 'Index'</script>");
            //    //return Json(new { result = true, msg = "Here!" });
            //}
            if (id <= 0)
            {
                return NotFound();
            }

            var returnText = _context.Database.ExecuteSqlRaw("EXECUTE Book.proc_BorrowOrder_Lend {0},{1},{2}", TempData.Peek("PageRoleID"), id, LendBook.EstimatedReturnTime).ToString();

            if (returnText == "2")
                return Content("<script >alert('Successed lend book, you can take the book away after paying the fee.');window.open('" + Url.Content("/BookInfoes/Index") + "', '_self')</script >", "text/html");
            else
                return Content("<script >alert('Failed! Asking manager for help.');window.open('" + Url.Content("/BookInfoes/Index") + "', '_self')</script >", "text/html");

            //return Json(new { result = true, msg = returnText ,ms1 = TempData.Peek("PageRoleID"), ms2 = id , ms3 = LendBook.EstimatedReturnTime});
            //return Redirect("/BookInfoes/Index");
        }

        // POST: BookInfoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BookID,BookName,BookAuthor,BookInventory,BookCollectionTime,BookeBorrow")] BookInfo bookInfo)
        {
            if (id != bookInfo.BookID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bookInfo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookInfoExists(bookInfo.BookID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
                //return Json(new { result = true, msg = "Here!" });
            }
            return View(bookInfo);
        }

        // GET: BookInfoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bookInfo = await _context.BookInfo
                .FirstOrDefaultAsync(m => m.BookID == id);
            if (bookInfo == null)
            {
                return NotFound();
            }

            return View(bookInfo);
        }

        // POST: BookInfoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var bookInfo = await _context.BookInfo.FindAsync(id);
            _context.BookInfo.Remove(bookInfo);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BookInfoExists(int id)
        {
            return _context.BookInfo.Any(e => e.BookID == id);
        }
    }
}

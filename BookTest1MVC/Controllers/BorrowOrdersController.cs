using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BookTest1MVC.Data;
using BookTest1MVC.Models;

namespace BookTest1MVC.Controllers
{
    public class BorrowOrdersController : Controller
    {
        private readonly BookTest1MVCContext _context;

        public BorrowOrdersController(BookTest1MVCContext context)
        {
            _context = context;
        }

        // GET: BorrowOrders
        public async Task<IActionResult> Index()
        {
            return View(await _context.BorrowOrder.ToListAsync());
        }

        // GET: BorrowOrders/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var borrowOrder = await _context.BorrowOrder
                .FirstOrDefaultAsync(m => m.OrderId == id);
            if (borrowOrder == null)
            {
                return NotFound();
            }
            return View(borrowOrder);
        }

        // GET: BorrowOrders/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: BorrowOrders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("OrderId,BookId,UserId,OrderStatus,BorrowTime,EstimatedReturnTime,ActualReturnTime,BorrowBooksCost,OverdueDays,PenaltyAmount,TotalCost,PaymentMethod")] BorrowOrder borrowOrder)
        {
            if (ModelState.IsValid)
            {
                _context.Add(borrowOrder);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(borrowOrder);
        }

        // GET: BorrowOrders/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var borrowOrder = await _context.BorrowOrder.FindAsync(id);
            if (borrowOrder == null)
            {
                return NotFound();
            }
            return View(borrowOrder);
        }

        // POST: BorrowOrders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("OrderId,BookId,UserId,OrderStatus,BorrowTime,EstimatedReturnTime,ActualReturnTime,BorrowBooksCost,OverdueDays,PenaltyAmount,TotalCost,PaymentMethod")] BorrowOrder borrowOrder)
        {
            if (id != borrowOrder.OrderId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(borrowOrder);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BorrowOrderExists(borrowOrder.OrderId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(borrowOrder);
        }

        // GET: BorrowOrders/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var borrowOrder = await _context.BorrowOrder
                .FirstOrDefaultAsync(m => m.OrderId == id);
            if (borrowOrder == null)
            {
                return NotFound();
            }

            return View(borrowOrder);
        }

        // POST: BorrowOrders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var borrowOrder = await _context.BorrowOrder.FindAsync(id);
            _context.BorrowOrder.Remove(borrowOrder);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult proc_BorrowOrder_Return()
        {
            return View("BookReturn");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> proc_BorrowOrder_Return(int id, [Bind("PaymentMethod")] ReturnBook ReturnBook)
        {
            if (id <= 0)
            {
                return NotFound();
            }

            var returnText = _context.Database.ExecuteSqlRaw("EXECUTE Book.proc_BorrowOrder_Return {0},{1}", id, ReturnBook.PaymentMethod).ToString();

            if (returnText == "2")
                return Content("<script >alert('Successed return the book');window.open('" + Url.Content("/BorrowOrders/Index") + "', '_self')</script >", "text/html");
            else
                return Content("<script >alert('Failed! Asking manager for help. error code:"+ returnText + "');window.open('" + Url.Content("/BorrowOrders/Index") + "', '_self')</script >", "text/html");

        }

        private bool BorrowOrderExists(int id)
        {
            return _context.BorrowOrder.Any(e => e.OrderId == id);
        }

    }
}

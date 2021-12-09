using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using BookTest1MVC.Data;
using BookTest1MVC.Models;
using Microsoft.AspNetCore.Authorization;

namespace BookTest1MVC.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class DetailsController : Controller
    {
        private readonly BookTest1MVCContext _context;

        public DetailsController(BookTest1MVCContext context)
        {
            _context = context;
        }

        // GET: Details
        //public async Task<IActionResult> Index()
        //{
        //    //var query = from Detail in _context.Set<Detail>()
        //    //            join BorrowOrder in _context.Set<BorrowOrder>()
        //    //                on Detail.UserID equals BorrowOrder.UserId
        //    //            select new { Detail, BorrowOrder };
        //    return View(await _context.Detail.ToListAsync());
        //}

        public async Task<IActionResult> Index(string searchString)
        {
            var Users = from m in _context.Detail
                        select m;

            if (!String.IsNullOrEmpty(searchString))
            {
                Users = Users.Where(s => s.FirstName.Contains(searchString) || s.MiddleName.Contains(searchString) || s.LastName.Contains(searchString));
            }

            return View(await Users.ToListAsync());
        }

        // GET: Details/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var detail = await _context.Detail
                .FirstOrDefaultAsync(m => m.UserID == id);
            if (detail == null)
            {
                return NotFound();
            }

            return View(detail);
        }

        // GET: Details/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Details/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UserID,UserName,UserPassword,FirstName,MiddleName,LastName,UserMail,UserNumber,UserAddress,SelectPriv,BorrowPriv,CreationTime")] Detail detail)
        {
            if (ModelState.IsValid)
            {
                _context.Add(detail);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(detail);
        }

        // GET: Details/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var detail = await _context.Detail.FindAsync(id);
            if (detail == null)
            {
                return NotFound();
            }
            return View(detail);
        }

        // POST: Details/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("UserID,UserName,UserPassword,FirstName,MiddleName,LastName,UserMail,UserNumber,UserAddress,SelectPriv,BorrowPriv,CreationTime")] Detail detail)
        {
            if (id != detail.UserID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(detail);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DetailExists(detail.UserID))
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
            return View(detail);
        }

        // GET: Details/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var detail = await _context.Detail
                .FirstOrDefaultAsync(m => m.UserID == id);
            if (detail == null)
            {
                return NotFound();
            }

            return View(detail);
        }

        // POST: Details/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var detail = await _context.Detail.FindAsync(id);
            _context.Detail.Remove(detail);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DetailExists(int id)
        {
                return _context.Detail.Any(e => e.UserID == id);
        }
    }
}

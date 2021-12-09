using BookTest1MVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;

namespace BookTest1MVC.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
            
        }
        public IActionResult Index()
        {
            //return View();
            //_signManager.SignOutAsync();
            //SetTitle();
            if (TempData.Peek("PageRole").ToString() == "Manager")
                return Redirect("/BorrowOrders/Index");
            if (TempData.Peek("PageRole").ToString() == "Borrow")
                return Redirect("/BookInfoes/Index");
            if (TempData.Peek("PageRole").ToString() == "Select")
                return Redirect("/BookInfoes/Index");
            return View();
        }

        //public void SetTitle()
        //{
        //    var userName = User.Identity.Name;
        //}

        //public string GetRole()
        //{
        //    var role = "";
        //    if (User.IsInRole("Manager"))
        //        role = "Manager";
        //    if (User.IsInRole("Select"))
        //        role = "Select";
        //    if (User.IsInRole("Borrow"))
        //        role = "Borrow";
        //    if (User.IsInRole("Administrator"))
        //        role = "Administrator";
        //    return role;
        //}

        public IActionResult Privacy()
        {
            //return Redirect("/BorrowOrders/Index");
            return View();
        }

        public IActionResult Logout()
        {
            TempData["PageRoleName"] = null;
            TempData["PageRole"] = null;
            return RedirectToAction("Logout", "Account");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

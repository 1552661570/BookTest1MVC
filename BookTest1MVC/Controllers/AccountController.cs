using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using BookTest1MVC.Data;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookTest1MVC.Controllers
{
    public class AccountController : Controller
    {
        private readonly BookTest1MVCContext _context;

        public AccountController(BookTest1MVCContext context)
        {
            _context = context;
        }

        [TempData]
        public string PageRole { get; set; }
        [TempData]
        public string PageRoleName { get; set; }
        [TempData]
        public int PageRoleID { get; set; }

        public IActionResult Login()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Login(string userName, string password)
        {
            var detail = from u in _context.Detail
                         select u;
            if (!String.IsNullOrEmpty(userName))
            {
                detail = detail.Where(s => s.UserName.Equals(userName));
            }
            if (detail == null)
            {
                return NotFound();
            }
            List<string> pwdTemp = new List<string>(1);
            pwdTemp = detail.Select(a => a.UserPassword).ToList();
            if (pwdTemp.Count() == 0 && !userName.Equals("admin"))
            {
                return Json(new { result = false, msg = "Wrong username or password!" });
            }
            if (userName.Equals("admin") && password.Equals("123456"))
            {
                SetClaim(userName, password, "Administrator");
                PageRoleID = 1;
                PageRole = "Administrator";
                PageRoleName = userName;
                return Redirect("/Home/Index");
            }
            else if (pwdTemp[0] == password)
            {
                var IDTemp = detail.Select(a => a.UserID).ToList();
                PageRoleID = IDTemp[0];
                PageRoleName = userName;
                if (detail.Select(a => a.SelectPriv).ToList()[0] is true && detail.Select(a => a.BorrowPriv).ToList()[0] is true)
                {
                    SetClaim(userName, password, "Manager");
                    PageRole = "Manager";
                    return Redirect("/BorrowOrders/Index");
                }
                else if (detail.Select(a => a.BorrowPriv).ToList()[0] is true)
                {
                    SetClaim(userName, password, "Borrow");
                    PageRole = "Borrow";
                    PageRoleName = userName;
                    return Redirect("/BookInfoes/Index");
                }
                else if (detail.Select(a => a.SelectPriv).ToList()[0] is true)
                {
                    SetClaim(userName, password, "Select");
                    PageRole = "Select";
                    return Redirect("/BookInfoes/Index");
                }
                return Json(new { result = false, msg = "The used banned." });
            }
            return Json(new { result = false, msg = "Wrong username or password!" });
        }

        public async void SetClaim(string userName, string password, string role)
        {
            var claims = new List<Claim>(){
                new Claim(ClaimTypes.NameIdentifier,userName), new Claim(ClaimTypes.Name,userName),new Claim("password",password),new Claim(ClaimTypes.Role, role)
                };
            var userPrincipal = new ClaimsPrincipal(new ClaimsIdentity(claims, "BookStore"));
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, userPrincipal, new AuthenticationProperties
            {
                ExpiresUtc = DateTime.UtcNow.AddMinutes(20),
                IsPersistent = false,
                AllowRefresh = false
            });
        }

        public IActionResult Logout()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Redirect("/Account/Login");
        }

    }
}
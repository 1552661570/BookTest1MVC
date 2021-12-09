﻿using BookTest1MVC.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BookTest1MVC.Controllers
{
    public class RoleController : ControllerBase
    {
        private readonly UserManager<User> userManager;

        public RoleController(UserManager<User> userManager)
        {
            this.userManager = userManager;
        }

        // GET api/role
        [Authorize]
        public async Task<IEnumerable<string>> Get()
        {
            var userId = User.FindFirstValue(ClaimTypes.Name);
            var user = await userManager.FindByIdAsync(userId);
            var role = await userManager.GetRolesAsync(user);
            return role;
        }
    }
}

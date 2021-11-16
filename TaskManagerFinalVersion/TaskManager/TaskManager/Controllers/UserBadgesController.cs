using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TaskManager.ApplicationLogic.Services.Abstractions;

namespace TaskManager.Controllers
{
    [Authorize]
    public class UserBadgesController : Controller
    {
        private readonly IUserBadgesService _userBadgesService;
        private readonly IUsersService _usersService;

        public UserBadgesController(IUserBadgesService userBadgesService,
                                    IUsersService usersService)
        {
            _userBadgesService = userBadgesService;
            _usersService = usersService;
        }

        // GET: /UserBadges/SeeBadges
        public async Task<ActionResult> SeeBadges()
        {
            var currentUser = await _usersService.GetCurrentUser(HttpContext.User);
            ViewData["Score"] = currentUser.Score;
            return View(_userBadgesService.FindBadges(currentUser));
        }
    }
}

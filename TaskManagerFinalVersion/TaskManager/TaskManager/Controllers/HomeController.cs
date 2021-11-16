using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TaskManager.ApplicationLogic.Services.Abstractions;
using TaskManager.Models;

namespace TaskManager.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        private readonly IImageService _imageService;
        private readonly IUsersService _usersService;

        public HomeController(IImageService imageService, IUsersService usersService)
        {
            _imageService = imageService;
            _usersService = usersService;
        }

        public async Task<IActionResult> Index()
        {
            var user = await _usersService.GetCurrentUser(HttpContext.User);
            if ( user != null)
            {
                return RedirectToAction("Index", "Projects");
            }
            return View();
        }

        public IActionResult About()
        {
            return View();
        }

        public IActionResult Contact()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public async Task<ActionResult> RetrieveImage()
        {
            var user = await _usersService.GetCurrentUser(HttpContext.User);
            var imageDataURL = _imageService.GetProfileImage(user);
            
            return Json(imageDataURL);
        }

        public async Task<ActionResult> GetTemporaryImage(List<IFormFile> image)
        {
            var imageDataURL = await _imageService.GetTemporaryProfileImage(image);

            return Json(imageDataURL);
        }
    }
}

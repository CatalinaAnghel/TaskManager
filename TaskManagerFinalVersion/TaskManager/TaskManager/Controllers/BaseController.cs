using Microsoft.AspNetCore.Mvc;
using System;
using TaskManager.Models;

namespace TaskManager.Controllers
{
    public class BaseController:Controller
    {
        protected DropdownViewModel BuildDropdownViewModel(Array Levels)
        {
            var model = new DropdownViewModel
            {
                Levels = Levels
            };

            return model;
        }
    }
}

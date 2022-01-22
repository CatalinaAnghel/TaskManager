using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace TaskManager.ApplicationLogic.Dtos
{
    public class RegisterDto
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public bool EmailConfirmed { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Score { get; set; }
        public List<IFormFile> ProfileImage { get; set; }
    }
}

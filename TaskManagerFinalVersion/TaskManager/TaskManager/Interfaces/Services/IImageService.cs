using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskManager.Models;

namespace TaskManager.Interfaces.Services
{
    public interface IImageService
    {
        public void SetProfileImage(List<IFormFile> profileImage, Users user);
        public string GetProfileImage(Users user);
        public Task<string> GetTemporaryProfileImage(List<IFormFile> profileImage);
    }
}

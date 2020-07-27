using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using TaskManager.Interfaces.Services;
using TaskManager.Models;

namespace TaskManager.Services
{
    public class ImageService : IImageService
    {
        //private readonly IUsersService _usersService;

        public ImageService()
        {
            //_usersService = usersService;
        }


        public async void SetProfileImage(List<IFormFile> profileImage, Users user)
        {
            if (profileImage != null)
            {
                foreach (var item in profileImage)
                {
                    if (item.Length > 0)
                    {
                        using var stream = new MemoryStream();
                        await item.CopyToAsync(stream);
                        user.ProfileImage = stream.ToArray();
                    }
                }
            }
        }

        public async Task<string> GetProfileImage(Users user)
        {
            //var user = await _usersService.GetCurrentUser(httpContext.User);
            var image = user.ProfileImage;
            try
            {
                string imageBase64Data = Convert.ToBase64String(image);
                string imageDataURL = string.Format("data:image/jpg;base64,{0}", imageBase64Data);
                return imageDataURL;
            }
            catch (ArgumentNullException)
            {
                return null;
            }
        }

        public async Task<string> GetTemporaryProfileImage(List<IFormFile> profileImage)
        {
            byte[] temporaryImage= null;
            if (profileImage != null)
            {
                foreach (var item in profileImage)
                {
                    if (item.Length > 0)
                    {
                        using var stream = new MemoryStream();
                        await item.CopyToAsync(stream);
                        temporaryImage = stream.ToArray();
                    }
                }
                try
                {
                    string imageBase64Data = Convert.ToBase64String(temporaryImage);
                    string imageDataURL = string.Format("data:image/jpg;base64,{0}", imageBase64Data);
                    return imageDataURL;
                }
                catch (ArgumentNullException)
                {
                    return null;
                }
            }
            return null;
        }
    }
}

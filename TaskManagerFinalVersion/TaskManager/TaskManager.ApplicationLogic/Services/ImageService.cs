using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using TaskManager.ApplicationLogic.Services.Abstractions;
using TaskManager.DataAccess.DataModels;

namespace TaskManager.ApplicationLogic.Services
{
    public class ImageService : IImageService
    {
        public ImageService()
        {
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

        public string GetProfileImage(Users user)
        {
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

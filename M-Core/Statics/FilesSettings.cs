using Microsoft.AspNetCore.Http;
using M_Core.Resources;
using M_Core.Statics;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Image = SixLabors.ImageSharp.Image;

namespace M_Core.Statics
{
    public static class FilesSettings
    {
        //Images

        public static string ImagesPath = "/Images";
        public static string DefaultUserImagePath = "/Images/defaultUserImage/Male Image.jpg";
        public static string DefaultFemaleImagePath = "/Images/defaultUserImage/Female image.webp";
        public static string AllowedImageExtensions = ".jpg,.jpeg,.png,.webp,.gif,.bmp";
        public static int MaxFileSizeInMB = 5;
        public static int MaxFileSizeInBytes = MaxFileSizeInMB * 1024 * 1024;
        public static int MaxWidthInPX = 2500;
        public static int MaxHightInPX = 2500;

        public static int UserMaxWidthInPX = 400;
        public static int UserMaxHightInPX = 400;

        public static int UserMinWidthInPX = 160;
        public static int UserMinHightInPX = 160;

        public static int MinWidthInPX = 600;
        public static int MinHightInPX = 315;

        //Video
        public static string VideosPath = "/Videos";
        public static string AllowedVideoExtensions = ".mp4,.mov,.avi,.mkv,.wmv";
        public static int MaxVideoSizeInBytes = 50 * 1024 * 1024;


        public static ValidationResult AllowUplaod(IFormFile ClientFile)
        {

            var allowedImageExtensions = AllowedImageExtensions.Split(',');
            var allowedVideoExtensions = AllowedVideoExtensions.Split(',');
            var fileExtension = Path.GetExtension(ClientFile.FileName).ToLower();

            if (allowedImageExtensions.Contains(fileExtension))
            {
                if (ClientFile.Length > MaxFileSizeInBytes)

                    return ValidationResult.Fail($"The Uplaoded image exceeds the maximum allowed size of {MaxFileSizeInBytes / (1024 * 1024)} MB.");

                using (var image = Image.Load(ClientFile.OpenReadStream()))
                {
                    if (image.Width > MaxWidthInPX || image.Height > MaxHightInPX)
                    {
                        return ValidationResult.Fail($"Image dimensions exceed the maximum allowed dimensions of {MaxWidthInPX}x{MaxHightInPX} pixels.");
                    }
                    if (image.Width < MinWidthInPX || image.Height < MinHightInPX)
                    {
                        return ValidationResult.Fail($"Image dimensions exceed the minimum allowed dimensions of {MinWidthInPX}x{MinHightInPX} pixels.");
                    }
                }
                return ValidationResult.Success("Image");
            }
            else if (allowedVideoExtensions.Contains(fileExtension))
            {
                if (ClientFile.Length > FilesSettings.MaxVideoSizeInBytes)
                    return ValidationResult.Fail($"The uploaded video exceeds the maximum allowed size of {MaxVideoSizeInBytes / (1024 * 1024)} MB.");

                return ValidationResult.Success("video");
            }
            return ValidationResult.Fail($"Invalid file extension. Allowed extensions are {AllowedImageExtensions} For Images and {AllowedVideoExtensions} For videos.");
        }



        public static ValidationResult UserImageAllowUplaod(IFormFile ClientFile)
        {
            if (ClientFile.Length > MaxFileSizeInBytes)

                return ValidationResult.Fail($"The Uplaoded image exceeds the maximum allowed size of {MaxFileSizeInBytes / (1024 * 1024)} MB.");

            var allowedExtensions = AllowedImageExtensions.Split(',');
            var fileExtension = Path.GetExtension(ClientFile.FileName).ToLower();

            if (!allowedExtensions.Contains(fileExtension))
                return ValidationResult.Fail($"Invalid file extension. Allowed extensions are {AllowedImageExtensions}.");

            using (var image = Image.Load(ClientFile.OpenReadStream()))
            {
                if (image.Width > UserMaxWidthInPX || image.Height > UserMaxHightInPX)
                {
                    return ValidationResult.Fail($"Image dimensions exceed the maximum allowed dimensions of {UserMaxWidthInPX}x{UserMaxHightInPX} pixels.");
                }
                if (image.Width < UserMinWidthInPX || image.Height < UserMinHightInPX)
                {
                    return ValidationResult.Fail($"Image dimensions exceed the minimum allowed dimensions of {UserMinWidthInPX}x{UserMinHightInPX} pixels.");
                }
            }

            return ValidationResult.Success("video");
        }
    }
}
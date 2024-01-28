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

namespace M_Core.Helpers
{
    public class AllowFile
    {

        //    public ValidationResult AllowUpload(IFormFile file)
        //    {
        //        var allowdImageExtensions = FilesSettings.AllowedImageExtensions.Split(",");
        //        var allowdVideoExtensions = FilesSettings.AllowedVideoExtensions.Split(",");
        //        var fileExtension = Path.GetExtension(file.FileName).ToLower();

        //        if(fileExtension.Contains(allowdImageExtensions))
        //        {
        //            if(file.Length > FilesSettings.MaxFileSizeInBytes)
        //            {
        //                return ValidationResult.Fail($"The Uplaoded image exceeds the maximum allowed size of {FilesSettings.MaxFileSizeInBytes / (1024 * 1024)} MB.");
        //            }
        //            using(var image = Image.Load(file.OpenReadStream()))
        //            {
        //                if(image.Width > FilesSettings.MaxWidthInPX || image.Height > FilesSettings.MaxHightInPX)
        //                    return ValidationResult.Fail($"Image dimensions exceed the maximum allowed dimensions of {FilesSettings.MaxWidthInPX}x{FilesSettings.MaxHightInPX} pixels.");

        //                if(image.Width > FilesSettings.MinWidthInPX || image.Height > FilesSettings.MinHightInPX)
        //                    return ValidationResult.Fail($"Image dimensions exceed the minimum allowed dimensions of {FilesSettings.MinWidthInPX}x{FilesSettings.MinHightInPX} pixels.");

        //            }
        //            return ValidationResult.Success("Image");
        //        }
        //        else if (fileExtension.Contains(allowdVideoExtensions))
        //        {
        //            if (file.Length > FilesSettings.MaxVideoSizeInBytes)
        //            {
        //                return ValidationResult.Fail(_localizer[LocalizerStatics.FileLength, FilesSettings.MaxVideoSizeInBytes]);
        //            }

        //            return ValidationResult.Success("Video");
        //        }

        //        return ValidationResult.Fail($"Invalid file extension. Allowed extensions are {FilesSettings.AllowedImageExtensions} For Images and {FilesSettings.AllowedVideoExtensions} For videos.");
        //    }

        //    public  ValidationResult UserImageAllowUplaod(IFormFile ClientFile)
        //    {
        //        if (ClientFile.Length > FilesSettings.MaxFileSizeInBytes)
        //            return ValidationResult.Fail($"The Uplaoded image exceeds the maximum allowed size of {FilesSettings.MaxFileSizeInBytes / (1024 * 1024)} MB.");

        //        var allowdImageExtensions = FilesSettings.AllowedImageExtensions.Split(",");
        //        var fileExtension = Path.GetExtension(ClientFile.FileName).ToLower();

        //        if (!allowdImageExtensions.Contains(fileExtension))
        //            return ValidationResult.Fail($"Invalid file extension. Allowed extensions are {FilesSettings.AllowedImageExtensions}.");

        //        using (var image = Image.Load(ClientFile.OpenReadStream()))
        //        {
        //            if (image.Width > FilesSettings.UserMaxWidthInPX || image.Height > FilesSettings.UserMaxHightInPX)
        //            {
        //                return ValidationResult.Fail($"Image dimensions exceed the maximum allowed dimensions of {UserMaxWidthInPX}x{UserMaxHightInPX} pixels.");
        //            }
        //            if (image.Width < FilesSettings.UserMinWidthInPX || image.Height < FilesSettings.UserMinHightInPX)
        //            {
        //                return ValidationResult.Fail($"Image dimensions exceed the minimum allowed dimensions of {FilesSettings.UserMinWidthInPX}x{FilesSettings.UserMinHightInPX} pixels.");
        //            }
        //        }
        //        return ValidationResult.Success("user Image");
        //    }
    }
}

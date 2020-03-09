using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using static MyResume.WebApp.ModelView.AchievementViewModel;

namespace MyResume.WebApp.Utilities
{
    public static class FileProcessing
    {

        public static string UploadAvatarPng(IFormFile ImageFile, string userName, Controller controller, IConfiguration config, IWebHostEnvironment webHostEnvironment)
        {
            string imageFilePath = null;
            var storageFilePath = @"images\AvatarImages";

            //if (System.IO.File.Exists(fileName))
            //{
            //    System.IO.File.Delete(fileName);
            //}

            if (ImageFile != null)
            {
                var maxFileSize = Convert.ToInt32(config.GetSection("FileUploadSettings")["MaxFileSize"]);

                if (ImageFile.Length == 0)
                {
                    controller.ModelState.AddModelError("", $"File is empty");
                    return null;
                }

                if (ImageFile.Length > maxFileSize)
                {
                    controller.ModelState.AddModelError("", $"Max file size allowed is {maxFileSize / 1024} KB");

                    return null;
                }

                var fileExtention = Path.GetExtension(ImageFile.FileName).ToLower();

                if (!fileExtention.Equals(".png")) // this might be faked, should read the file signature bytes of the file to confirm its a PNG file
                {

                    controller.ModelState.AddModelError("", "Only PNG images are supported");

                    return null;
                }

                string uploadsFolderPath = Path.Combine(webHostEnvironment.WebRootPath, storageFilePath); // This will find the storage folder in wwwroot
                //uniqueFileName = Guid.NewGuid().ToString() + "_" + model.AvatarImage.FileName;
                var splittResult = storageFilePath.Split('\\');
                var uploadsFolderName = splittResult[^1];

                var imageName = $"{uploadsFolderName}_{userName}{fileExtention}";


                var FilePath = Path.Combine(uploadsFolderPath, imageName);

                using (var fileStream = new FileStream(FilePath, FileMode.Create)) // overwrites the file if it already exists
                {

                    ImageFile.CopyTo(fileStream);
                }

                imageFilePath = $"~/{storageFilePath}/{imageName}";
            }

            if (imageFilePath != null)
            {
                controller.ViewBag.FileSuccessMessage = " New avatar image was successfully uploaded"; // idk if this is a good idea. Achievement
            }

            return imageFilePath;
        }


        public static string[] UploadItemGalleryPngs(BidingBridgeIFormFile[] bidingBridgeIFormFiles, string userName,Guid achievementId ,Controller controller, IConfiguration config, IWebHostEnvironment webHostEnvironment)
        {
            string storageFilePath = "images/ItemThumbnails";

            string[] imageFilePath = new string[bidingBridgeIFormFiles.Length];

            for (int i = 0; i < bidingBridgeIFormFiles.Length; i++)
            {

                if (bidingBridgeIFormFiles[i].GalleryImage!= null)
                {
                    var maxFileSize = Convert.ToInt32(config.GetSection("FileUploadSettings")["MaxFileSize"]);

                    if (bidingBridgeIFormFiles[i].GalleryImage.Length == 0)
                    {
                        controller.ModelState.AddModelError("", $"File is empty");
                        return null;
                    }

                    if (bidingBridgeIFormFiles[i].GalleryImage.Length > maxFileSize)
                    {
                        controller.ModelState.AddModelError("", $"Max file size allowed is {maxFileSize / 1024} KB");

                        return null;
                    }

                    var fileExtention = Path.GetExtension(bidingBridgeIFormFiles[i].GalleryImage.FileName).ToLower();

                    if (!fileExtention.Equals(".png")) // this might be faked, should read the file signature bytes of the file to confirm its a PNG file
                    {

                        controller.ModelState.AddModelError("", "Only PNG images are supported");

                        return null;
                    }

                    string uploadsFolderPath = Path.Combine(webHostEnvironment.WebRootPath, storageFilePath); // This will find the storage folder in wwwroot
                                                                                                              //uniqueFileName = Guid.NewGuid().ToString() + "_" + model.AvatarImage.FileName;
                                                                                                              //var splittResult = storageFilePath.Split('/');
                                                                                                              //var uploadsFolderName = splittResult[^1];

                    var imageName = $"{userName}_{achievementId.ToString()}_GalleryImg{i}{fileExtention}";

                    var FilePath = Path.Combine(uploadsFolderPath, imageName);

                    using (var fileStream = new FileStream(FilePath, FileMode.Create)) // overwrites the file if it already exists
                    {

                        bidingBridgeIFormFiles[i].GalleryImage.CopyTo(fileStream);
                    }

                    imageFilePath[i] = $"~/{storageFilePath}/{imageName}";
                }
                else
                {
                    controller.ModelState.AddModelError("", "No file to upload");
                }

                if (imageFilePath[i] != null)
                {
                    controller.ViewData["GalleryImage" + i] = $"Gallery image nr:{i} was successfully uploaded"; // idk if this is a good idea.
                }


            }
            return imageFilePath;

        }
    }

}
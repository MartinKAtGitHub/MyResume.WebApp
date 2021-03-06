﻿using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MyResume.WebApp.Models;
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
            var avatarImagesDir = config.GetValue<string>("FileUploadSettings:AvatarImageDir");
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

                if (!fileExtention.Equals(config.GetValue<string>("FileUploadSettings:AcceptedFileType"))) // this might be faked, should read the file signature bytes of the file to confirm its a PNG file
                {
                    controller.ModelState.AddModelError("", "Only PNG images are supported");

                    return null;
                }

                string uploadsFolderPath = Path.Combine(webHostEnvironment.WebRootPath, avatarImagesDir); // This will find the storage folder in wwwroot
                //uniqueFileName = Guid.NewGuid().ToString() + "_" + model.AvatarImage.FileName;
                //var splittResult = storageFilePath.Split('\\');
                //var uploadsFolderName = splittResult[^1];

                var imageName = $"{config.GetValue<string>("FileUploadSettings:AvatarImagePreFix")}{userName}{fileExtention}";


                var FilePath = Path.Combine(uploadsFolderPath, imageName);

                using (var fileStream = new FileStream(FilePath, FileMode.Create)) // overwrites the file if it already exists
                {

                    ImageFile.CopyTo(fileStream);
                }

                imageFilePath = $@"\{avatarImagesDir}\{imageName}";
            }

            if (imageFilePath != null)
            {
                controller.ViewBag.FileSuccessMessage = " New avatar image was successfully uploaded"; // idk if this is a good idea. Achievement
            }

            return imageFilePath;
        }

        public static string[] UploadItemGalleryPngs(List<IFormFile> IFormFiles, string userName, Guid achievementId, Controller controller, IConfiguration config, IWebHostEnvironment webHostEnvironment)
        {
            string storageFilePath = "images/ItemThumbnails";

            string[] imageFilePath = new string[IFormFiles.Count];

            for (int i = 0; i < IFormFiles.Count; i++)
            {

                if (IFormFiles[i] != null)
                {
                    var maxFileSize = Convert.ToInt32(config.GetSection("FileUploadSettings")["MaxFileSize"]);

                    if (IFormFiles[i].Length == 0)
                    {
                        controller.ModelState.AddModelError("", $@" {IFormFiles[i].FileName}File is empty");
                        return null;
                    }

                    if (IFormFiles[i].Length > maxFileSize)
                    {
                        controller.ModelState.AddModelError("", $@" {IFormFiles[i].FileName} needs to be smaller then {maxFileSize / 1024} KB");

                        return null;
                    }

                    var fileExtention = Path.GetExtension(IFormFiles[i].FileName).ToLower();

                    if (!fileExtention.Equals(config.GetValue<string>("FileUploadSettings:AcceptedFileType"))) // this might be faked, should read the file signature bytes of the file to confirm its a PNG file
                    {
                        controller.ModelState.AddModelError("", $@" {IFormFiles[i].FileName} - file type not supported, only (.png) is allowed");
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

                        IFormFiles[i].CopyTo(fileStream);
                    }

                    //imageFilePath[i] = $"~/{storageFilePath}/{imageName}"; // the ~/ is used by asp.net core ? to indicate the root folder of the application.
                    imageFilePath[i] = $"/{storageFilePath}/{imageName}";
                }

                if (imageFilePath[i] != null)
                {
                    controller.ViewData["GalleryImage" + i] = $"Gallery image nr:{i} was successfully uploaded"; // idk if this is a good idea.
                }


            }
            return imageFilePath;

        }

        public static void DeleteAvatarImage(string userName, IConfiguration config, IWebHostEnvironment webHostEnvironment)
        {

            var RootPath = webHostEnvironment.WebRootPath; // the wwwroot absolute path
            var avatarImagesDir = config.GetValue<string>("FileUploadSettings:AvatarImageDir");
            var imageFileName = $"{config.GetValue<string>("FileUploadSettings:AvatarImagePreFix")}{userName}{config.GetValue<string>("FileUploadSettings:AcceptedFileType")}";

            var absoluteImageFilePath = Path.Combine(RootPath, avatarImagesDir, imageFileName);

            try
            {
                if (File.Exists(absoluteImageFilePath))
                {
                    File.Delete(absoluteImageFilePath);
                }

            }
            catch (IOException)
            {

                throw;
            }


        }


        public static void DeleteGalleryImage(string userName, Guid itemID, int galleryImgIndex, IConfiguration config, IWebHostEnvironment webHostEnvironment)
        {

            var RootPath = webHostEnvironment.WebRootPath; // the wwwroot absolute path
            var avatarImagesDir = config.GetValue<string>("FileUploadSettings:GalleryImageDir");
            var imageFileName = $"{userName}_{itemID.ToString()}_GalleryImg{galleryImgIndex}{config.GetValue<string>("FileUploadSettings:AcceptedFileType")}";

            var absoluteImageFilePath = Path.Combine(RootPath, avatarImagesDir, imageFileName);

            try
            {
                if (File.Exists(absoluteImageFilePath))
                {
                    File.Delete(absoluteImageFilePath);
                }

            }
            catch (IOException)
            {

                throw;
            }
        }

        public static void DeleteAllGalleryImages(string userName, Achievement item, IConfiguration config, IWebHostEnvironment webHostEnvironment)
        {

            var RootPath = webHostEnvironment.WebRootPath; // the wwwroot absolute path
            var avatarImagesDir = config.GetValue<string>("FileUploadSettings:GalleryImageDir");


            foreach (var imageData in item.ItemGalleryImageFilePaths)
            {
                var imageFileName = $"{userName}_{item.AchievementId.ToString()}_GalleryImg{imageData.GalleryIndex}{config.GetValue<string>("FileUploadSettings:AcceptedFileType")}";

                var absoluteImageFilePath = Path.Combine(RootPath, avatarImagesDir, imageFileName);

                try
                {
                    if (File.Exists(absoluteImageFilePath))
                    {
                        File.Delete(absoluteImageFilePath);
                    }

                }
                catch (IOException)
                {

                    throw;
                } 
            }
        }
    }

}
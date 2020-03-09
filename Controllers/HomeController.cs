using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MyResume.WebApp.Models;
using MyResume.WebApp.ModelView;
using MyResume.WebApp.Data;
using Microsoft.AspNetCore.Http;
using MyResume.WebApp.Utilities;

namespace MyResume.WebApp.Controllers
{

    public class HomeController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUserInfoRepo _userInfoRepo;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IConfiguration _config;
        private readonly IAchievementRepo _achievementRepo;

        public HomeController(UserManager<ApplicationUser> userManager, IUserInfoRepo userInfoRepo, IWebHostEnvironment webHostEnvironment,
            IConfiguration config, IAchievementRepo achievementRepo)
        {
            _userManager = userManager;
            _userInfoRepo = userInfoRepo;
            _webHostEnvironment = webHostEnvironment;
            _config = config;
            _achievementRepo = achievementRepo;
        }

        public IActionResult Index(string searchString)
        {
            var UserSearchResult = new UserSearchViewModel();
            UserSearchResult.UsersResult = _userInfoRepo.Search(searchString).ToList();

            return View(UserSearchResult);
        }

        public IActionResult UserResume(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                ViewBag.ErrorTitle = "Can't find Resume ";
                @ViewBag.ErrorMessage = "No user is selected";
                return View("Error");
            }

            var model = new UserResumeViewModel();

            if (_userManager.GetUserId(User) == id)
            {
                model.EnableOwnerOptions = true;
            }
            else
            {
                model.EnableOwnerOptions = false;
            }

            model.UserInfo = _userInfoRepo.Read(id);
            model.Achievements = _achievementRepo.ReadAll(model.UserInfo.UserInformationId);

            return View(model);
        }


        [HttpGet]
        [Authorize]
        public IActionResult EditUserInfo()
        {
            var userInfo = _userInfoRepo.Read(_userManager.GetUserId(User));

            // For sec reason it is recommended we create a ViewModel only exposing the properties we want to edit
            var model = new EditUserInfoViewModel
            {
                FirstName = userInfo.FirstName,
                MiddleName = userInfo.MiddelName,
                LastName = userInfo.LasttName,
                AvatarImgPath = userInfo.AvatarImgPath,
                Summary = userInfo.Summary,
                MainText = userInfo.MainText,
                AvailableForContact = userInfo.AvailableForContact
            };

            return View(model);
        }


        [HttpPost]
        [Authorize]
        public IActionResult EditUserInfo(EditUserInfoViewModel model)
        {

            var userInfo = _userInfoRepo.Read(_userManager.GetUserId(User));

            if (ModelState.IsValid)
            {
                //model.AvatarImgPath = ProccessUploadedFile(model.AvatarImage, userInfo, _userManager.GetUserName(User), "images/AvatarImages");

                var result = FileProcessing.UploadAvatarPng(model.AvatarImage, _userManager.GetUserName(User), this, _config, _webHostEnvironment);

                if (result != null)
                {
                    model.AvatarImgPath = result; // Render on page
                    userInfo.AvatarImgPath = result; //Save in DB
                }
                else
                {
                    model.AvatarImgPath = userInfo.AvatarImgPath; // If no changes were made to img path, just use the same img path from before
                }

                userInfo.FirstName = model.FirstName;
                userInfo.MiddelName = model.MiddleName;
                userInfo.LasttName = model.LastName;

                userInfo.Summary = model.Summary;
                userInfo.MainText = model.MainText;
                userInfo.AvailableForContact = model.AvailableForContact;

                _userInfoRepo.Update(userInfo);
                return View(model);
            }


            model = new EditUserInfoViewModel
            {
                AvatarImgPath = userInfo.AvatarImgPath,
                Summary = userInfo.Summary,
                MainText = userInfo.MainText,
                AvailableForContact = userInfo.AvailableForContact
            };
            return View(model);
        }


        [HttpPost]
        [Authorize]
        public IActionResult DeleteItem(Guid id)
        {
            var item = _achievementRepo.Read(id);
            var userId = _userManager.GetUserId(User);

            if (item == null)
            {
                ViewBag.ErrorMessage = $"Item with Id = {id} cannot be found";
                return View("PageNotFound");
            }
            _achievementRepo.Delete(item);
            return RedirectToAction("UserResume", new { id = userId });
        }

        [Authorize]
        [HttpGet]
        public IActionResult EditItem(Guid id)
        {

            var item = _achievementRepo.Read(id);
            var userInfo = _userInfoRepo.Read(_userManager.GetUserId(User));


            // TODO research better alternatives for handling the errors -----

            if (item == null)
            {
                Response.StatusCode = 404;
                ViewBag.ErrorTitle = "Can't find item to edit";
                ViewBag.ErrorMessage = "";
                return View("Error");
            }

            if (item.UserInformationId != userInfo.UserInformationId)
            {
                Response.StatusCode = 403;
                ViewBag.ErrorTitle = "Wrong user";
                ViewBag.ErrorMessage = "Please login with the correct user to edit this item";
                return View("Error");
            }

            // ----------------------------------------------

            var model = new AchievementViewModel()
            {
                GalleryImagesArray = new AchievementViewModel.BidingBridgeIFormFile[Convert.ToInt32(_config.GetSection("FileUploadSettings")["MaxImageStorageLimit"])],
                ImageSrcPaths = new List<string>(),
                Title = item.Title,
                Summary = item.Summary,
                MainText = item.MainText,
                OrderPosition = item.OrderPosition,
                EnableComments = item.EnableComments,
                EnableRating = item.EnableRating,
            };


            var sorted = item.ItemGalleryImageFilePaths.OrderBy(i => i.GalleryIndex);
            foreach (var filePathContainer in sorted)
            {
                model.ImageSrcPaths.Add(filePathContainer.GalleryImageFilePath);
            }
            return View(model);
        }

        [HttpPost]
        [Authorize]
        public IActionResult EditItem(Guid id, AchievementViewModel model)
        {

            if (ModelState.IsValid)
            {
                var item = _achievementRepo.Read(id);
                var userInfo = _userInfoRepo.Read(_userManager.GetUserId(User));

                item.Title = model.Title;
                item.Summary = model.Summary;
                item.MainText = model.MainText;
                item.OrderPosition = model.OrderPosition;
                item.EnableComments = model.EnableComments;
                item.EnableRating = model.EnableRating;

             //  item.ItemGalleryImageFilePaths = item.ItemGalleryImageFilePaths.OrderBy(i => i.GalleryIndex).ToList();


                var filePaths = FileProcessing.UploadItemGalleryPngs(model.GalleryImagesArray, userInfo.ApplicationUser.UserName, item.AchievementId, this, _config, _webHostEnvironment);

                var maxImageLimit = Convert.ToInt32(_config.GetSection("FileUploadSettings")["MaxImageStorageLimit"]);

                if (filePaths.Length > maxImageLimit)
                {
                    ModelState.AddModelError("", "You are trying to upload more images then the allowed limit ");
                    return View(model);
                }


                var sorted = item.ItemGalleryImageFilePaths.OrderBy(i => i.GalleryIndex);

                model.ImageSrcPaths = new List<string>(); // WHY DO I HAVE TO DO THIS
                foreach (var filePathContainer in sorted)
                {
                    model.ImageSrcPaths.Add(filePathContainer.GalleryImageFilePath);
                }

                _achievementRepo.Update(item);
                return View(model);
            }

            return View(model);
        }

        [Authorize]
        [HttpGet]
        public IActionResult CreateItem()
        {

            var model = new AchievementViewModel()
            {
                GalleryImagesArray = new AchievementViewModel.BidingBridgeIFormFile[Convert.ToInt32(_config.GetSection("FileUploadSettings")["MaxImageStorageLimit"])]
            };

            return View(model);
        }

        [Authorize]
        [HttpPost]
        public IActionResult CreateItem(AchievementViewModel model)
        {
            if (ModelState.IsValid)
            {
                var userInfo = _userInfoRepo.Read(_userManager.GetUserId(User));
                // userInfo.AchievementCount++;

                var newItemId = Guid.NewGuid();

                var newItem = new Achievement
                {
                    UserInformationId = userInfo.UserInformationId,
                    AchievementId = newItemId,
                    Title = model.Title,
                    Summary = model.Summary,
                    MainText = model.MainText,
                    OrderPosition = model.OrderPosition,
                    EnableComments = model.EnableComments,
                    EnableRating = model.EnableRating,
                    ItemGalleryImageFilePaths = new List<ItemGalleryImageFilePath>()

                };

                var filePaths = FileProcessing.UploadItemGalleryPngs(model.GalleryImagesArray, userInfo.ApplicationUser.UserName, newItemId, this, _config, _webHostEnvironment);
                var maxImageLimit = Convert.ToInt32(_config.GetSection("FileUploadSettings")["MaxImageStorageLimit"]);

                if (filePaths.Length > maxImageLimit)
                {
                    ModelState.AddModelError("", "You are trying to upload more images then the allowed limit ");
                    return View();
                }

                for (int i = 0; i < maxImageLimit; i++)
                {
                    newItem.ItemGalleryImageFilePaths.Add(new ItemGalleryImageFilePath
                    {
                        Id = Guid.NewGuid().ToString(), //Id = $"{userInfo.ApplicationUser.UserName}_{model.Title}_Gallery_{i}"
                        GalleryImageFilePath = null,
                        GalleryIndex = i
                    });
                }

                if (filePaths.Length > 0)
                {
                    for (int i = 0; i < filePaths.Length; i++)
                    {
                        newItem.ItemGalleryImageFilePaths[i].GalleryImageFilePath = filePaths[i];
                    }
                }

                if (newItem.ItemGalleryImageFilePaths.Count > 0)
                {
                    newItem.ThumbnailImgPath = newItem.ItemGalleryImageFilePaths[0].GalleryImageFilePath;
                }

                _userInfoRepo.Update(userInfo);
                _achievementRepo.Create(newItem);

                return RedirectToAction("UserResume", new { id = _userManager.GetUserId(User) });
            }

            return View();
        }

        [HttpGet]
        public IActionResult DisplayItem(Guid id)
        {
            var item = _achievementRepo.Read(id);

            if (item == null)
            {
                Response.StatusCode = 404;
                ViewBag.ErrorTitle = "Can't find display item";
                ViewBag.ErrorMessage = "";
                return View("Error");
            }

            return View(item);
        }


        [Authorize]
        [HttpPost]
        public IActionResult RemoveAvatarImg()
        {
            var userInfo = _userInfoRepo.Read(_userManager.GetUserId(User));

            userInfo.AvatarImgPath = "~/images/MyResumeDefaultAvatar.png";

            _userInfoRepo.Update(userInfo);
            return RedirectToAction("EditUserInfo");
        }

    }
}
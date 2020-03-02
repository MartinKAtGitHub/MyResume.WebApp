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
                model.AvatarImgPath = ProccessUploadedFile(model.AvatarImage, userInfo, _userManager.GetUserName(User), "images/AvatarImages");

                userInfo.FirstName = model.FirstName;
                userInfo.MiddelName = model.MiddleName;
                userInfo.LasttName = model.LastName;

                userInfo.Summary = model.Summary;
                userInfo.MainText = model.MainText;
                userInfo.AvatarImgPath = model.AvatarImgPath;
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

            if(item == null)
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

            var achievement = _achievementRepo.Read(id);
            var userInfo = _userInfoRepo.Read(_userManager.GetUserId(User));


            // TODO research better alternatives for handling the errors -----

            if (achievement == null)
            {
                Response.StatusCode = 404;
                ViewBag.ErrorTitle = "Can't find item to edit";
                ViewBag.ErrorMessage = "";
                return View("Error");
            }

            if (achievement.UserInformationId != userInfo.UserInformationId)
            {
                Response.StatusCode = 403;
                ViewBag.ErrorTitle = "Wrong user";
                ViewBag.ErrorMessage = "Please login with the correct user to edit this item";
                return View("Error");
            }

            // ----------------------------------------------

            var model = new AchievementViewModel
            {
                Title = achievement.Title,
                Summary = achievement.Summary,
                MainText = achievement.MainText,
                OrderPosition = achievement.OrderPosition,
                EnableComments = achievement.EnableComments,
                EnableRating = achievement.EnableRating
            };

            return View(model);
        }

        [HttpPost]
        [Authorize]
        public IActionResult EditItem(AchievementViewModel model, Guid id)
        {

            var userInfo = _userInfoRepo.Read(_userManager.GetUserId(User));
            var item = _achievementRepo.Read(id);

            if (ModelState.IsValid)
            {
                item.ThumbnailImgPath = ProccessUploadedFile(model.ThumbnailImage, userInfo, _userManager.GetUserName(User), "images/ItemThumbnails");

                item.Title = model.Title;
                item.Summary = model.Summary;
                item.MainText = model.MainText;
                item.OrderPosition = model.OrderPosition;
                item.EnableComments = model.EnableComments;
                item.EnableRating = model.EnableRating;

                _achievementRepo.Update(item);
                return View(model);
            }

            return View(model);
        }

        [Authorize]
        [HttpGet]
        public IActionResult CreateItem()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public IActionResult CreateItem(AchievementViewModel model)
        {
            if (ModelState.IsValid)
            {
                var userInfo = _userInfoRepo.Read(_userManager.GetUserId(User));
                userInfo.AchievementCount++;

                var newAchievement = new Achievement
                {
                    UserInformationId = userInfo.UserInformationId,

                    Title = model.Title,
                    Summary = model.Summary,
                    MainText = model.MainText,
                    OrderPosition = model.OrderPosition,
                    EnableComments = model.EnableComments,
                    EnableRating = model.EnableRating
                };

                _userInfoRepo.Update(userInfo);
                _achievementRepo.Create(newAchievement);

                return RedirectToAction("UserResume", new { id = _userManager.GetUserId(User) });
            }

            return View();
        }



        private string ProccessUploadedFile(IFormFile ImageFile, UserInformation userInfo, string userName, string storageFilePath)
        {
            string imageFilePath = null;
            var defaultImage = "~/images/MyResumeDefaultAvatar.png";

            imageFilePath = defaultImage;

            if (ImageFile != null)
            {
                var maxFileSize = Convert.ToInt32(_config.GetSection("FileUploadSettings")["MaxFileSize"]);
                if (ImageFile.Length > maxFileSize)
                {
                    ModelState.AddModelError("", $"Max file size allowed is {maxFileSize / 1000} KB");

                    if (!string.IsNullOrEmpty(userInfo.AvatarImgPath))
                    {
                        return userInfo.AvatarImgPath;
                    }
                    else
                    {
                        //return string.Empty;
                        return defaultImage;
                    }
                }

                var fileExtention = Path.GetExtension(ImageFile.FileName);

                if (!fileExtention.Equals(".png"))
                {

                    ModelState.AddModelError("", "Only PNG images are supported");
                    if (!string.IsNullOrEmpty(userInfo.AvatarImgPath))
                    {
                        return userInfo.AvatarImgPath;
                    }
                    else
                    {
                        //return string.Empty;
                        return defaultImage;
                    }
                }

                string uploadsFolderPath = Path.Combine(_webHostEnvironment.WebRootPath, storageFilePath); // This will find the storage folder in wwwroot
                //uniqueFileName = Guid.NewGuid().ToString() + "_" + model.AvatarImage.FileName;
                var splittResult = storageFilePath.Split('/');
                var uploadsFolderName = splittResult[^1];

                var imageName = $"{uploadsFolderName}_{userName}{fileExtention}";


                 var FilePath = Path.Combine(uploadsFolderPath, imageName);

                //  using (var memoryStream = new MemoryStream())

                using (var fileStream = new FileStream(FilePath, FileMode.Create))
                {
                    ImageFile.CopyTo(fileStream);
                }

                imageFilePath = $"~/{storageFilePath}/{imageName}";
            }
            else
            {
                if (!string.IsNullOrEmpty(userInfo.AvatarImgPath))
                {
                    return userInfo.AvatarImgPath;
                }
            }


            return imageFilePath;
        }
    }
}
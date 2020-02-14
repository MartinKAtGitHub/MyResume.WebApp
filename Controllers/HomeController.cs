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

            if (!string.IsNullOrEmpty(searchString))
            {
                var filterdUsers = _userManager.Users.Where(
               t => t.UserName.Contains(searchString)).ToList();

                UserSearchResult.UsersResult = filterdUsers;
            }

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
                model.AvatarImgPath = ProccessUploadedFile(model, userInfo, _userManager.GetUserName(User));


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

        [Authorize]
        [HttpGet]
        public IActionResult EditItem(Guid id)
        {


            var achievement = _achievementRepo.Read(id); // We need to check if it belongs to the correct user
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

                _achievementRepo.Create(newAchievement);
                return RedirectToAction("UserResume", new { id = _userManager.GetUserId(User) });
            }

            return View();
        }


        private string ProccessUploadedFile(EditUserInfoViewModel model, UserInformation userInfo, string userName)
        {
            string uniqueFileName = null;
            if (model.AvatarImage != null)
            {
                var maxFileSize = Convert.ToInt32(_config.GetSection("FileUploadSettings")["MaxFileSize"]);
                if (model.AvatarImage.Length > maxFileSize)
                {
                    ModelState.AddModelError("", $"Max file size allowed is {maxFileSize/1000} KB");

                    if (!string.IsNullOrEmpty(userInfo.AvatarImgPath))
                    {
                        return uniqueFileName = userInfo.AvatarImgPath;
                    }
                    else
                    {
                        return null;
                    }
                }

                var fileExtention = Path.GetExtension(model.AvatarImage.FileName);

                if (!fileExtention.Equals(".png"))
                {

                    ModelState.AddModelError("", "Only PNG images are supported");
                    if (!string.IsNullOrEmpty(userInfo.AvatarImgPath))
                    {
                        return uniqueFileName = userInfo.AvatarImgPath;
                    }
                    else
                    {
                        return null;
                    }
                }

                string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images/AvatarImages"); // This will find the wwwroot/images
                //uniqueFileName = Guid.NewGuid().ToString() + "_" + model.AvatarImage.FileName;
                uniqueFileName = "AvatarImg _" + userName + fileExtention;


                var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                //  using (var memoryStream = new MemoryStream())

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    model.AvatarImage.CopyTo(fileStream);
                }
            }
            else
            {
                if (!string.IsNullOrEmpty(userInfo.AvatarImgPath))
                {
                    return uniqueFileName = userInfo.AvatarImgPath;
                }
            }


            return uniqueFileName;
        }
    }
}
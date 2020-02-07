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

namespace MyResume.WebApp.Controllers
{

    public class HomeController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUserInfoRepo _userInfoRepo;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IConfiguration _config;

        public HomeController(UserManager<ApplicationUser> userManager, IUserInfoRepo userInfoRepo, IWebHostEnvironment webHostEnvironment, IConfiguration config)
        {
            _userManager = userManager;
            _userInfoRepo = userInfoRepo;
            _webHostEnvironment = webHostEnvironment;
            _config = config;
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

            var model = new UserResumeViewModel
            {
                UserInfo = _userInfoRepo.Read(id)
            };

            if (_userManager.GetUserId(User) == id)
            {
                model.EnableOwnerOptions = true;
                //userResumeViewModel.UserInfo = Context.getInfo
                //userResumeViewModel.Achivemtns = Context.getallAchivements
                return View(model);
            }

            model.EnableOwnerOptions = false;
            //userResumeViewModel.UserInfo = Context.getInfo
            //userResumeViewModel.Achivemtns = Context.getallAchivements

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

        private string ProccessUploadedFile(EditUserInfoViewModel model, UserInformation userInfo, string userName)
        {
            string uniqueFileName = null;
            if (model.AvatarImage != null)
            {
                var maxFileSize = Convert.ToInt32(_config.GetSection("FileUploadSettings")["MaxFileSize"]);
                if (model.AvatarImage.Length > maxFileSize)
                {
                    ModelState.AddModelError("", $"Max file size allowed is {maxFileSize}");

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

                string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images"); // This will find the wwwroot/images
                //uniqueFileName = Guid.NewGuid().ToString() + "_" + model.AvatarImage.FileName;
                uniqueFileName = "AvatarImg _" + userName + fileExtention;


                var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                //  using (var memoryStream = new MemoryStream())

                using (var fileStream = new FileStream(filePath, FileMode.Create)) // this line makes sure the Filestream is done doing what it needs to do before copying
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
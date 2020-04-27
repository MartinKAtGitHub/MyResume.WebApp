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
        private readonly IExperienceRepo _experienceRepo;
        private readonly ISkillRepo _skillRepo;

        public HomeController(UserManager<ApplicationUser> userManager, IUserInfoRepo userInfoRepo, IWebHostEnvironment webHostEnvironment,
            IConfiguration config, IAchievementRepo achievementRepo, IExperienceRepo experienceRepo, ISkillRepo skillRepo)
        {
            _userManager = userManager;
            _userInfoRepo = userInfoRepo;
            _webHostEnvironment = webHostEnvironment;
            _config = config;
            _achievementRepo = achievementRepo;
            _experienceRepo = experienceRepo;
            _skillRepo = skillRepo;
        }

        public IActionResult Index(string searchString)
        {
            var UserSearchResult = new UserSearchViewModel
            {
                UsersResult = _userInfoRepo.Search(searchString).ToList(),
                DefaultAvatarImg = _config.GetValue<string>("FileUploadSettings:DefaultAvatarImgFilePath")

            };

            return View(UserSearchResult);
        }

        public IActionResult UserResume(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                ViewBag.ErrorTitle = "Can't find Resume ";
                ViewBag.ErrorMessage = "No user is selected";
                return View("Error");
            }

            var userID = _userManager.GetUserId(User);
            var userInfo = _userInfoRepo.Read(id);
            var allUserItems = _achievementRepo.ReadAll(userInfo.UserInformationId);

            var model = new UserResumeViewModel
            {
                AppUserId = userID,
                UserInfo = userInfo,
                Achievements = allUserItems,
                NewSkillViewModel = new SkillViewModel()

            };

            if (userID == id)
            {
                model.EnableOwnerOptions = true;
            }
            else
            {
                model.EnableOwnerOptions = false;
            }

            model.DefaultAvatarImage = _config.GetValue<string>("FileUploadSettings:DefaultAvatarImgFilePath");
            model.DefaultGalleryImage = _config.GetValue<string>("FileUploadSettings:DefaultGalleryImgFilePath");

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
                UserName = _userManager.GetUserName(User),
                FirstName = userInfo.FirstName,
                MiddleName = userInfo.MiddelName,
                LastName = userInfo.LasttName,
                AvatarImgPath = userInfo.AvatarImgPath,
                Summary = userInfo.Summary,
                //MainText = userInfo.MainText,
                AvailableForContact = userInfo.AvailableForContact
            };
            model.DefaultAvatarPath = _config.GetValue<string>("FileUploadSettings:DefaultAvatarImgFilePath");
            return View(model);
        }

        [HttpPost]
        [Authorize]
        public IActionResult EditUserInfo(EditUserInfoViewModel model)
        {

            var userInfo = _userInfoRepo.Read(_userManager.GetUserId(User));
            model.DefaultAvatarPath = _config.GetValue<string>("FileUploadSettings:DefaultAvatarImgFilePath");

            if (ModelState.IsValid)
            {
                var result = FileProcessing.UploadAvatarPng(model.AvatarImage, _userManager.GetUserName(User), this, _config, _webHostEnvironment);

                if (result != null)
                {
                    model.AvatarImgPath = result; // Render on page
                    userInfo.AvatarImgPath = result; //Save in DB
                }
                else
                {
                    model.AvatarImgPath = userInfo.AvatarImgPath;
                }

                userInfo.FirstName = model.FirstName;
                userInfo.MiddelName = model.MiddleName;
                userInfo.LasttName = model.LastName;

                userInfo.Summary = model.Summary;
                //userInfo.MainText = model.MainText;
                userInfo.AvailableForContact = model.AvailableForContact;

                _userInfoRepo.Update(userInfo);
                return View(model);
            }


            model = new EditUserInfoViewModel
            {
                AvatarImgPath = userInfo.AvatarImgPath,
                Summary = userInfo.Summary,
                //MainText = userInfo.MainText,
                AvailableForContact = userInfo.AvailableForContact
            };
            return View(model);
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
                ViewBag.ErrorMessage = "Can't find the item you are looking for";
                return View("PageNotFound");
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

                ImageSrcPaths = new List<string>(),
                Title = item.Title,
                Summary = item.Summary,
                MainText = item.MainText,
                OrderPosMaxLimit = _config.GetValue<int>("ItemSettings:MaxLimit"),
                OrderPosition = item.OrderPosition,
                EnableComments = item.EnableComments,
                EnableRating = item.EnableRating,

            };

            foreach (var filePathContainer in item.ItemGalleryImageFilePaths)
            {
                model.ImageSrcPaths.Add(filePathContainer.GalleryImageFilePath);
            }

            return View(model);
        }

        [HttpPost]
        [Authorize]
        public IActionResult EditItem(Guid id, AchievementViewModel model)
        {


            var item = _achievementRepo.Read(id);
            var userInfo = _userInfoRepo.Read(_userManager.GetUserId(User));


            // TODO research better alternatives for handling the errors -----

            if (item == null)
            {
                Response.StatusCode = 404;
                ViewBag.ErrorMessage = "Can't find the item you are looking for";
                return View("PageNotFound");
            }

            if (item.UserInformationId != userInfo.UserInformationId)
            {
                Response.StatusCode = 403;
                ViewBag.ErrorTitle = "Wrong user";
                ViewBag.ErrorMessage = "Please login with the correct user to edit this item";
                return View("Error");
            }

            // ----------------------------------------------



            if (ModelState.IsValid)
            {

                model.ImageSrcPaths = new List<string>();

                item.Title = model.Title;
                item.Summary = model.Summary;
                item.MainText = model.MainText;
                item.OrderPosition = model.OrderPosition;
                item.EnableComments = model.EnableComments;
                item.EnableRating = model.EnableRating;

                var files = new List<IFormFile>() {
                    model.Thumbnail,
                    model.GallaryImage_1,
                    model.GallaryImage_2,
                    model.GallaryImage_3,
                    model.GallaryImage_4,
                    model.GallaryImage_5
                };

                var filePaths = FileProcessing.UploadItemGalleryPngs(files, userInfo.ApplicationUser.UserName, item.AchievementId, this, _config, _webHostEnvironment);
                var maxImageLimit = Convert.ToInt32(_config.GetSection("FileUploadSettings")["MaxImageStorageLimit"]);

                for (int i = 0; i < maxImageLimit; i++)
                {
                    if (filePaths[i] == null)
                    {
                        continue;
                    }
                    item.ItemGalleryImageFilePaths[i].GalleryImageFilePath = filePaths[i];
                }

                foreach (var filePathContainer in item.ItemGalleryImageFilePaths)
                {
                    model.ImageSrcPaths.Add(filePathContainer.GalleryImageFilePath); // for img src display 
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
            // If max limit is reached ----------------------
            var userInfo = _userInfoRepo.Read(_userManager.GetUserId(User));
            var maxLimit = _config.GetValue<int>("ItemSettings:MaxLimit");

            if (userInfo.AchievementCount >= maxLimit)
            {
                ViewBag.ErrorTitle = "Item limit reached!";
                ViewBag.ErrorMessage = $"The limit per user is {maxLimit}. Please delete any outdated items";
                return View("Error");
            }
            // -----------------------------------------------

            var model = new AchievementViewModel()
            {
                OrderPosMaxLimit = maxLimit,
                OrderPosition = userInfo.AchievementCount++
            };


            return View(model);
        }

        [Authorize]
        [HttpPost]
        public IActionResult CreateItem(AchievementViewModel model)
        {
            // If max limit is reached ----------------------
            var userInfo = _userInfoRepo.Read(_userManager.GetUserId(User));
            var maxLimit = _config.GetValue<int>("ItemSettings:MaxLimit");

            if (userInfo.AchievementCount >= maxLimit)
            {
                ViewBag.ErrorTitle = "Item limit reached!";
                ViewBag.ErrorMessage = $"The limit per user is {maxLimit}. Please delete any outdated items";
                return View("Error");
            }
            // -----------------------------------------------

            if (ModelState.IsValid)
            {
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

                var files = new List<IFormFile>() {
                    model.Thumbnail,
                    model.GallaryImage_1,
                    model.GallaryImage_2,
                    model.GallaryImage_3,
                    model.GallaryImage_4,
                    model.GallaryImage_5
                };

                var filePaths = FileProcessing.UploadItemGalleryPngs(files, userInfo.ApplicationUser.UserName, newItemId, this, _config, _webHostEnvironment);

                var maxImageLimit = Convert.ToInt32(_config.GetSection("FileUploadSettings")["MaxImageStorageLimit"]);

                // Allocate space in the DB for this item.
                for (int i = 0; i < maxImageLimit; i++)
                {
                    newItem.ItemGalleryImageFilePaths.Add(new ItemGalleryImageFilePath
                    {
                        Id = Guid.NewGuid().ToString(), //Id = $"{userInfo.ApplicationUser.UserName}_{model.Title}_Gallery_{i}"
                        GalleryImageFilePath = filePaths[i] ?? null,
                        GalleryIndex = i
                    });
                }

                model.OrderPosition = userInfo.AchievementCount++;

                _userInfoRepo.Update(userInfo);
                _achievementRepo.Create(newItem);

                return RedirectToAction("UserResume", new { id = _userManager.GetUserId(User) });
            }

            return View(model);
        }

        [HttpPost]
        [Authorize]
        public IActionResult DeleteItem(Guid id)
        {
            var item = _achievementRepo.Read(id);
            var userId = _userManager.GetUserId(User);
            var userInfo = _userInfoRepo.Read(userId);

            if (item == null)
            {
                ViewBag.ErrorMessage = $"Item with Id = {id} cannot be found";
                return View("PageNotFound");
            }

            FileProcessing.DeleteAllGalleryImages(_userManager.GetUserName(User), item, _config, _webHostEnvironment);

            userInfo.AchievementCount--;

            _userInfoRepo.Update(userInfo);
            _achievementRepo.Delete(item);
            return RedirectToAction("UserResume", new { id = userId });
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


            var model = new AchievementViewModel()
            {
                //userId =  item.UserInformationId.ToString(),
                ImageSrcPaths = new List<string>(),
                Title = item.Title,
                Summary = item.Summary,
                MainText = item.MainText,
                OrderPosMaxLimit = _config.GetValue<int>("ItemSettings:MaxLimit"),
                OrderPosition = item.OrderPosition,
                EnableComments = item.EnableComments,
                EnableRating = item.EnableRating,

            };

            foreach (var filePathContainer in item.ItemGalleryImageFilePaths)
            {
                if (!string.IsNullOrEmpty(filePathContainer.GalleryImageFilePath))
                {
                    model.ImageSrcPaths.Add(filePathContainer.GalleryImageFilePath);
                }
            }

            return View(model);
        }


        [Authorize]
        [HttpPost]
        public IActionResult RemoveAvatarImg()
        {
            var userInfo = _userInfoRepo.Read(_userManager.GetUserId(User));

            if (string.IsNullOrEmpty(userInfo.AvatarImgPath)) // We don't want to do anything if we dont have an image to remove 
            {
                ModelState.AddModelError("","No Image to Remove");
                return RedirectToAction("EditUserInfo");
            }


            FileProcessing.DeleteAvatarImage(userInfo.ApplicationUser.UserName, _config, _webHostEnvironment);
            userInfo.AvatarImgPath = null;

            _userInfoRepo.Update(userInfo);
            return RedirectToAction("EditUserInfo");
        }

        [Authorize]
        [HttpPost]
        public IActionResult RemoveGalleryImg(Guid id, int imageIndex)
        {
            var userInfo = _userInfoRepo.Read(_userManager.GetUserId(User));
            var item = _achievementRepo.Read(id);

            if (string.IsNullOrEmpty(item.ItemGalleryImageFilePaths[imageIndex].GalleryImageFilePath)) // We don't want to do anything if we dont have an image to remove 
            {
                ModelState.AddModelError("", "Can't find an image to remove");
                return RedirectToAction("EditItem", new { id });
            }

            FileProcessing.DeleteGalleryImage(userInfo.ApplicationUser.UserName, id, imageIndex, _config, _webHostEnvironment);
            item.ItemGalleryImageFilePaths[imageIndex].GalleryImageFilePath = null;

            _achievementRepo.Update(item);
            return RedirectToAction("EditItem", new { id });
        }

        // MODAL Create EXP OutDated
        //[HttpPost]
        //[Authorize]
        //public UserResumeViewModel CreateNewExperienceGroup(/* string id,*/ UserResumeViewModel model) // TODO CreateNewExperienceGroup can be removed
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var userID = _userManager.GetUserId(User);
        //        var id = model.NewExpGrp.UserId;

        //        if (id != userID) // we pass a ID with the form, if this dose not match with the user editing it we kick them out
        //        {
        //            Response.StatusCode = 403; // This is sendt to the AJAX and will cause the ERROR CallbackFunc to run
        //            ViewBag.ErrorTitle = "Wrong user";
        //            ViewBag.ErrorMessage = "Please login with the correct user to create this experience section";

        //            return model;
        //            //return ("Error");
        //        }

        //        var expCount = _experienceRepo.GetExperienceCount(_userInfoRepo.Read(userID).UserInformationId);
        //        var expMaxLimit = _config.GetValue<int>("ExperienceDBLimits:ExperienceSectionsMaxLimit");
        //        if (expCount >= expMaxLimit)
        //        {
        //            Response.StatusCode = 400;
        //            ModelState.AddModelError("", $"You have exceeded the maximum({expMaxLimit}) amount of experience sections you can make.");
        //            return model;
        //        }

        //        var expPointCount = model.NewExpGrp.ExpPoints.Count;
        //        var expPointMaxLimit = _config.GetValue<int>("ExperienceDBLimits:ExperienceHighLightMaxLimit");

        //        if (expPointCount >= expPointMaxLimit)
        //        {
        //            Response.StatusCode = 400;
        //            ModelState.AddModelError("", $"You have exceeded the maximum({expMaxLimit}) amount of experience highligh sections you can make.");
        //            return model;
        //        }

        //        var exp = new Experience()
        //        {
        //            Id = Guid.NewGuid().ToString(),
        //            Title = model.NewExpGrp.Title,
        //            UserInformationId = _userInfoRepo.Read(userID).UserInformationId,
        //            ExperiencePoints = new List<ExperiencePoint>()
        //        };


        //        foreach (var point in model.NewExpGrp.ExpPoints)
        //        {

        //            var descCount = point.Descriptions.Count;
        //            var descMaxLimit = _config.GetValue<int>("ExperienceDBLimits:ExperienceHighLightDescriptionMaxLimit");

        //            if (descCount >= descMaxLimit)
        //            {
        //                Response.StatusCode = 400;
        //                ModelState.AddModelError("", $"You have exceeded the maximum({descMaxLimit}) amount of experience highlight description in section {point.PointTitle} you can make.");
        //                return model;
        //            }

        //            var newPoint = new ExperiencePoint
        //            {
        //                Id = Guid.NewGuid().ToString(),
        //                Title = point.PointTitle,
        //                Descriptions = new List<ExperiencePointDescription>()
        //                //ExperienceId = auto gen EF core +?
        //            };

        //            for (int i = 0; i < point.Descriptions.Count; i++)
        //            {
        //                var desc = new ExperiencePointDescription()
        //                {
        //                    Id = Guid.NewGuid().ToString(),
        //                    Discription = point.Descriptions[i].Desc,
        //                };

        //                newPoint.Descriptions.Add(desc);
        //            }

        //            exp.ExperiencePoints.Add(newPoint);
        //        }

        //        _experienceRepo.CreateExp(exp);
        //        return model;
        //    }

        //    //var forDeBuggingErrors = ModelState.Select(x => x.Value.Errors)
        //    //               .Where(y => y.Count > 0)
        //    //               .ToList();

        //    Response.StatusCode = 422;
        //    //TODO  Maybe Add error text for when ModelState fails and we throw an error
        //    return model;
        //}


        public IActionResult GetExperienceView()//Ajax call
        {
            var activeUserId = _userManager.GetUserId(User);

            return ViewComponent("ExperienceEditDisplay", new { userInfoId = _userInfoRepo.Read(activeUserId).UserInformationId });
        }

        public IActionResult GetSkillsContainerEditing() // this wont get Model state or anything els
        {
            var appUserId = _userManager.GetUserId(User);
            return ViewComponent("SkillsContainerEditing", new { appUserId = appUserId });
        }

        public IActionResult AddEXP()
        {
            var activeUserId = _userManager.GetUserId(User);

            var expCount = _experienceRepo.GetExperienceCount(_userInfoRepo.Read(activeUserId).UserInformationId);
            var expMaxLimit = _config.GetValue<int>("ExperienceDBLimits:ExperienceSectionsMaxLimit");
            if (expCount >= expMaxLimit)
            {
                ModelState.AddModelError("", $"You have exceeded the maximum({expMaxLimit}) amount of experience sections you can make.");
                return ViewComponent("ExperienceEditDisplay", new { userInfoId = _userInfoRepo.Read(activeUserId).UserInformationId });
            }

            var exp = new Experience()
            {
                Id = Guid.NewGuid().ToString(),
                Index = expCount,
                Title = "",
                UserInformationId = _userInfoRepo.Read(activeUserId).UserInformationId,
                ExperiencePoints = new List<ExperiencePoint>()
            };

            var newExpPoint = new ExperiencePoint
            {
                Id = Guid.NewGuid().ToString(),
                Index = 0,
                Title = ""
            };
            newExpPoint.Descriptions = new List<ExperiencePointDescription>();
            newExpPoint.Descriptions.Add(new ExperiencePointDescription
            {
                Id = Guid.NewGuid().ToString(),
                Index = 0,
                Discription = ""
            });

            exp.ExperiencePoints.Add(newExpPoint);
            _experienceRepo.CreateExp(exp);
            return ViewComponent("ExperienceEditDisplay", new { userInfoId = _userInfoRepo.Read(activeUserId).UserInformationId });
        }

        public IActionResult AddpointFieldToExperienceView(string expGrpId)
        {
            var activeUserId = _userManager.GetUserId(User);
            var userInfoId = _userInfoRepo.Read(activeUserId).UserInformationId;
            if (!ModelState.IsValid)
            {
                return ViewComponent("ExperienceEditDisplay", new { userInfoId = userInfoId });
            }

            var exp = _experienceRepo.Read(expGrpId);

            if (exp.UserInformationId != userInfoId)
            {
                Response.StatusCode = 403;
                ViewBag.ErrorTitle = "Wrong user";
                ViewBag.ErrorMessage = "Unauthorized edit attempt was made";
                return View("Error");
            }

            var expPointCount = exp.ExperiencePoints.Count;
            var maxLimit = _config.GetValue<int>("ExperienceDBLimits:ExperienceHighLightMaxLimit");
            if (expPointCount >= maxLimit)
            {
                ModelState.AddModelError("", $"You have exceeded the maximum({maxLimit}) amount of experience point sections you can make.");
                return ViewComponent("ExperienceEditDisplay", new { userInfoId = userInfoId });
            }

            var newExpPoint = new ExperiencePoint
            {
                Id = Guid.NewGuid().ToString(),
                Index = expPointCount,
                Title = ""
            };

            newExpPoint.Descriptions = new List<ExperiencePointDescription>();
            newExpPoint.Descriptions.Add(new ExperiencePointDescription
            {
                Id = Guid.NewGuid().ToString(),
                Index = 0,
                Discription = ""
            });

            exp.ExperiencePoints.Add(newExpPoint);
            _experienceRepo.Update(exp);

            return ViewComponent("ExperienceEditDisplay", new { userInfoId = userInfoId });
        }

        public IActionResult AddDescFieldToExperienceView(string expGrpId, string pointId)
        {
            var activeUserId = _userManager.GetUserId(User);
            var userInfoId = _userInfoRepo.Read(activeUserId).UserInformationId;
         
            if (!ModelState.IsValid)
            {
                return ViewComponent("ExperienceEditDisplay", new { userInfoId = userInfoId });
            }

            var exp = _experienceRepo.Read(expGrpId);

            if (exp.UserInformationId != userInfoId)
            {
                Response.StatusCode = 403;
                ViewBag.ErrorTitle = "Wrong user";
                ViewBag.ErrorMessage = "Unauthorized edit attempt was made";
                return View("Error");
            }


            var expPoint = exp.ExperiencePoints.FirstOrDefault(x => x.Id == pointId);
            if (expPoint != null)
            {
                var expPointDescCount = expPoint.Descriptions.Count;
                var maxLimit = _config.GetValue<int>("ExperienceDBLimits:ExperienceHighLightDescriptionMaxLimit");
                if (expPointDescCount >= maxLimit)
                {
                    ModelState.AddModelError("", $"You have exceeded the maximum({maxLimit}) amount of experience highlight descriptions sections you can make.");
                    return ViewComponent("ExperienceEditDisplay", new { userInfoId = userInfoId });
                }
                expPoint.Descriptions.Add(new ExperiencePointDescription
                {
                    Id = Guid.NewGuid().ToString(),
                    Index = expPointDescCount,
                    Discription = ""                });
            }
            else
            {
                Response.StatusCode = 404;
                ViewBag.ErrorTitle = "Can't find the experience highlight to edit";
                ViewBag.ErrorMessage = "Please contact the administration";
                return View("Error");
            }


            _experienceRepo.Update(exp);

            return ViewComponent("ExperienceEditDisplay", new { userInfoId = userInfoId });
        }

        [HttpPost]
        [Authorize]
        public IActionResult UpdateExperiences(List<ExpViewModel> model)
        {

            var userInfoId = _userInfoRepo.Read(_userManager.GetUserId(User)).UserInformationId;

            //var forDeBuggingErrors = ModelState.Select(x => x.Value.Errors)
            //               .Where(y => y.Count > 0)
            //               .ToList();

            if (!ModelState.IsValid) // TODO UpdateExperiences() validation will fire off before you delete the field and block your attempt which might be an issue.
            {
                return ViewComponent("ExperienceEditDisplay", new { userInfoId = userInfoId });
            }

            var updatedExpGrps = new List<Experience>();

            foreach (var modelExpGrp in model)
            {
                Experience expGrp = _experienceRepo.Read(modelExpGrp.Id); // If the user pampers with the id, the returned value will not belong to th active user

                if (expGrp == null)
                {
                    Response.StatusCode = 404;
                    ViewBag.ErrorMessage = "Can't find the experience section you are looking for, pleas contact the administration";
                    return View("PageNotFound");
                }

                if (expGrp.UserInformation.UserInformationId != userInfoId)
                {
                    Response.StatusCode = 403;
                    ViewBag.ErrorTitle = "Wrong user";
                    ViewBag.ErrorMessage = "Unauthorized edit attempt was made";
                    return View("Error");

                }

                if (modelExpGrp.MarkForDeletion)
                {
                    _experienceRepo.DeleteExp(expGrp);
                    continue;
                }

                expGrp.Title = modelExpGrp.Title;

                var pointDeletionList = new List<ExperiencePoint>();
                var descDeletionList = new List<ExperiencePointDescription>();

                int count = 0;

                foreach (var point in expGrp.ExperiencePoints)
                {
                    //if (modelExpGrp.ExperiencePoints[count].MarkForDeletion)
                    if (modelExpGrp.ExpPoints[count].MarkForDeletion)
                    {
                        pointDeletionList.Add(point);
                        count++;
                        continue;
                    }

                    // point.Title = modelExpGrp.ExperiencePoints[count].Title;
                    point.Title = modelExpGrp.ExpPoints[count].PointTitle;

                    int descCount = 0;


                    foreach (var desc in point.Descriptions)
                    {

                        // if (modelExpGrp.ExperiencePoints[count].Descriptions[descCount].MarkForDeletion)
                        if (modelExpGrp.ExpPoints[count].Descriptions[descCount].MarkForDeletion)
                        {
                            descDeletionList.Add(desc);
                            descCount++;
                            continue;
                        }

                        //desc.Discription = modelExpGrp.ExperiencePoints[count].Descriptions[descCount].Discription;
                        desc.Discription = modelExpGrp.ExpPoints[count].Descriptions[descCount].Desc;
                        descCount++;
                    }
                    count++;
                }

                foreach (var point in pointDeletionList)
                {
                    _experienceRepo.DeleteExpPoint(point);
                }

                foreach (var desc in descDeletionList)
                {
                    _experienceRepo.DeleteExpPointDesc(desc);
                }

                updatedExpGrps.Add(expGrp);
            }



            _experienceRepo.UpdateAll(updatedExpGrps);
            return ViewComponent("ExperienceEditDisplay", new { userInfoId = userInfoId });
        }

        [HttpPost]
        [Authorize]
        public IActionResult CreatNewSkill(UserResumeViewModel model)
        {
            var appUserId = _userManager.GetUserId(User);
            var skilCount = _skillRepo.ReadAll(appUserId).Count();
            var maxLimit = _config.GetValue<int>("SkillDBLimits:MaxSkillLimit");

            if (skilCount >= maxLimit)
            {
               // Response.StatusCode = 400; // THIS WILL STOP FORM AJAX TO UPDATE THE DIV TO DISPLAY MODEL ERRORS
                ModelState.AddModelError("", $"You have reached the max limit of proficiencies({maxLimit}), please delete unnecessary proficiencies ");
                return ViewComponent("SkillsContainerEditing", new { appUserId = appUserId });
            }

            if (!ModelState.IsValid)
            {
              //  Response.StatusCode = 400; //THIS WILL STOP FORM AJAX TO UPDATE THE DIV TO DISPLAY MODEL ERRORS
                return ViewComponent("SkillsContainerEditing", new { appUserId = appUserId });
            }

            var newSkill = new Skill()
            {
                Id = Guid.NewGuid(),
                ApplicationUserId = appUserId,
                TagName = model.NewSkillViewModel.TagName,
                Level = model.NewSkillViewModel.Level
            };

            _skillRepo.Create(newSkill);

            return ViewComponent("SkillsContainerEditing", new { appUserId = appUserId });
        }

        [HttpPost]
        [Authorize]
        public IActionResult EditSkill(Skill model)
        {
            var appUserId = _userManager.GetUserId(User);
            var updatedSkill = _skillRepo.Read(model.Id);

            if (updatedSkill == null)
            {
                Response.StatusCode = 404;
                ViewBag.ErrorTitle = "Cant find proficiency";
                ViewBag.ErrorMessage = "Pleas refresh and try again";
                return View("Error");
            }

            if (updatedSkill.ApplicationUserId != appUserId)
            {
                Response.StatusCode = 403;
                ViewBag.ErrorTitle = "Wrong user";
                ViewBag.ErrorMessage = "Unauthorized edit attempt was made";
                return View("Error");
            }

            if (!ModelState.IsValid)
            {
                return ViewComponent("SkillsContainerEditing", new { appUserId = appUserId });
                //   return ViewComponent("SkillEditDisplay", new { updatedSkill });
            }


            updatedSkill.Level = model.Level;
            updatedSkill.TagName = model.TagName;

            var skill = _skillRepo.Update(updatedSkill);

            return ViewComponent("SkillsContainerEditing", new { appUserId = appUserId });
            // return ViewComponent("SkillEditDisplay", new { skill });
        }

        [HttpPost]
        [Authorize]
        public IActionResult DeleteSkill(string id)
        {

            var appUserId = _userManager.GetUserId(User);
            var deleteSkill = _skillRepo.Read(Guid.Parse(id));

            if (deleteSkill == null)
            {
                Response.StatusCode = 404;
                ViewBag.ErrorTitle = "Cant find proficiency";
                ViewBag.ErrorMessage = "Pleas refresh and try again";
                return View("Error");
            }

            if (deleteSkill.ApplicationUserId != appUserId)
            {
                Response.StatusCode = 403;
                ViewBag.ErrorTitle = "Wrong user";
                ViewBag.ErrorMessage = "Unauthorized edit attempt was made";
                return View("Error");
            }

            var forDeBuggingErrors = ModelState.Select(x => x.Value.Errors)
                           .Where(y => y.Count > 0)
                           .ToList();

            _skillRepo.Delete(deleteSkill);
            return ViewComponent("SkillsContainerEditing", new { appUserId = appUserId });
        }


    }
}
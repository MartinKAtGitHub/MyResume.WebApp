using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MyResume.WebApp.Controllers
{
    public class ErrorController : Controller
    {
      
        [Route("Error/{code}")]
        public IActionResult ErrorCodeHandler(int code)
        {

            var statusCodeResult = HttpContext.Features.Get<IStatusCodeReExecuteFeature>();

            switch (code)
            {
                case 404:
                    ViewBag.ErrorMessage = $"{statusCodeResult.OriginalPath}/{statusCodeResult.OriginalQueryString} Dose not exist";
                    break;
                
            }
            return View("PageNotFound");
        }

        [Route("Error")]
        [AllowAnonymous]
        public IActionResult Error()
        {
            var exeptionDetailes = HttpContext.Features.Get<IExceptionHandlerPathFeature>();

          //  logger.LogError($"The path {exeptionDetailes.Path} threw an exception {exeptionDetailes.Error} ");

            ViewBag.ExceptionPath = "Path = " + exeptionDetailes.Path;
            ViewBag.ExceptionMessage = "Error Message = " + exeptionDetailes.Error.Message;
            //ViewBag.Stacktrace = exeptionDetailes.Error.StackTrace;
            return View("Error");
        }
    }
}

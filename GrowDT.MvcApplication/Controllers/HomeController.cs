using System;
using System.Web.Mvc;
using GrowDT.MvcHelper.Authorization;
using GrowDT.MvcHelper.Filters;
using GrowDT.MvcHelper.Utility;
using GrowDT.Services.Interfaces;

namespace GrowDT.MvcApplication.Controllers
{
    public class HomeController : BaseController
    {
        private readonly IUserService _userService;
        public HomeController(IUserService userService)
        {
            _userService = userService;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            //throw new Exception();
            ViewBag.Message = "Your application description page.";

            return View();
        }

        [HttpPost]
        public ActionResult About(FormCollection form)
        {
            var uploadResut = Request.UploadExcelAndSaveData("test", s => "");
            ViewBag.Message = uploadResut.ToString();
            return View();
        }

        public ActionResult AboutDownload()
        {
            string path = @"App_Data\ExcelUploaded\Im_test_20161202104717.xlsx";
            path = FileHelper.GetFullFilePath(path);
            return DownloadFile(path);
        }

        public ActionResult Contact(UserSession user)
        {
            ViewBag.Message = "Your contact page."+user.Username;
            _userService.GetUsers();
        //    _userService.AddUser(new Services.Messaging.UserService.AddUserRequest {UserCode = "p0020614", UserName = "Dennis"});

            return View();
        }

        [HttpPost]
        [RoleAuthorize(UserRole.SuperAdmin)]
        public JsonResult JsonTest()
        {
            throw new ArgumentNullException();
            return Json(new {Success = true, Message = "ok."});
        }
    }
}
﻿using System.Web.Mvc;
using GrowDT.Application;
using GrowDT.MvcApplication.ViewModels;
using GrowDT.MvcHelper.Authorization;
using GrowDT.Services.Interfaces;
using GrowDT.Services.Messaging.IUserAuthorityService;

namespace GrowDT.MvcApplication.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAuthProvider _authProvider;
        private readonly IUserAuthorityService _userAuthorityService;

        public AccountController(IAuthProvider authProvider, IUserAuthorityService userAuthorityService)
        {
            _authProvider = authProvider;
            _userAuthorityService = userAuthorityService;
        }

        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            Response.Cache.SetNoStore();

            if (_authProvider.IsAuthenticated())
            {
                return RedirectToLocal(returnUrl);
            }
            ViewBag.ReturnUrl = returnUrl;

            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult Login(LoginViewModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var checkRequest = new CheckLoginRequest {Username = model.Username, Password = model.Password};
                var checkResponse = _userAuthorityService.CheckLogin(checkRequest);
                if (checkResponse.UserValid)
                {
                    _authProvider.Authenticate(model.Username);
                    Session[AppConfig.UserSessionKey] = new UserSession(model.Username);//TODO:
                    return RedirectToLocal(returnUrl);
                }
            }

            ModelState.AddModelError("", model.MsgNamePwdWrong);
            return View(model);
        }

        public ActionResult Logout()
        {
            Response.Cache.SetNoStore();
            _authProvider.SignOut();
            return Redirect(_authProvider.LoginUrl);
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            return Url.IsLocalUrl(returnUrl) ? Redirect(returnUrl) : RedirectToWebsiteDefaultPage();
        }

        private ActionResult RedirectToWebsiteDefaultPage()
        {
            return RedirectToAction("Index", "Home");
        }

    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DeskHelp.Services;
using DeskHelp.Models;

namespace DeskHelp.Controllers
{
    public class AccountController : Controller
    {
        private UserService userService = new UserService();

        private UserModel GetCurrentUser()
        {
            var UserLogged = Session["User"] as UserModel;
            return UserLogged;
        }

        private void logInUser(UserModel user)
        {
            Session["User"] = user;
        }

        [HttpGet]
        public ActionResult LogoutUser()
        {
            Session.Clear();
            Session.Abandon();

            return RedirectToAction("Login", "Account");
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel model)
        {
            try
            {
                bool validUser = userService.ValidateUser(model.Email, model.Password);
                UserModel user = userService.GetUserByEmail(model.Email);

                if (validUser && user != null && user.State)
                {
                    logInUser(user);
                    return RedirectToAction("Index", "Home");
                }

                ViewBag.Error = "Correo o contraseña incorrectos.";

                return View(model);
            }
            catch
            {
                return View();
            }
        }
    }
}
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

                if (validUser)
                {
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
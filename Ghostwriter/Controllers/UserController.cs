using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Ghostwriter.Controllers{
    
    public class UserController : Controller
    {
        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]        
        public ActionResult Logon(string Email, string Password)
        {
            string email = Email;
            string password = Password;

            if (Email == "admin@ghostwriter.com" && Password == "admin")
            {
                FormsAuthentication.SetAuthCookie(Email, false);
                return RedirectToAction("Backup", "User");
            }
            else
            {
                return RedirectToAction("Login", "User");
            }
        }

        [Authorize]
        public ActionResult Backup()
        {
            return View();
        }

        [HttpPost]
        public ActionResult IncrementalBackUp()
        {
            return View();
        }

        [HttpPost]
        public ActionResult FullBackUp()
        {
            return View();
        }        
    }
}
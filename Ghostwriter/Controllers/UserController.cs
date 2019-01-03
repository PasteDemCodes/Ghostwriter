using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Ghostwriter.Models;

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
            string BackUpQuery(string databaseName, string fileAddress)
            {
                return (@"BACKUP DATABASE " + databaseName + @" TO DISK = '" + fileAddress + "' WITH FORMAT;");
            }

            string message = "";

            using (pubsEntities _context = new pubsEntities())
            {
                string command = "select db_name()";
                string databaseName = _context.Database.SqlQuery(typeof(string), command).ToListAsync().Result.FirstOrDefault().ToString();
                string backUpQuery = BackUpQuery(databaseName, "C:/backuphere/backup.bat");
                var result = _context.Database.SqlQuery<List<string>>(backUpQuery).ToList();
                if (result.Count() > 0)
                {
                    result.ForEach(x =>
                    {
                        message += x.ToString();
                    });
                }
            }

            return RedirectToAction("Backup", "User");
        }
       

        [HttpPost]
        public ActionResult FullBackUp()
        {
            string BackUpQuery(string databaseName, string fileAddress)
            {
                return (@"BACKUP DATABASE " + databaseName + @" TO DISK = '" + fileAddress + "' WITH FORMAT;");
            }

            string message = "";

            using (pubsEntities _context = new pubsEntities())
            {
                string command = "select db_name()";
                string databaseName = _context.Database.SqlQuery(typeof(string), command).ToListAsync().Result.FirstOrDefault().ToString();
                string backUpQuery = BackUpQuery(databaseName, "C:/backuphere/backup.bat");
                var result = _context.Database.SqlQuery<List<string>>(backUpQuery).ToList();
                if (result.Count() > 0)
                {
                    result.ForEach(x =>
                    {
                        message += x.ToString();
                    });
                }
            }

            return RedirectToAction("Backup", "User");
        }

        [HttpPost]
        public ActionResult Restore()
        {
            string RestoreQuery(string databaseName, string fileAddress)
            {
                string command = @"use [master]
                        ALTER DATABASE  " + databaseName + @"
                        SET SINGLE_USER
                        WITH ROLLBACK IMMEDIATE
                        RESTORE DATABASE " + databaseName + @"
                        FROM  DISK = N'" + fileAddress + "'";

                return command;
            }

            string message = "";

            using (pubsEntities _context = new pubsEntities())
            {
                string command = "select db_name()";
                string databaseName = _context.Database.SqlQuery(typeof(string), command).ToListAsync().Result.FirstOrDefault().ToString();
                string restoreQuery = RestoreQuery(databaseName, "C:/backuphere/backup.bat");
                var result = _context.Database.SqlQuery<List<string>>(restoreQuery).ToList();
                if (result.Count() > 0)
                {
                    result.ForEach(x =>
                    {
                        message += x.ToString();
                    });
                }
            }

            return RedirectToAction("Backup", "User");
        }
    }
}
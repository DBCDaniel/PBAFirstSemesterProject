using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using InterfaceMVC.Models;

namespace InterfaceMVC.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        public ActionResult Index()
        {
            using (OurDbContext db = new OurDbContext())
            {
                if (db.userAccount.Count() == 0)
                {
                    return RedirectToAction("Setup");
                }
                else
                {
                    return RedirectToAction("Login");
                }
            }
        }

        public ActionResult Setup()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Setup(UserAccount account)
        {
            if (ModelState.IsValid)
            {
                using (OurDbContext db = new OurDbContext())
                {
                    account.Clearance = -1;
                    db.userAccount.Add(account);
                    db.SaveChanges();
                }
                ModelState.Clear();
                ViewBag.Message = account.FirstName + " " + account.LastName + " Created.";
            }
            return RedirectToAction("Index");
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(UserAccount user)
        {
            using (OurDbContext db = new OurDbContext())
            {
                var usr = db.userAccount.Where(u => u.Username == user.Username && u.Password == user.Password).FirstOrDefault();
                if (usr != null)
                {
                    Session["UserID"] = usr.UserID.ToString();
                    Session["Username"] = usr.Username.ToString();
                    Session["Clearance"] = usr.Clearance.ToString();
                    return RedirectToAction("Account");
                }
                else
                {
                    ModelState.AddModelError("", "Login Failed");
                }
            }
            return View();
        }

        public ActionResult Account()
        {
            if (Session["UserID"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login");
            }
        }
    }
}
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
                    if (Session["UserID"] != null)
                    {
                        return RedirectToAction("Account");
                    }
                    else
                    {
                        return RedirectToAction("Login");
                    }
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

        public ActionResult AccessDenied()
        {
            return View();
        }

        public ActionResult Users()
        {
            if (Session["UserID"] != null && Session["Clearance"] != null)
            {
                if (Session["Clearance"].ToString() != "-1")
                {
                    using (OurDbContext db = new OurDbContext())
                    {
                        int lvl = 0;
                        int.TryParse(Session["Clearance"].ToString(), out lvl);
                        ClearanceLevel clearance = db.clearanceLevel.Where(x => x.LevelID == lvl).FirstOrDefault();
                        if (clearance.UserAdministration)
                        {
                            return View(db.userAccount.ToList());
                        }
                        else
                        {
                            return RedirectToAction("AccessDenied");
                        }
                    }
                }
                else
                {
                    using (OurDbContext db = new OurDbContext())
                    {
                        return View(db.userAccount.ToList());
                    }
                }
            }
            else
            {
                return RedirectToAction("Login");
            }
        }

        public ActionResult UserRegister()
        {
            return View();
        }

        [HttpPost]
        public ActionResult UserRegister(UserAccount account)
        {
            if (ModelState.IsValid)
            {
                using (OurDbContext db = new OurDbContext())
                {
                    account.Clearance = 0;
                    db.userAccount.Add(account);
                    db.SaveChanges();
                }
                ModelState.Clear();
                ViewBag.Message = account.FirstName + " " + account.LastName + " Created.";
            }
            return View();
        }
        
        public ActionResult DeleteAccount(int id = 0)
        {
            ModelState.Clear();
            ViewBag.Message = "ID# - " + id + " DELETED.";

            return RedirectToAction("Users");
        }
        
        public ActionResult ResetPassword(int id = 0)
        {
            ModelState.Clear();
            ViewBag.Message = "ID# - " + id + " Password RESET.";

            return RedirectToAction("Users");
        }

        public ActionResult ClearanceLevels()
        {
            if (Session["UserID"] != null && Session["Clearance"] != null)
            {
                if (Session["Clearance"].ToString() != "-1")
                {
                    using (OurDbContext db = new OurDbContext())
                    {
                        int lvl = 0;
                        int.TryParse(Session["Clearance"].ToString(), out lvl);
                        ClearanceLevel clearance = db.clearanceLevel.Where(x => x.LevelID == lvl).FirstOrDefault();
                        if (clearance.ClearanceAdminstration)
                        {
                            return View(db.clearanceLevel.ToList());
                        }
                        else
                        {
                            return RedirectToAction("AccessDenied");
                        }
                    }
                }
                else
                {
                    using (OurDbContext db = new OurDbContext())
                    {
                        return View(db.clearanceLevel.ToList());
                    }
                }
            }
            else
            {
                return RedirectToAction("Login");
            }
        }

        public ActionResult EventLog()
        {
            if (Session["UserID"] != null && Session["Clearance"] != null)
            {
                if (Session["Clearance"].ToString() != "-1")
                {
                    using (OurDbContext db = new OurDbContext())
                    {
                        int lvl = 0;
                        int.TryParse(Session["Clearance"].ToString(), out lvl);
                        ClearanceLevel clearance = db.clearanceLevel.Where(x => x.LevelID == lvl).FirstOrDefault();
                        if (clearance.EventLog)
                        {
                            return View(db.eventLog.ToList());
                        }
                        else
                        {
                            return RedirectToAction("AccessDenied");
                        }
                    }
                }
                else
                {
                    using (OurDbContext db = new OurDbContext())
                    {
                        return View(db.eventLog.ToList());
                    }
                }
            }
            else
            {
                return RedirectToAction("Login");
            }
        }

        public ActionResult Logout()
        {
            Session["UserID"] = null;
            Session["Username"] = null;
            Session["Clearance"] = null;
            return RedirectToAction("Login");
        }
    }
}
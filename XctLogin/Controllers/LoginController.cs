using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using XctLogin.Models;

namespace XctLogin.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Authorize(XctLogin.Models.User userModel)
        {
            using (LoginDatabaseEntities db = new LoginDatabaseEntities())
            {
                var userDetails = db.Users.Where(x => x.Username == userModel.Username && x.Password == userModel.Password).FirstOrDefault();
                if (userDetails == null)
                {
                    userModel.LoginErrorMessage = "Incorrect username or password.";
                    return View("Index", userModel);
                }
                else
                {
                    Session["Id"] = userDetails.Id;
                    Session["Name"] = userDetails.Name;
                    Session["Username"] = userDetails.Username;
                    return RedirectToAction("Index", "Home");
                }
            }
            
        }

        public ActionResult LogOut()
        {
            int Id = (int)Session["Id"];
            Session.Abandon();
            return RedirectToAction("Index", "Login");
        }
    }
}
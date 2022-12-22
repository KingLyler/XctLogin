using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Xml;
using XctLogin.Models;

namespace XctLogin.Controllers
{
    public class LoginController : Controller
    {
       // LoginDatabaseEntities db = new LoginDatabaseEntities();
        public ActionResult Index()
        {
            //var data = db.Users.OrderByDescending(x => x.Id).Select(s => s).ToList();
            return View();
        }

        [HttpPost]
        public JsonResult doesUsernameExist(XctLogin.Models.User Username)
        {
            var user = Membership.GetUser(Username);
            return Json(user != null);
        }

        
        [HttpPost]
        public ActionResult Authorize(XctLogin.Models.User userModel)
        {
            using (LoginDatabaseEntities db = new LoginDatabaseEntities())
            {
                
                var userDetails = db.Users.Where(x => x.Username == userModel.Username && x.Password == userModel.Password).FirstOrDefault();
                var username = db.Users.Where(x => x.Username == userModel.Username);
                var password = db.Users.Where(x => x.Password == userModel.Password).FirstOrDefault();
                string loginError = userModel.LoginErrorMessage;
                string noSuchUser = "Username does not exist!";


                // string query = "SELECT COUNT(*) FROM Users WHERE Username = @username";
                // SqlCommand findId = new SqlCommand(query);
                //
                // if (ModelState.IsValid)
                // {
                //  bool idExists = findId > 0;
                try
                {
                     if (userDetails != null )
                    {
                        Session["Id"] = userDetails.Id;
                        Session["Name"] = userDetails.Name;
                        Session["Username"] = userDetails.Username;
                        return RedirectToAction("Index", "Home");

                    }
                     else if (username.Count() != 1)
                     {
                      userModel.UsernameNotFound = noSuchUser;
                      return View("Index", userModel);
                     }
                      else if (userModel.ConfirmPassword == null)
                      {
                      userModel.IncorrectPassword = "Incorrect password";
                              return View("Index", userModel);
                      }
                   else 
                    {
                        userModel.LoginErrorMessage = "Please enter username and password.";
                        return View("Index", userModel);

                    }
                   
                }
                catch (Exception ex)
                {
                    return View(ex);
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
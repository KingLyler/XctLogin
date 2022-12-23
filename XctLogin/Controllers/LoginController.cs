using System;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;
using XctLogin.Models;

namespace XctLogin.Controllers
{
    public class LoginController : Controller
    {
        public string UserNotFoundErrorMessage = "Username does not exist.";
        public string PasswordErrorMessage = "Incorrect password.";

        public ActionResult Index()
        {
            //var data = db.Users.OrderByDescending(x => x.Id).Select(s => s).ToList();
            var viewModel = new LoginViewModel();
            return View("Index", viewModel);
        }

        [HttpPost]
        public JsonResult DoesUsernameExist(User username)
        {
            var user = Membership.GetUser(username);
            return Json(user != null);
        }

        
        [HttpPost]
        public ActionResult Authorize(LoginViewModel loginViewModel)
        {
            using (var db = new LoginDatabaseEntities())
            {
                try
                {
                    //Set errors back to blank
                    loginViewModel.UsernameNotFound = string.Empty;
                    loginViewModel.IncorrectPassword = string.Empty;
                    loginViewModel.LoginErrorMessage = string.Empty;

                    //Set Loading to disable button
                    loginViewModel.Loading = true;

                    //Check for valid user on both inputs
                    var userDetails = db.Users.FirstOrDefault(x => x.Username == loginViewModel.User.Username && x.Password == loginViewModel.User.Password);
                    if (userDetails != null)
                    {
                        Session["Id"] = userDetails.Id;
                        Session["Name"] = userDetails.Name;
                        Session["Username"] = userDetails.Username;
                        return RedirectToAction("Index", "Home");
                    }

                    //If valid user not found, validate only username
                    var userFoundByUsername = db.Users.FirstOrDefault(x => x.Username == loginViewModel.User.Username);
                    if (userFoundByUsername == null)
                    {
                        loginViewModel.UsernameNotFound = UserNotFoundErrorMessage;
                        loginViewModel.Loading = false;
                        return View("Index", loginViewModel);
                    }

                    //If valid user not found, and the username is valid, validate the username and password
                    var userFoundByPasswordAndUsername = db.Users.FirstOrDefault(x => x.Username.Equals(userFoundByUsername.Username) && x.Password == loginViewModel.User.Password);
                    if (userFoundByPasswordAndUsername == null)
                    {
                        loginViewModel.IncorrectPassword = PasswordErrorMessage;
                        loginViewModel.Loading = false;
                        return View("Index", loginViewModel);
                    }

                    // Else give general error
                    loginViewModel.LoginErrorMessage = UserNotFoundErrorMessage;
                    loginViewModel.Loading = false;
                    return View("Index", loginViewModel);
                }

                catch (Exception ex)
                {
                    loginViewModel.Loading = false;
                    loginViewModel.LoginErrorMessage = ex.Message;
                    return View("Index", loginViewModel);
                }
               
            }
            
        }
        [HttpPost]
        [Authorize]
        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Login");
        }
    }
}
using System.Web.Mvc;

namespace XctLogin.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View("Index", "_Layout");
        }
       
     
    }
}
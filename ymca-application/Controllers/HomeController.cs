using System.Web.Mvc;

namespace ymca_application.Controllers
{
    public class HomeController : Controller
    {
        // GET: /Home/Index
        public ActionResult Index()
        {
            return View();
        }

        // GET: /Home/About
        public ActionResult About()
        {
            ViewBag.Message = "About page.";

            return View();
        }

        // GET: /Home/Contact
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        // GET: /Home/Programs
        public ActionResult Programs()
        {
            return View();
        }
    }
}
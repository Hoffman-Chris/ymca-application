using System.Web.Mvc;

namespace ymca_application.Controllers
{
    public class AdministrationController : Controller
    {
        // GET: /Administration/Index
        public ActionResult Index()
        {
            return View();
        }

        // GET: /Administration/Users
        public ActionResult Users()
        {
            return View();
        }

        // GET: /Administration/Programs
        public ActionResult Programs()
        {
            return View();
        }
    }
}
using System.Web.Mvc;

namespace StudentAdministrationSystem.Controllers
{
    public class StudentController : Controller
    {
        // GET
        public ActionResult Index()
        {
            return View();
        }
    }
}
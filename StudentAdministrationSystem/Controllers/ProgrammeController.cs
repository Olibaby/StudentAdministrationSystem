using System.Web.Mvc;
using StudentAdministrationSystem.Models;
using StudentAdministrationSystem.Service.Interface;

namespace StudentAdministrationSystem.Controllers
{
    public class ProgrammeController : Controller
    {
        private IProgrammeService _programmeService;
        public ProgrammeController(IProgrammeService programmeService)
        {
            _programmeService = programmeService;
        }
        // GET
        public ActionResult Index()
        {
            var programmeModel = _programmeService.GetProgrammes();
            return View(programmeModel);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        
        [HttpPost]
        public ActionResult Create(ProgrammeModel programmeModel)
        {
            if (!ModelState.IsValid)
            {
                TempData["Message"] = "Program is not valid";
                return RedirectToAction("Index", programmeModel);
            }
            _programmeService.AddProgramme(programmeModel);
            TempData["Message"] = "Program has been successfully added";
            return RedirectToAction("Index");
        }
    }
}
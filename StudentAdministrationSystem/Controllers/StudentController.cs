using System.Web.Mvc;
using StudentAdministrationSystem.Models;
using StudentAdministrationSystem.Service.Interface;

namespace StudentAdministrationSystem.Controllers
{
    public class StudentController : Controller
    {
        private IStudentService _studentService;
        private IProgrammeService _programmeService;
        
        public StudentController(IStudentService studentService, IProgrammeService programmeService)
        {
            _studentService = studentService;
            _programmeService = programmeService;
        }
        // GET
        public ActionResult Index()
        {
            var studentModel = _studentService.GetStudents();
            return View(studentModel);
        }
        
        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.programmes = new SelectList(_programmeService.GetProgrammes(), "ProgrammeId", "ProgrammeTitle");
            return View();
        }
        
        [HttpPost]
        public ActionResult Create(StudentModel studentModel)
        {
            ViewBag.programmes = new SelectList(_programmeService.GetProgrammes(), "ProgrammeId", "ProgrammeTitle");
            if (!ModelState.IsValid)
            {
                TempData["Message"] = "Student is not valid";
                return RedirectToAction("Index", studentModel);
            }
           
            _studentService.AddStudent(studentModel);
            TempData["Message"] = "Student has been successfully added";
            return RedirectToAction("Index");
        }
    }
}
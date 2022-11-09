using System.Linq;
using System.Web.Mvc;
using StudentAdministrationSystem.Models;
using StudentAdministrationSystem.Service;
using StudentAdministrationSystem.Service.Interface;

namespace StudentAdministrationSystem.Controllers
{
    public class GradeController : Controller
    {
        private IGradeService _gradeService;
        private IAssessmentService _assessmentService;
        private IStudentService _studentService;
        public GradeController(IGradeService gradeService, IAssessmentService assessmentService, IStudentService studentService)
        {
            _gradeService = gradeService;
            _assessmentService = assessmentService;
            _studentService = studentService;
        }
        // GET
        public ActionResult Index()
        {
            var gradeModel = _gradeService.GetGrades();
            return View(gradeModel);
        }
        
        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.assessments = new SelectList(_assessmentService.GetAssessments(), "AssessmentId", "AssessmentTitle");
            ViewBag.students = new SelectList(_studentService.GetStudents(), "StudentId", "StudentName");
            return View();
        }
        
        [HttpPost]
        public ActionResult Create(GradeModel gradeModel)
        {
            ViewBag.assessments = new SelectList(_assessmentService.GetAssessments(), "AssessmentId", "AssessmentTitle");
            ViewBag.students = new SelectList(_studentService.GetStudents(), "StudentId", "StudentName");
            if (!ModelState.IsValid)
            {
                TempData["Message"] = "Grade is not valid";
                return RedirectToAction("Index", gradeModel);
            }

            _gradeService.AddGrade(gradeModel);
            TempData["Message"] = "Grade has been successfully added";
            return RedirectToAction("Index");
        }
        
        [HttpGet]
        public ActionResult Edit(int id)
        {
            ViewBag.assessments = new SelectList(_assessmentService.GetAssessments(), "AssessmentId", "AssessmentTitle");
            ViewBag.students = new SelectList(_studentService.GetStudents(), "StudentId", "StudentName");

            var model = _gradeService.GetGrade(id);
            if (model == null)
            {
                TempData["Message"] = "Grade cannot be found";
                return View("Index");
            }
            return View(model);
        }
        
        [HttpPost]
        public ActionResult Edit(GradeModel gradeModel)
        {
            ViewBag.assessments = new SelectList(_assessmentService.GetAssessments(), "AssessmentId", "AssessmentTitle");
            ViewBag.students = new SelectList(_studentService.GetStudents(), "StudentId", "StudentName");

            if (ModelState.IsValid)
            {
                if (gradeModel == null)
                {
                    TempData["Message"] = "Grade is not found";
                    return View("Index");
                }
                _gradeService.UpdateGrade(gradeModel);
                return RedirectToAction("Index");
            }
            return View(gradeModel);
        }
    }
}
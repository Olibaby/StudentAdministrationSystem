using System;
using System.Linq;
using System.Web.Mvc;
using StudentAdministrationSystem.data.Entities;
using StudentAdministrationSystem.data.Repository.Interface;
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
        public ActionResult ViewModules(string id)
        {
            Console.WriteLine("mod are" + _studentService.GetModuleByStudentId(id).Count());
            var modules = _studentService.GetModuleByStudentId(id);
            ViewBag.studentId = id;
            return View(modules);
        }
        
        [HttpGet]
        public ActionResult Create(string moduleId, string studentId)
        {
            ViewBag.assessments = new SelectList(_assessmentService.GetAssessmentsByModule(moduleId), "AssessmentId", "AssessmentTitle");
            ViewBag.studentId = studentId;
            ViewBag.moduleId = moduleId;
            return View();
        }
        
        [HttpPost]
        public ActionResult Create(GradeModel gradeModel)
        {
            if (!ModelState.IsValid)
            {
                TempData["Message"] = "Grade is not valid";
                Console.WriteLine("Grade is not valid");
                return RedirectToAction("ViewModules", gradeModel);
            }
            // var assessmentExist = _gradeService.GetGrades()
            //     .Any(g => g.AssessmentId == gradeModel.AssessmentId && g.StudentId == gradeModel.StudentId);
            var assessmentExist = _gradeService.GetGradesByStudentAssessmentModule(gradeModel.StudentId,
                gradeModel.ModuleId, gradeModel.AssessmentId);
            Console.WriteLine(gradeModel.AssessmentId);
            Console.WriteLine(assessmentExist.Count());
            Console.WriteLine(gradeModel.StudentId);
            if (assessmentExist.Count() == 1)
            {
                Console.WriteLine("assessment grade has already been added for this student");
                return RedirectToAction("ViewModules", new { id = gradeModel.StudentId });
            }
            var assessmentMark = _assessmentService.GetAssessment(gradeModel.AssessmentId).AssessmentMaxScore;
            if (gradeModel.Mark <= decimal.Parse(assessmentMark))
            {
                _gradeService.AddGrade(gradeModel);
                Console.WriteLine("Grade has been successfully added");
                TempData["Message"] = "Grade has been successfully added";
                return RedirectToAction("ViewModules", new { id = gradeModel.StudentId });
            }
            TempData["Message"] = "student mark is higher than expected mark";
            Console.WriteLine("student mark is higher than expected mark");
            return RedirectToAction("ViewModules", new { id = gradeModel.StudentId });
        }
        
        [HttpGet]
        public ActionResult Edit(int id, GradeModel gradeModel)
        {
            ViewBag.assessments = new SelectList(_assessmentService.GetAssessmentsByModule(gradeModel.ModuleId), "AssessmentId", "Assessment Title");
            ViewBag.students = new SelectList(_studentService.GetStudents(), "StudentId", "StudentName");
            ViewBag.modules = new SelectList(_studentService.GetModuleByStudentId(gradeModel.StudentId), "ModuleId", "ModuleTitle");
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
            ViewBag.assessments = new SelectList(_assessmentService.GetAssessmentsByModule(gradeModel.ModuleId), "AssessmentId", "AssessmentTitle");
            ViewBag.students = new SelectList(_studentService.GetStudents(), "StudentId", "StudentName");
            ViewBag.modules = new SelectList(_studentService.GetModuleByStudentId(gradeModel.StudentId), "ModuleId", "ModuleTitle");

            if (ModelState.IsValid)
            {
                var assessmentMark = _assessmentService.GetAssessment(gradeModel.AssessmentId).AssessmentMaxScore;
                if (gradeModel.Mark <= decimal.Parse(assessmentMark))
                {
                    _gradeService.UpdateGrade(gradeModel);
                    Console.WriteLine("Grade has been successfully updated");
                    TempData["Message"] = "Grade has been successfully updated";
                    return RedirectToAction("Index");
                }
                TempData["Message"] = "student mark is higher than expected mark";
                Console.WriteLine("student mark is higher than expected mark");
                return View(gradeModel);
            }
            return View(gradeModel);
        }
    }
}
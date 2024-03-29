using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using StudentAdministrationSystem.data.Entities;
using StudentAdministrationSystem.data.Repository.Interface;
using StudentAdministrationSystem.Extensions;
using StudentAdministrationSystem.Models;
using StudentAdministrationSystem.Service;
using StudentAdministrationSystem.Service.Interface;

namespace StudentAdministrationSystem.Controllers
{
    public class StudentController : Controller
    {
        private IStudentService _studentService;
        private IProgrammeService _programmeService;
        private IModuleService _moduleService;
        private IGradeService _gradeService;
        
        public StudentController(IStudentService studentService, IProgrammeService programmeService, IModuleService moduleService,IGradeService gradeService)
        {
            _studentService = studentService;
            _programmeService = programmeService;
            _moduleService = moduleService;
            _gradeService = gradeService;
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
        
        [HttpGet]
        public ActionResult Edit(string id)
        {
            ViewBag.programmes = new SelectList(_programmeService.GetProgrammes(), "ProgrammeId", "ProgrammeTitle");

            var model = _studentService.GetStudent(id);
            if (model == null)
            {
                TempData["Message"] = "Student cannot be found";
                return View("Index");
            }
            return View(model);
        }
        
        [HttpPost]
        public ActionResult Edit(StudentModel studentModel)
        {
            ViewBag.programmes = new SelectList(_programmeService.GetProgrammes(), "ProgrammeId", "ProgrammeTitle");

            if (ModelState.IsValid)
            {
                if (studentModel == null)
                {
                    TempData["Message"] = "Student is not found";
                    return View("Index");
                }
                _studentService.UpdateStudent(studentModel);
                return RedirectToAction("Index");
            }
            return View(studentModel);
        }
        
        [HttpGet]
        public ActionResult SelectModules(string studentId)
        { 
            var modCount =_studentService.GetModuleByStudentId(studentId).Count();
            if (modCount > 0)
            {
                Console.WriteLine("Modules have been selected");
                TempData["Message"] = "Modules have been selected";
                return RedirectToAction("Index");
            }
            
            var student = _studentService.GetStudent(studentId);
            var moduleModelsByProgramme = _moduleService.GetModulesByProgramme(student.ProgrammeId);
            ViewBag.moduleByPrograms = moduleModelsByProgramme;
            ViewBag.studentId = studentId;
            return View(moduleModelsByProgramme);
        }

        [HttpPost]
        public JsonResult SelectModules(string moduleIds, string studentId)
        { 
            Console.WriteLine("student id is " + studentId);
            if (!string.IsNullOrEmpty(moduleIds))
            {
                var idss = moduleIds.Split('*').ToList();
                idss.RemoveAt(0);
                Console.WriteLine("count is " + idss.Count());
                if (idss.Count() > 0)
                {
                    foreach (var strid in idss)
                    {
                        if (!string.IsNullOrEmpty(strid))
                        {
                            Random r = new Random();
                            int randNum = r.Next(10000);
                            var stMdId = randNum.ToString("D4");
                            Console.WriteLine("count is " + randNum);
                            _studentService.AddModuleToStudent(strid, studentId, stMdId);
                        }
                    }
                    return Json(new { status = true, message = "Modules have been successfully added to student", JsonRequestBehavior.AllowGet });
                }
            }
            return Json(new { status = false, error = "Invalid Id" }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult StudentResult(string studentId, string moduleId)
        {
            ViewBag.stuId = studentId;
            
            var student = _studentService.GetStudent(studentId);
            ViewBag.stuYear = student.StudentYear;
            ViewBag.stuProgram = _programmeService.GetProgramme(student.ProgrammeId).ProgrammeTitle;
            
            var grades = _gradeService.GetGradesByStudentModule(studentId, moduleId);
            if (grades.Count() == 0)
            {
                TempData["Message"] = "Add Students Grades for this Module";
                Console.WriteLine("Add Students Grades for this Module");
                return RedirectToAction("Index", "Student");
            }
            ViewBag.grades = grades;

            var moduleMark = grades.Sum(g => g.Mark);
            ViewBag.totalScore = moduleMark;
            if (moduleMark >= 50)
            {
                ViewBag.moduleResult = Result.PASS;
            }
            else if(moduleMark < 50 && moduleMark >= 45)
            {
                ViewBag.moduleResult = Result.PASSCOMPENSATION; 
            }
            else if(moduleMark < 45)
            {
                ViewBag.moduleResult = Result.FAIL;
            }
            return View();
        }

        [HttpGet]
        public ActionResult ProgramResult(string studentId)
        {
            ViewBag.stuId = studentId;
            
            var student = _studentService.GetStudent(studentId);
            ViewBag.stuYear = student.StudentYear;
            ViewBag.stuProgram = _programmeService.GetProgramme(student.ProgrammeId).ProgrammeTitle;

            var sums = _studentService.GetStudentModuleGrade(studentId);
            
            var sumsCount = _studentService.GetStudentModuleGrade(studentId).Count();
            if (sumsCount == 0)
            {
                TempData["Message"] = "Students Grades does not exist";
                Console.WriteLine("Students Grades does not exist");
                return RedirectToAction("Index", "Student");
            }
            var avg = sums.Sum(s => s.Mark) / sumsCount;
            ViewBag.avg = avg;
            if (avg >= 70)
            {
                ViewBag.programmeResult = Result.DISTINCTION;
            }
            else if(avg >= 50 && avg < 70)
            {
                ViewBag.programmeResult = Result.PASS;
            }
            else if (avg < 50)
            {
                ViewBag.programmeResult = Result.FAIL;
            }
            
            ViewBag.studentGrades = sums;
            return View();
        }


        [HttpGet]
        public ActionResult Delete(string id)
        {
            _studentService.RemoveStudent(id);
            TempData["Message"] = "Student has been successfully deleted";
            return RedirectToAction("Index");
        }
    }
}
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using StudentAdministrationSystem.Models;
using StudentAdministrationSystem.Service.Interface;

namespace StudentAdministrationSystem.Controllers
{
    public class StudentController : Controller
    {
        private IStudentService _studentService;
        private IProgrammeService _programmeService;
        private IModuleService _moduleService;
        
        public StudentController(IStudentService studentService, IProgrammeService programmeService, IModuleService moduleService)
        {
            _studentService = studentService;
            _programmeService = programmeService;
            _moduleService = moduleService;
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
                return RedirectToAction("Index");
            }
            //student id
            var student = _studentService.GetStudent(studentId);
            var moduleModelsByProgramme = _moduleService.GetModulesByProgramme(student.ProgrammeId);
            ViewBag.moduleByPrograms = moduleModelsByProgramme;
            ViewBag.studentId = studentId;
            if (_programmeService.GetProgramme(student.ProgrammeId).ProgrammeDuration.Equals("One Year"))
            {
                ViewBag.moduleCount = "One Year Programme- Only Six Modules Should be Selected in Total, Compulsory Modules are checked already";
            }else
            {
                ViewBag.moduleCount = "Two Year Programme- Only Twelve Modules Should be Selected in Total, Compulsory Modules are checked already";
            }
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
                    // TempData["Message"] = "Module has been successfully added to student";
                    // return RedirectToAction("Index");
                }
            }
            return Json(new { status = false, error = "Invalid Id" }, JsonRequestBehavior.AllowGet);
            // TempData["Message"] = "Invalid Selection";
            // return View("SelectModules");
        }
        
    }
}
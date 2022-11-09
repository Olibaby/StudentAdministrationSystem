using System;
using System.Collections.Generic;
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
        public ActionResult Delete(string id)
        {
            _studentService.AddModuleToStudent("33637", "2021662023");
            TempData["Message"] = "Module has been successfully added to student";
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult SelectModules(string studentId)
        {
            //student id
            var student = _studentService.GetStudent(studentId);
            var moduleModel = _moduleService.GetModulesByProgramme(student.ProgrammeId);
            ViewBag.studentId = studentId;
            return View(moduleModel);
        }
        
        [HttpPost]
        public ActionResult SelectModules(IEnumerable<ModuleModel> moduleModel, string id)
        {
            if (moduleModel.Count() > 6)
            {
                TempData["Message"] = "Selection is not valid";
                return View("SelectModules");
            }
            //use select list to know selected modules
            foreach (ModuleModel moduleModels in moduleModel)
            {
                _studentService.AddModuleToStudent(moduleModels.ModuleId, id);
                TempData["Message"] = "Module has been successfully added to student";
                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");
        }
        
        [HttpGet]
        public ActionResult SelectedModules(string moduleId, string studentId)
        {
            if (!ModelState.IsValid)
            {
                TempData["Message"] = "Selection is not valid";
                return View("SelectModules");
            }
           //get program by module id
           //if program.programduration = 1
           //number of course should not be >6 i.e programduration * 6
            _studentService.AddModuleToStudent(moduleId, studentId);
            TempData["Message"] = "Module has been successfully added to student";
            return RedirectToAction("Index");
        }
    }
}
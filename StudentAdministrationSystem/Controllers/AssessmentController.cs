using System;
using System.Linq;
using System.Web.Mvc;
using StudentAdministrationSystem.Models;
using StudentAdministrationSystem.Service.Interface;

namespace StudentAdministrationSystem.Controllers
{
    public class AssessmentController : Controller
    {
        private IAssessmentService _assessmentService;
        private IModuleService _moduleService;
        
        public AssessmentController(IAssessmentService assessmentService,IModuleService moduleService)
        {
            _assessmentService = assessmentService;
            _moduleService = moduleService;
        }
        // GET
        public ActionResult Index()
        {
            var assessmentModel = _assessmentService.GetAssessments();
            return View(assessmentModel);
        }
        
        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.modules = new SelectList(_moduleService.GetModules(), "ModuleId", "ModuleTitle");
            return View();
        }
        
        [HttpPost]
        public ActionResult Create(AssessmentModel assessmentModel)
        {
            ViewBag.modules = new SelectList(_moduleService.GetModules(), "ModuleId", "ModuleTitle");
            if (!ModelState.IsValid)
            {
                TempData["Message"] = "Assessment is not valid";
                return RedirectToAction("Index", assessmentModel);
            }

            var assessmentMaxScore = _assessmentService.GetAssessmentsByModule(assessmentModel.ModuleId).Sum(a => decimal.Parse(a.AssessmentMaxScore));
            if ((decimal.Parse(assessmentModel.AssessmentMaxScore) + assessmentMaxScore) > 100)
            {
                TempData["Message"] = "Assessments total score cannot exceed 100";
                return RedirectToAction("Index", assessmentModel);
            }
            _assessmentService.AddAssessment(assessmentModel);
            TempData["Message"] = "Assessment has been successfully added";
            return RedirectToAction("Index");
        }
        
        [HttpGet]
        public ActionResult Edit(int id)
        {
            ViewBag.modules = new SelectList(_moduleService.GetModules(), "ModuleId", "ModuleTitle");

            var model = _assessmentService.GetAssessment(id);
            if (model == null)
            {
                TempData["Message"] = "Assessment cannot be found";
                return View("Index");
            }
            return View(model);
        }
        
        [HttpPost]
        public ActionResult Edit(AssessmentModel assessmentModel)
        {
            ViewBag.modules = new SelectList(_moduleService.GetModules(), "ModuleId", "ModuleTitle");

            if (ModelState.IsValid)
            {
                if (assessmentModel == null)
                {
                    TempData["Message"] = "Assessment is not found";
                    return View("Index");
                }
                var assessmentMaxScore = _assessmentService.GetAssessmentsByModule(assessmentModel.ModuleId).Sum(a => decimal.Parse(a.AssessmentMaxScore));
                if ((decimal.Parse(assessmentModel.AssessmentMaxScore) + assessmentMaxScore) > 100)
                {
                    TempData["Message"] = "Assessments total score cannot exceed 100";
                    return RedirectToAction("Index", assessmentModel);
                }
                _assessmentService.UpdateAssessment(assessmentModel);
                return RedirectToAction("Index");
            }
            return View(assessmentModel);
        }
    }
}
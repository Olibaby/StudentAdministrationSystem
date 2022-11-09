using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using StudentAdministrationSystem.Models;
using StudentAdministrationSystem.Service.Interface;

namespace StudentAdministrationSystem.Controllers
{
    public class ModuleController : Controller
    {
        private IModuleService _moduleService;
        private IProgrammeService _programmeService;
        
        public ModuleController(IModuleService moduleService, IProgrammeService programmeService)
        {
            _moduleService = moduleService;
            _programmeService = programmeService;
        }
        // GET
        public ActionResult Index()
        {
            var moduleModel = _moduleService.GetModules();
            return View(moduleModel);
        }
        
        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.programmes = new SelectList(_programmeService.GetProgrammes(), "ProgrammeId", "ProgrammeTitle");
            return View();
        }
        
        [HttpPost]
        public ActionResult Create(ModuleModel moduleModel)
        {
            ViewBag.programmes = new SelectList(_programmeService.GetProgrammes(), "ProgrammeId", "ProgrammeTitle");
            if (!ModelState.IsValid)
            {
                TempData["Message"] = "Module is not valid";
                return RedirectToAction("Index", moduleModel);
            }
           
            _moduleService.AddModule(moduleModel);
            TempData["Message"] = "Module has been successfully added";
            return RedirectToAction("Index");
        }
        
        [HttpGet]
        public ActionResult Edit(string id)
        {
            ViewBag.programmes = new SelectList(_programmeService.GetProgrammes(), "ProgrammeId", "ProgrammeTitle");

            if (id == null)
            {
                TempData["Message"] = "Module does not exist";
                return View("Index");
            }

            var model = _moduleService.GetModule(id);
            if (model == null)
            {
                TempData["Message"] = "Programme cannot be found";
                return View("Index");
            }
            return View(model);
        }
        
        [HttpPost]
        public ActionResult Edit(ModuleModel moduleModel)
        {
            if (ModelState.IsValid)
            {
                if (moduleModel == null)
                {
                    TempData["Message"] = "Programme is not found";
                    return View("Index");
                }
                ViewBag.programmes = new SelectList(_programmeService.GetProgrammes(), "ProgrammeId", "ProgrammeTitle");
                _moduleService.UpdateModule(moduleModel);
                return RedirectToAction("Index");
            }
            return View(moduleModel);
        }
    }
}
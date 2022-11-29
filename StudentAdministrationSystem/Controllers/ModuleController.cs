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
            var vm= new ModuleModel();
            vm.States = new List<SelectListItem> {
                new SelectListItem { Value="1", Text="Compulsory" },
                new SelectListItem { Value ="2", Text="Optional" }
            };
            return View(vm);
        }
        
        [HttpPost]
        public ActionResult Create(ModuleModel moduleModel)
        {
            ViewBag.programmes = new SelectList(_programmeService.GetProgrammes(), "ProgrammeId", "ProgrammeTitle");
            if (!ModelState.IsValid)
            {
                TempData["Message"] = "Module is not valid";
                // ModelState.AddModelError("name","Module is not valid");
                // var errors = ModelState.Values.SelectMany(v => v.Errors);
                // foreach (var e in errors)
                // {
                //     Console.WriteLine("error" + e.ErrorMessage);
                // }
                return RedirectToAction("Create");
            }
            var vm= new ModuleModel();
            vm.States = new List<SelectListItem> {
                new SelectListItem { Value="1", Text="Compulsory" },
                new SelectListItem { Value ="2", Text="Optional" }
            };
            var selectedDuration = vm.States.First(x => x.Value == moduleModel.SelectedStatesId).Text;
            moduleModel.ModuleType = selectedDuration;
            
            var isExist = _moduleService.GetModules().Any(m => m.ModuleTitle == moduleModel.ModuleTitle);
            if (isExist)
            {
                TempData["Message"] = "Module Already Exist";
                return RedirectToAction("Create");
            }
            
            var moduleCount = _moduleService.GetModulesByProgramme(moduleModel.ProgrammeId).Length;
            var programModuleNo = _programmeService.GetProgramme(moduleModel.ProgrammeId).ProgrammeModuleNo;
            if (moduleCount >= programModuleNo)
            {
                Console.WriteLine("You have reached maximum number of modules for this programme");
                TempData["Message"] = "You have reached maximum number of modules for this programme";
                return RedirectToAction("Index");
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
            ViewBag.programmes = new SelectList(_programmeService.GetProgrammes(), "ProgrammeId", "ProgrammeTitle");
            
            model.States = new List<SelectListItem> {
                new SelectListItem { Value="1", Text="Compulsory" },
                new SelectListItem { Value ="2", Text="Optional" }
            };
            
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
                
                var vm= new ModuleModel();
                vm.States = new List<SelectListItem> {
                    new SelectListItem { Value="1", Text="Compulsory" },
                    new SelectListItem { Value ="2", Text="Optional" } 
                };
                var selectedDuration = vm.States.First(x => x.Value == moduleModel.SelectedStatesId).Text;
                moduleModel.ModuleType = selectedDuration;
                
                var moduleCount = _moduleService.GetModulesByProgramme(moduleModel.ProgrammeId).Length;
                var programModuleNo = _programmeService.GetProgramme(moduleModel.ProgrammeId).ProgrammeModuleNo;
                if (moduleCount >= programModuleNo)
                {
                    Console.WriteLine("You have reached maximum number of modules for this programme");
                    TempData["Message"] = "You have reached maximum number of modules for this programme";
                    return RedirectToAction("Edit", "Module", moduleModel.ModuleId);
                }
                
                _moduleService.UpdateModule(moduleModel);
                return RedirectToAction("Index");
            }
            return View(moduleModel);
        }
        
        [HttpGet]
        public ActionResult Delete(string id)
        {
            _moduleService.RemoveModule(id);
            TempData["Message"] = "Module has been successfully deleted";
            return RedirectToAction("Index");
        }
    }
}
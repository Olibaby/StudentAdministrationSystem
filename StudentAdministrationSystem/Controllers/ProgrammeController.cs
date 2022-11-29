using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Microsoft.Ajax.Utilities;
using StudentAdministrationSystem.data.Entities;
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
                ModelState.AddModelError("name","Program is not valid");
                return RedirectToAction("Index", programmeModel);
            }
            if (programmeModel.ProgrammeDuration == 0)
            {
                TempData["Message"] = "Programme Duration cannot be Zero";
                return RedirectToAction("Create");
            }
            var isExist = _programmeService.GetProgrammes().Any(m => m.ProgrammeTitle == programmeModel.ProgrammeTitle);
            if (isExist)
            {
                TempData["Message"] = "Programme Already Exist";
                return RedirectToAction("Create");
            }
            _programmeService.AddProgramme(programmeModel);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Edit(String id)
        {
            if (id == null)
            {
                TempData["Message"] = "Programme does not exist";
                return View("Index");
            }
            var model = _programmeService.GetProgramme(id);
            if (model == null)
            {
                TempData["Message"] = "Programme cannot be found";
                return View("Index");
            }
            return View(model);
        }
        
        [HttpPost]
        public ActionResult Edit(ProgrammeModel model)
        {
            if (ModelState.IsValid)
            {
                if (model == null)
                {
                    TempData["Message"] = "Programme is not found";
                    return View("Index");
                }
                _programmeService.UpdateProgramme(model);
                return RedirectToAction("Index");
            }
            return View(model);
        }
        
        [HttpGet]
        public ActionResult Delete(string id)
        {
            _programmeService.RemoveProgramme(id);
            TempData["Message"] = "Assessment has been successfully deleted";
            return RedirectToAction("Index");
        }
    }
}
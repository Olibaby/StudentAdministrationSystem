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
            var vm= new ProgrammeModel();
            // Hard coded for demo. You can replace with real data from db
            // ViewBag.colleges = new SelectList(_attmgr.GetColleges(), "CollegeId", "CollegeName");
            vm.Durations = new List<SelectListItem> {
                new SelectListItem { Value="1", Text="One Year" },
                new SelectListItem { Value ="2", Text="Two Years" }
            };
            return View(vm);
        }
        
        [HttpPost]
        public ActionResult Create(ProgrammeModel programmeModel)
        {
            if (!ModelState.IsValid)
            {
                TempData["Message"] = "Program is not valid";
                return RedirectToAction("Index", programmeModel);
            }
            var vm= new ProgrammeModel();
            // Hard coded for demo. You can replace with real data from db
            // ViewBag.colleges = new SelectList(_attmgr.GetColleges(), "CollegeId", "CollegeName");
            vm.Durations = new List<SelectListItem> {
                new SelectListItem { Value="1", Text="One Year" },
                new SelectListItem { Value ="2", Text="Two Years" }
            };
            var selectedDuration = vm.Durations.First(x => x.Value == programmeModel.SelectedDurationsId).Text;
            _programmeService.AddProgramme(new ProgrammeModel
            {
                ProgrammeId = vm.GenerateProgrammeId(),
                ProgrammeDuration = selectedDuration,
                ProgrammeTitle = programmeModel.ProgrammeTitle,
                CreatedDate = DateTime.Now
            });
            TempData["Message"] = "Program has been successfully added";
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Edit(String id)
        {
            var vm= new ProgrammeModel();
            vm.Durations = new List<SelectListItem> {
                new SelectListItem { Value="1", Text="One Year" },
                new SelectListItem { Value ="2", Text="Two Years" }
            };
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
            var programmeModel = new ProgrammeModel
            {
               ProgrammeId = model.ProgrammeId,
               ProgrammeTitle = model.ProgrammeTitle,
               ProgrammeDuration = model.ProgrammeDuration,
               Durations = vm.Durations,
               SelectedDurationsId = model.SelectedDurationsId
            };
            return View(programmeModel);
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
                var vm= new ProgrammeModel();
                vm.Durations = new List<SelectListItem> {
                    new SelectListItem { Value="1", Text="One Year" },
                    new SelectListItem { Value ="2", Text="Two Years" }
                };
                var selectedDuration = vm.Durations.First(x => x.Value == model.SelectedDurationsId).Text;
                _programmeService.UpdateProgramme(model.ProgrammeId, new ProgrammeModel
                {
                    ProgrammeId = model.ProgrammeId,
                    ProgrammeDuration = selectedDuration,
                    ProgrammeTitle = model.ProgrammeTitle,
                    ModifiedDate = DateTime.Now
                });
                return RedirectToAction("Index");
            }
            return View(model);
        }
        
        [HttpPost]
        public ActionResult Delete(String id, string programmeName)
        {
            if (int.Parse(id) > 0)
            {
                try
                {
                    _programmeService.RemoveProgramme(id);
                    return Json(new { status = true, message = $" Programme has been successfully deleted!", JsonRequestBehavior.AllowGet });
                }
                catch (Exception ex)
                {
                    return Json(new { status = false, error = ex.Message }, JsonRequestBehavior.AllowGet);
                }

            }
            return Json(new { status = false, error = "Invalid Id" }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult DeleteIds(string ids)
        {
            if (!string.IsNullOrEmpty(ids))
            {
                List<string> idss = ids.Split('*').ToList();
                if (idss.Count() > 0)
                {
                    foreach (var strid in idss)
                    {
                        if (!string.IsNullOrEmpty(strid))
                        {
                            int intid = Convert.ToInt32(strid);
                            // _programmeService.RemoveProgramme(intid);
                        }
                    }
                    return Json(new { status = true, message = " All selected programme(s) has been successfully deleted!", JsonRequestBehavior.AllowGet });

                    //return Json(new { status = false, error = result.Message }, JsonRequestBehavior.AllowGet);
                }


            }

            return Json(new { status = false, error = "Invalid Id" }, JsonRequestBehavior.AllowGet);
        }
    }
}
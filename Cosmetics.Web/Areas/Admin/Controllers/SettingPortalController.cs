using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cosmetics.BLL.DTO.ConfigurationModelDTO;
using Cosmetics.BLL.Services.Language;
using Cosmetics.BLL.Services.SettingPortal;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Cosmetics.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    //[Authorize(Roles = "Admin")]
    public class SettingPortalController : Controller
    {
        private readonly ISettingPortalService _settingPortalService;
        private readonly ILanguageService _languageService;

        public SettingPortalController(ISettingPortalService settingPortalService, ILanguageService languageService)
        {
            _settingPortalService = settingPortalService;
            _languageService = languageService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string message)
        {
            ViewBag.Message = message;

            EditSettingPortalDTO category = await _settingPortalService.GetSettingPortal();
            ViewBag.Languages = _languageService.GetAllPublishLanguage();
            if (category != null)
            {
                return View(category);
            }
            else
            {
                EditSettingPortalDTO editSetting = new EditSettingPortalDTO();
                return View(editSetting);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditSettingPortalDTO value)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _settingPortalService.EditSettingPortal(value);
                    return RedirectToAction("Edit", new { message = "success" });
                }
                catch(Exception ex)
                {
                    await _settingPortalService.EditSettingPortal(value);
                    return RedirectToAction("Edit", new { message = "danger" });
                }
                
            }
            ViewBag.Languages = _languageService.GetAllPublishLanguage();

            return View(value);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Cosmetics.BLL.DTO.ConfigurationModelDTO;
using Cosmetics.BLL.Services.SettingPortal;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Cosmetics.Web.Areas.Admin.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class SettingPortalAPIController : ControllerBase
    {
        private readonly ISettingPortalService _settingPortalService;

        public SettingPortalAPIController(ISettingPortalService settingPortalService)
        {
            _settingPortalService = settingPortalService;
        }

        // GET: api/SettingPortalAPI/GetSettingPortal
        [HttpGet("GetSettingPortal")]
        public async Task<IActionResult> GetSettingPortal()
        {
            EditSettingPortalDTO editSettingPortalDTO = await _settingPortalService.GetSettingPortal();
            return Ok(editSettingPortalDTO);
        }

        // GET: api/SettingPortalAPI/GetSettingPortalTranslate
        [HttpGet("GetSettingPortalTranslate")]
        public async Task<IActionResult> GetSettingPortalTranslate()
        {
            string culture = CultureInfo.CurrentCulture.TwoLetterISOLanguageName;
            SettingPortalDTO settingPortalDTO = await _settingPortalService.GetSettingPortalTranslate(culture);
            return Ok(settingPortalDTO);
        }

        // PUT: api/SettingPortalAPI
        [HttpPut]
        public async Task<IActionResult> Put([FromForm] EditSettingPortalDTO value)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _settingPortalService.EditSettingPortal(value);

            return Ok(value);
        }
    }
}
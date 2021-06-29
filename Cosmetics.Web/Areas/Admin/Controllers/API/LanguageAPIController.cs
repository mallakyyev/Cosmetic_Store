using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cosmetics.BLL.DTO.ConfigurationModelDTO;
using Cosmetics.BLL.Services.Language;
using Cosmetics.Web.Binder;
using DevExtreme.AspNet.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Cosmetics.Web.Areas.Admin.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class LanguageAPIController : ControllerBase
    {
        private readonly ILanguageService _languageService;

        public LanguageAPIController(ILanguageService languageService)
        {
            _languageService = languageService;
        }

        // GET: api/Language
        [HttpGet]
        public object Get(DataSourceLoadOptions loadOptions)
        {
            return DataSourceLoader.Load<LanguageDTO>(_languageService.GetAllLanguage(), loadOptions);
        }

        // GET: api/Language/GetPublishLanguages
        [HttpGet("GetPublishLanguages")]
        public IEnumerable<LanguageDTO> GetPublishLanguages()
        {
            var languageDTOs = _languageService.GetAllPublishLanguage();
            return languageDTOs.ToArray();
        }

        // GET: api/Language/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Language
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateLanguageDTO value)
        {
            if (ModelState.IsValid)
            {
                await _languageService.CreateLanguage(value);
                return Ok(value);
            }
            return BadRequest();
        }

        // PUT: api/Language/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string culture, [FromBody] LanguageDTO value)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (culture != value.Culture)
            {
                return BadRequest();
            }

            await _languageService.EditLanguage(value);

            return Ok(value);
        }

        // DELETE: api/Language/5
        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            await _languageService.RemoveLanguage(id);
        }
    }
}

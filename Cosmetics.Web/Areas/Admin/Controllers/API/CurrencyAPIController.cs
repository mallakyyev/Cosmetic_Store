using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cosmetics.BLL.DTO.ConfigurationModelDTO;
using Cosmetics.BLL.Services.Currency;
using Cosmetics.Web.Binder;
using DevExtreme.AspNet.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Cosmetics.Web.Areas.Admin.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class CurrencyAPIController : ControllerBase
    {
        private readonly ICurrencyService _currencyService;

        public CurrencyAPIController(ICurrencyService currencyService)
        {
            _currencyService = currencyService;
        }

        // GET: api/Currency
        [HttpGet]
        public object Get(DataSourceLoadOptions loadOptions)
        {
            return DataSourceLoader.Load<CurrencyDTO>(_currencyService.GetAllCurrency(), loadOptions);
        }

        // GET: api/Currency/GetPublishCurrencies
        [HttpGet("GetPublishCurrencies")]
        public IEnumerable<CurrencyDTO> GetPublishCurrencies()
        {
            var currencyDTOs = _currencyService.GetAllPublishCurrency();
            return currencyDTOs.ToArray();
        }

        // GET: api/Currency/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Currency
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateCurrencyDTO value)
        {
            if (ModelState.IsValid)
            {
                await _currencyService.CreateCurrency(value);
                return Ok(value);
            }
            return BadRequest();
        }

        // PUT: api/Currency/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] CurrencyDTO value)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != value.Id)
            {
                return BadRequest();
            }

            await _currencyService.EditCurrency(value);

            return Ok(value);
        }

        // DELETE: api/Currency/5
        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            await _currencyService.RemoveCurrency(id);
        }
    }
}

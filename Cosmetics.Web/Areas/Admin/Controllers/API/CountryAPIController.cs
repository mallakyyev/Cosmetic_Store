using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cosmetics.BLL.DTO.ConfigurationModelDTO;
using Cosmetics.BLL.Services.Country;
using Cosmetics.Web.Binder;
using DevExtreme.AspNet.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Cosmetics.Web.Areas.Admin.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountryAPIController : ControllerBase
    {
        private readonly ICountryService _countryService;

        public CountryAPIController(ICountryService countryService)
        {
            _countryService = countryService;
        }

        // GET: api/CountryAPI
        [HttpGet]
        public object Get(DataSourceLoadOptions loadOptions)
        {
            return DataSourceLoader.Load<CountryDTO>(_countryService.GetAllCountries(), loadOptions);
        }

        // GET: api/CountryAPI/GetPublishCountries
        [HttpGet("GetPublishCountries")]
        public IEnumerable<CountryDTO> GetPublishCountries()
        {
            var countryDTOs = _countryService.GetAllPublishCountry();
            return countryDTOs.ToArray();
        }

        // Get: api/CountryAPI/GetDeliveryPriceCountry/1
        [HttpGet("GetDeliveryPriceCountry/{id}")]
        public decimal GetDeliveryPriceCountry(int id)
        {
            var country = _countryService.GetDeliveryPriceCountry(id);
            return country.PriceDelivery;
        }

        // POST: api/CountryAPI
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CountryDTO value)
        {
            if (ModelState.IsValid)
            {
                await _countryService.CreateCountry(value);
                return Ok(value);
            }
            return BadRequest();
        }

        // PUT: api/CountryAPI/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] CountryDTO value)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != value.Id)
            {
                return BadRequest();
            }

            await _countryService.EditCountry(value);

            return Ok(value);
        }

        // DELETE: api/CountryAPI/5
        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            await _countryService.RemoveCountry(id);
        }
    }
}
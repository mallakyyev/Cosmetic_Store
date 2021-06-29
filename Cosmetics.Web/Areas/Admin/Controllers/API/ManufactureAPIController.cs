using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cosmetics.BLL.DTO.ManufactureModelDTO;
using Cosmetics.BLL.Services.Manufacture;
using Cosmetics.Web.Binder;
using DevExtreme.AspNet.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Cosmetics.Web.Areas.Admin.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class ManufactureAPIController : ControllerBase
    {
        private readonly IManufactureService _manufactureService;

        public ManufactureAPIController(IManufactureService manufactureService)
        {
            _manufactureService = manufactureService;
        }

        [HttpGet]
        public object Get(DataSourceLoadOptions loadOptions)
        {
            return DataSourceLoader.Load<ManufactureDTO>(_manufactureService.GetAllManufacture(), loadOptions);
        }

        // GET: api/Manufacture/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Manufacture
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateManufactureDTO value)
        {
            if (ModelState.IsValid)
            {
                await _manufactureService.CreateManufacture(value);
                return Ok(value);
            }
            return BadRequest();
        }

        // PUT: api/Manufacture/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] ManufactureDTO value)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != value.Id)
            {
                return BadRequest();
            }

            await _manufactureService.EditManufacture(value);

            return Ok(value);
        }

        // DELETE: api/Manufacture/5
        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            await _manufactureService.RemoveManufacture(id);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cosmetics.BLL.DTO.ApplicationUserModelDTO;
using Cosmetics.BLL.Services.ApplicationUser;
using Cosmetics.DAL.Models.Users;
using Cosmetics.Web.Binder;
using DevExtreme.AspNet.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Cosmetics.Web.Areas.Admin.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountAPIController : ControllerBase
    {
        private readonly IApplicationUserService _applicationUserService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AccountAPIController(IApplicationUserService applicationUserService, UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            _applicationUserService = applicationUserService;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        // GET: api/AccountAPI
        [HttpGet]
        public async Task<object> Get(DataSourceLoadOptions loadOptions)
        {
            return DataSourceLoader.Load<ApplicationUserDTO>(await _applicationUserService.GetAllUsers(), loadOptions);
        }

        // POST: api/AccountAPI
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/AccountAPI/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id, [FromBody] ApplicationUserDTO value)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.Users.FirstOrDefaultAsync(p => p.Id == id);
                var selectedRoleNames = await _userManager.GetRolesAsync(user);

                user.EmailConfirmed = value.EmailConfirmed;

                if (value.Role != selectedRoleNames.FirstOrDefault())
                {
                    await _userManager.RemoveFromRolesAsync(user, selectedRoleNames);
                    await _userManager.AddToRoleAsync(user, value.Role);
                }

                await _userManager.UpdateAsync(user);

                return Ok();
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        // DELETE: api/Account/5
        [HttpDelete("{id}")]
        public async Task Delete(string id)
        {
            await _applicationUserService.DeleteUser(id);
        }
    }
}

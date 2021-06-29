using AutoMapper;
using Cosmetics.BLL.DTO.ApplicationUserModelDTO;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cosmetics.BLL.Services.ApplicationUser
{
    public class ApplicationUserService : IApplicationUserService
    {
        private readonly UserManager<Cosmetics.DAL.Models.Users.ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IMapper _mapper;

        public ApplicationUserService(UserManager<Cosmetics.DAL.Models.Users.ApplicationUser> userManager, 
            RoleManager<IdentityRole> roleManager, IMapper mapper)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _mapper = mapper;
        }

        public async Task DeleteUser(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user != null)
            {
                IdentityResult result = await _userManager.DeleteAsync(user);
            }
        }

        public async Task<IEnumerable<ApplicationUserDTO>> GetAllUsers()
        {
            var applicationUsers = _userManager.Users.OrderBy(o => o.FirstName);
            List<ListApplicationUserDTO> listApplicationUserDTO = new List<ListApplicationUserDTO>();

            //var applicationUserDTOs = _mapper.Map<IEnumerable<Cosmetics.DAL.Models.Users.ApplicationUser>, IEnumerable<ApplicationUserDTO>>(applicationUsers);

            foreach (var applicationUser in applicationUsers)
            {
                var user = await _userManager.Users.FirstOrDefaultAsync(p => p.Id == applicationUser.Id);
                var rolesUser = await _userManager.GetRolesAsync(user);
                listApplicationUserDTO.Add(new ListApplicationUserDTO
                {
                    Id = applicationUser.Id,
                    FullName = applicationUser.FullName,
                    FirstName = applicationUser.FirstName,
                    LastName = applicationUser.LastName,
                    Email = applicationUser.Email,
                    EmailConfirmed = applicationUser.EmailConfirmed,
                    Role = rolesUser.FirstOrDefault()
                });
            }

            var applicationUserDTOs = _mapper.ProjectTo<ApplicationUserDTO>(listApplicationUserDTO.AsQueryable());
            return applicationUserDTOs;
        }
    }
}

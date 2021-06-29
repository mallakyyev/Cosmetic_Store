using Cosmetics.BLL.DTO.ApplicationUserModelDTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Cosmetics.BLL.Services.ApplicationUser
{
    public interface IApplicationUserService
    {
        Task<IEnumerable<ApplicationUserDTO>> GetAllUsers();

        Task DeleteUser(string id);
    }
}

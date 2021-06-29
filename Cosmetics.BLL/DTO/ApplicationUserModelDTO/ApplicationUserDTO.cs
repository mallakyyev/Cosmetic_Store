using System;
using System.Collections.Generic;
using System.Text;

namespace Cosmetics.BLL.DTO.ApplicationUserModelDTO
{
    public class ApplicationUserDTO
    {
        public string Id { get; set; }

        public string FullName { get; set; }

        public string Email { get; set; }

        public bool EmailConfirmed { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Role { get; set; }

    }
}

using Cosmetics.BLL.DTO.ConfigurationModelDTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Cosmetics.BLL.Services.SettingPortal
{
    public interface ISettingPortalService
    {

        Task EditSettingPortal(EditSettingPortalDTO modelDTO);

        Task<EditSettingPortalDTO> GetSettingPortal();
        Task<SettingPortalDTO> GetSettingPortalTranslate(string culture);
    }
}

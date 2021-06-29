using Cosmetics.BLL.DTO.ConfigurationModelDTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Cosmetics.BLL.Services.Language
{
    public interface ILanguageService
    {
        IEnumerable<LanguageDTO> GetAllLanguage();
        IEnumerable<LanguageDTO> GetAllPublishLanguage();

        Task CreateLanguage(CreateLanguageDTO modelDTO);

        Task EditLanguage(LanguageDTO modelDTO);

        Task RemoveLanguage(int id);
    }
}

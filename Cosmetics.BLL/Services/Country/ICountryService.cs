using Cosmetics.BLL.DTO.ConfigurationModelDTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Cosmetics.BLL.Services.Country
{
    public interface ICountryService
    {
        IEnumerable<CountryDTO> GetAllCountries();

        IEnumerable<CountryDTO> GetAllPublishCountry();

        CountryDTO GetDeliveryPriceCountry(int id);

        Task CreateCountry(CountryDTO modelDTO);

        Task EditCountry(CountryDTO modelDTO);

        Task RemoveCountry(int id);
    }
}

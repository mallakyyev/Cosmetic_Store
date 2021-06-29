using Cosmetics.BLL.DTO.ConfigurationModelDTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Cosmetics.BLL.Services.Currency
{
    public interface ICurrencyService
    {
        IEnumerable<CurrencyDTO> GetAllCurrency();

        IEnumerable<CurrencyDTO> GetAllPublishCurrency();

        Task CreateCurrency(CreateCurrencyDTO modelDTO);

        Task EditCurrency(CurrencyDTO modelDTO);

        Task RemoveCurrency(int id);
    }
}

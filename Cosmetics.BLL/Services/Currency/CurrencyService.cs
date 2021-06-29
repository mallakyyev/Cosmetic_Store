using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Cosmetics.BLL.DTO.ConfigurationModelDTO;
using Cosmetics.DAL.Data;
using currency = Cosmetics.DAL.Models.Configuration;

namespace Cosmetics.BLL.Services.Currency
{
    public class CurrencyService : ICurrencyService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public CurrencyService(ApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task CreateCurrency(CreateCurrencyDTO modelDTO)
        {
            currency.Currency crc = _mapper.Map<currency.Currency>(modelDTO);
            await _dbContext.Currencies.AddAsync(crc);
            await _dbContext.SaveChangesAsync();
        }

        public async Task EditCurrency(CurrencyDTO modelDTO)
        {
            currency.Currency crc = _mapper.Map<currency.Currency>(modelDTO);
            _dbContext.Currencies.Update(crc);
            await _dbContext.SaveChangesAsync();
        }

        public IEnumerable<CurrencyDTO> GetAllCurrency()
        {
            var currencyDTOs = _mapper.Map<IEnumerable<currency.Currency>, IEnumerable<CurrencyDTO>>(GetList());
            return currencyDTOs;
        }

        public IEnumerable<CurrencyDTO> GetAllPublishCurrency()
        {
            var currencyDTOs = _mapper.Map<IEnumerable<currency.Currency>, IEnumerable<CurrencyDTO>>(GetList().Where(p => p.Published == true));
            return currencyDTOs;
        }

        public IEnumerable<currency.Currency> GetList()
        {
            var currencies = _dbContext.Currencies.ToList();
            return currencies;
        }

        public async Task RemoveCurrency(int id)
        {
            currency.Currency crc = await _dbContext.Currencies.FindAsync(id);
            _dbContext.Currencies.Remove(crc);
            await _dbContext.SaveChangesAsync();
        }
    }
}

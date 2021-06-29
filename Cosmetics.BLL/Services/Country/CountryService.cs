using AutoMapper;
using Cosmetics.BLL.DTO.ConfigurationModelDTO;
using Cosmetics.DAL.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using model = Cosmetics.DAL.Models.Configuration;

namespace Cosmetics.BLL.Services.Country
{
    public class CountryService : ICountryService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public CountryService(ApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task CreateCountry(CountryDTO modelDTO)
        {
            model.Country country = _mapper.Map<model.Country>(modelDTO);
            await _dbContext.Countries.AddAsync(country);
            await _dbContext.SaveChangesAsync();
        }

        public async Task EditCountry(CountryDTO modelDTO)
        {
            model.Country country = _mapper.Map<model.Country>(modelDTO);
            _dbContext.Countries.Update(country);
            await _dbContext.SaveChangesAsync();
        }

        public IEnumerable<CountryDTO> GetAllCountries()
        {
            var countryDTOs = _mapper.Map<IEnumerable<model.Country>, IEnumerable<CountryDTO>>(GetList());
            return countryDTOs;
        }

        public IEnumerable<CountryDTO> GetAllPublishCountry()
        {
            var countryDTOs = _mapper.Map<IEnumerable<model.Country>, IEnumerable<CountryDTO>>(GetList().Where(p => p.Published == true));
            return countryDTOs;
        }

        public CountryDTO GetDeliveryPriceCountry(int id)
        {
            var country = _mapper.Map<model.Country, CountryDTO>(_dbContext.Countries.Find(id));
            return country;
        }

        public IEnumerable<model.Country> GetList()
        {
            var countries = _dbContext.Countries.ToList();
            return countries;
        }

        public async Task RemoveCountry(int id)
        {
            model.Country country = await _dbContext.Countries.FindAsync(id);
            _dbContext.Countries.Remove(country);
            await _dbContext.SaveChangesAsync();
        }
    }
}

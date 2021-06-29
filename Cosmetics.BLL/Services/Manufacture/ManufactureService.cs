using AutoMapper;
using Cosmetics.BLL.DTO.ManufactureModelDTO;
using Cosmetics.DAL.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using manufacture = Cosmetics.DAL.Models.Catalog;

namespace Cosmetics.BLL.Services.Manufacture
{
    public class ManufactureService: IManufactureService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public ManufactureService(ApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task CreateManufacture(CreateManufactureDTO modelDTO)
        {
            manufacture.Manufacture mnf = _mapper.Map<manufacture.Manufacture>(modelDTO);
            await _dbContext.Manufactures.AddAsync(mnf);
            await _dbContext.SaveChangesAsync();
        }

        public async Task EditManufacture(ManufactureDTO modelDTO)
        {
            manufacture.Manufacture mnf = _mapper.Map<manufacture.Manufacture>(modelDTO);
            _dbContext.Manufactures.Update(mnf);
            await _dbContext.SaveChangesAsync();
        }

        public IEnumerable<ManufactureDTO> GetAllManufacture()
        {
            var manufactureDTOs = _mapper.Map<IEnumerable<manufacture.Manufacture>, IEnumerable<ManufactureDTO>>(GetList());
            return manufactureDTOs;
        }

        public IEnumerable<manufacture.Manufacture> GetList()
        {
            var manufactures = _dbContext.Manufactures.ToList();
            return manufactures;
        }

        public async Task RemoveManufacture(int id)
        {
            manufacture.Manufacture mnf = await _dbContext.Manufactures.FindAsync(id);
            _dbContext.Manufactures.Remove(mnf);
            await _dbContext.SaveChangesAsync();
        }
    }
}

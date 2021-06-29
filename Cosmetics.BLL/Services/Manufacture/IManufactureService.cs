using Cosmetics.BLL.DTO.ManufactureModelDTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Cosmetics.BLL.Services.Manufacture
{
    public interface IManufactureService
    {
        IEnumerable<ManufactureDTO> GetAllManufacture();

        Task CreateManufacture(CreateManufactureDTO modelDTO);

        Task EditManufacture(ManufactureDTO modelDTO);

        Task RemoveManufacture(int id);
    }
}

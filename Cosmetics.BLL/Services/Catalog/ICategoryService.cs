using Cosmetics.BLL.DTO.CatalogModelDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cosmetics.BLL.Services.Catalog
{
    public interface ICategoryService
    {
        IEnumerable<CategoryDTO> GetAllCategory();

        IEnumerable<CategoryDTO> GetAllPublishCategory(FilterCategoryDTO filterCategoryDTO);

        IEnumerable<CategoryDTO> GetAllPublishParentCategories(FilterCategoryDTO filterCategoryDTO);

        Task CreateCategory(CreateCategoryDTO modelDTO);

        Task EditCategory(EditCategoryDTO modelDTO);

        Task RemoveCategory(int id);

        Task<EditCategoryDTO> GetCategoryId(int id);
    }
}

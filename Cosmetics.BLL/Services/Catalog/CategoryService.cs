using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Cosmetics.BLL.DTO.CatalogModelDTO;
using Cosmetics.DAL.Data;
using Cosmetics.DAL.Models.Catalog;
using Microsoft.EntityFrameworkCore;

namespace Cosmetics.BLL.Services.Catalog
{
    public class CategoryService : ICategoryService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public CategoryService(ApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task CreateCategory(CreateCategoryDTO modelDTO)
        {
            Category category = _mapper.Map<Category>(modelDTO);
            await _dbContext.Categories.AddAsync(category);
            await _dbContext.SaveChangesAsync();

        }

        public async Task EditCategory(EditCategoryDTO modelDTO)
        {
            Category category = _mapper.Map<Category>(modelDTO);
            _dbContext.Categories.Update(category);
            await _dbContext.SaveChangesAsync();
        }

        public IEnumerable<CategoryDTO> GetAllCategory()
        {
            string culture = CultureInfo.CurrentCulture.TwoLetterISOLanguageName;
            var categories = _dbContext.Categories;
            var result = _dbContext.CategoryTranslates
                .Where(p => p.LanguageCulture == culture).Join(categories, p => p.CategoryId, k => k.Id,
                (p, k) => new CategoryDTO
                {
                    Id = k.Id,
                    Name = p.Name,
                    Description = p.Description,
                    DisplayOrder = k.DisplayOrder,
                    ParentCategoryId = k.ParentCategoryId,
                    Published = k.Published
                });
            //var categoryDTOs = _mapper.Map<IEnumerable<CategoryTranslate>, IEnumerable<CategoryDTO>>(result);
            return result;
        }

        public IEnumerable<CategoryDTO> GetAllPublishCategory(FilterCategoryDTO filterCategoryDTO)
        {
            var categories = _dbContext.Categories.Where(p => p.Published == true);
            var result = _dbContext.CategoryTranslates
                .Where(p => p.LanguageCulture == filterCategoryDTO.LanguageCulture).Join(categories, p => p.CategoryId, k => k.Id,
                (p, k) => new CategoryDTO
                {
                    Id = k.Id,
                    Name = p.Name,
                    Description = p.Description,
                    DisplayOrder = k.DisplayOrder,
                    ParentCategoryId = k.ParentCategoryId,
                    Published = k.Published
                });
            return result;
        }

        public IEnumerable<CategoryDTO> GetAllPublishParentCategories(FilterCategoryDTO filterCategoryDTO)
        {
            var categories = _dbContext.Categories.Where(p => p.Published == true && p.ParentCategoryId == filterCategoryDTO.ParentId);
            var result = _dbContext.CategoryTranslates
                .Where(p => p.LanguageCulture == filterCategoryDTO.LanguageCulture).Join(categories, p => p.CategoryId, k => k.Id,
                (p, k) => new CategoryDTO
                {
                    Id = k.Id,
                    Name = p.Name,
                    Description = p.Description,
                    DisplayOrder = k.DisplayOrder,
                    ParentCategoryId = k.ParentCategoryId,
                    Published = k.Published
                });
            return result;
        }

        public async Task<EditCategoryDTO> GetCategoryId(int id)
        {
            Category category = await _dbContext.Categories.Include(i => i.CategoryTranslates)
                .SingleOrDefaultAsync(s => s.Id == id);
            EditCategoryDTO categoryDTO = _mapper.Map<EditCategoryDTO>(category);
            return categoryDTO;
        }

        //public IEnumerable<CategoryTranslateDTO> GetList()
        //{
        //    var categories = _dbContext.Categories.Include(p => p.Categories)
        //        .AsEnumerable();
        //    var result = _dbContext.CategoryTranslates.Join(categories, p => p.CategoryId, k => k.Id,
        //        (p, k) => new CategoryTranslateDTO
        //        {

        //        });
        //    //var categories = _dbContext.CategoryTranslates
        //    //    .Include(i => i.Category)
        //    //    .ThenInclude(p=>p.Categories)
        //    //    .Where(s => s.LanguageId == 1)
        //    //    .AsEnumerable()
        //    //    .ToList();
        //    //return categories;
        //}

        public async Task RemoveCategory(int id)
        {
            Category category = await _dbContext.Categories.FindAsync(id);
            _dbContext.Categories.Remove(category);
            await _dbContext.SaveChangesAsync();
        }
    }
}

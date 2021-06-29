using Cosmetics.BLL.DTO.CatalogModelDTO;
using Cosmetics.BLL.Services.Catalog;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace Cosmetics.Web.ViewComponents
{
    public class CategoriesViewComponent: ViewComponent
    {
        private readonly ICategoryService _categoryService;
        private readonly IMemoryCache _memoryCache;

        private const string cacheCategoriesKey = "CategoriesCache";

        public CategoriesViewComponent(ICategoryService categoryService,
            IMemoryCache memoryCache)
        {
            _categoryService = categoryService;
            _memoryCache = memoryCache;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            //IEnumerable<CategoryDTO> categories = GetCategoriesFromCache();
            string culture = CultureInfo.CurrentCulture.TwoLetterISOLanguageName;
            FilterCategoryDTO filter = new FilterCategoryDTO();
            filter.LanguageCulture = culture;
            IEnumerable<CategoryDTO> categories = _categoryService.GetAllPublishCategory(filter).Where(p => p.ParentCategoryId == null);
            return View(await Task.FromResult(categories));
        }

        private IEnumerable<CategoryDTO> GetCategoriesFromCache()
        {
            IEnumerable<CategoryDTO> categoryDTOs;
            string culture = CultureInfo.CurrentCulture.TwoLetterISOLanguageName;

            if (!_memoryCache.TryGetValue(cacheCategoriesKey, out categoryDTOs))
            {
                FilterCategoryDTO filter = new FilterCategoryDTO();
                filter.LanguageCulture = culture;
                // Key not in cache, so get data.
                categoryDTOs = _categoryService.GetAllPublishCategory(filter).Where(p => p.ParentCategoryId == null);

                // Set cache options.
                var cacheEntryOptions = new MemoryCacheEntryOptions()
                    // Keep in cache for this time, reset time if accessed.
                    .SetSlidingExpiration(TimeSpan.FromHours(3));
                // Save data in cache.
                _memoryCache.Set(cacheCategoriesKey, categoryDTOs, cacheEntryOptions);
            }
            return categoryDTOs;
        }
    }
}

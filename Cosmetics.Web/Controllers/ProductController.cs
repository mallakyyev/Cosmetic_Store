using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Cosmetics.BLL.DTO.CatalogModelDTO;
using Cosmetics.BLL.DTO.ProductModelDTO;
using Cosmetics.BLL.Services.Catalog;
using Cosmetics.BLL.Services.Product;
using Microsoft.AspNetCore.Mvc;

namespace Cosmetics.Web.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;

        public ProductController(IProductService productService, ICategoryService categoryService)
        {
            _productService = productService;
            _categoryService = categoryService;
        }

        public IActionResult Index(int categoryId, int page = 1)
        {
            ViewBag.CategoryId = categoryId;
            string culture = CultureInfo.CurrentCulture.TwoLetterISOLanguageName;

            FilterCategoryDTO filterCategory = new FilterCategoryDTO();
            filterCategory.LanguageCulture = culture;
            filterCategory.ParentId = categoryId;
            ViewBag.Categories = _categoryService.GetAllPublishParentCategories(filterCategory);

            FilterProductDTO filter = new FilterProductDTO();
            filter.CategoryId = categoryId;
            filter.LanguageCulture = culture;
            filter.page = page;
            var products = _productService.GetAllProductsCategory(filter);

            return View(products);
        }

        [HttpGet]
        public IActionResult DetailProduct(int productId)
        {
            string culture = CultureInfo.CurrentCulture.TwoLetterISOLanguageName;
            ViewBag.ProductPictures = _productService.GetProductPictures(productId);

            FilterProductDTO filter = new FilterProductDTO();
            filter.LanguageCulture = culture;

            var product = _productService.GetPublishProductId(productId, filter);

            return View(product.Result);
        }

        [HttpGet]
        public IActionResult SearchProduct(string title)
        {
            string culture = CultureInfo.CurrentCulture.TwoLetterISOLanguageName;
            FilterProductDTO filter = new FilterProductDTO();
            filter.LanguageCulture = culture;

            var product = _productService.GetSearchProducts(title, filter);
            return View(product);
        }
    }
}
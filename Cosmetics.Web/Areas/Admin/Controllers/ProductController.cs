using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cosmetics.BLL.DTO.ProductModelDTO;
using Cosmetics.BLL.Services.Catalog;
using Cosmetics.BLL.Services.Language;
using Cosmetics.BLL.Services.Manufacture;
using Cosmetics.BLL.Services.Product;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Cosmetics.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    //[Authorize(Roles = "Admin")]
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly ILanguageService _languageService;
        private readonly ICategoryService _categoryService;
        private readonly IManufactureService _manufactureService;

        public ProductController(IProductService productService, ILanguageService languageService, ICategoryService categoryService,
            IManufactureService manufactureService)
        {
            _productService = productService;
            _languageService = languageService;
            _categoryService = categoryService;
            _manufactureService = manufactureService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Languages = _languageService.GetAllPublishLanguage().OrderBy(o => o.DisplayOrder);
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateProductDTO value)
        {
            if (ModelState.IsValid)
            {
                await _productService.CreateProduct(value);
                return RedirectToAction("Index");
            }
            ViewBag.Languages = _languageService.GetAllPublishLanguage().OrderBy(o => o.DisplayOrder);
            return View(value);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var category = await _productService.GetProductId(id);

            ViewBag.Languages = _languageService.GetAllPublishLanguage().OrderBy(o => o.DisplayOrder);

            return View(category);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditProductDTO value)
        {
            if (ModelState.IsValid)
            {
                await _productService.EditProduct(value);
                return RedirectToAction("Index");
            }
            ViewBag.Languages = _languageService.GetAllPublishLanguage().OrderBy(o => o.DisplayOrder);

            return View(value);
        }
    }
}
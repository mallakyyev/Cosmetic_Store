using Cosmetics.BLL.DTO.ProductModelDTO;
using Cosmetics.BLL.Services.Product;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace Cosmetics.Web.ViewComponents
{
    public class ShowOnHomeProductsViewComponent : ViewComponent
    {
        private readonly IProductService _productService;

        public ShowOnHomeProductsViewComponent(IProductService productService)
        {
            _productService = productService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            string culture = CultureInfo.CurrentCulture.TwoLetterISOLanguageName;
            FilterProductDTO filter = new FilterProductDTO();
            filter.LanguageCulture = culture;
            IEnumerable<ProductDTO> products = _productService.GetAllProductInShowOnHomaPage(filter);
            return View(await Task.FromResult(products));
        }
    }
}

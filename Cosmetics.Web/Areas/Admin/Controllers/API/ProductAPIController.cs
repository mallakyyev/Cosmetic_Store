using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cosmetics.BLL.DTO.ProductModelDTO;
using Cosmetics.BLL.Services.Product;
using Cosmetics.Web.Binder;
using DevExtreme.AspNet.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Cosmetics.Web.Areas.Admin.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductAPIController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductAPIController(IProductService productService)
        {
            _productService = productService;
        }

        // GET: api/Product
        [HttpGet]
        public object Get(DataSourceLoadOptions loadOptions)
        {
            return DataSourceLoader.Load<ProductDTO>(_productService.GetAllProduct(), loadOptions);
        }

        // Post: api/Product/GetAllProductsCategory
        [HttpPost("GetAllProductsCategory")]
        public IActionResult GetAllProductsCategory(FilterProductDTO filterProductDTO)
        {
            var products = _productService.GetAllProductsCategory(filterProductDTO);
            return Ok(products);
        }

        // Get: api/Product/SearchProducts
        [HttpPost("SearchProducts/{value}")]
        public IActionResult SearchProducts(string value, [FromBody] FilterProductDTO filterProductDTO)
        {
            var products = _productService.GetSearchProducts(value, filterProductDTO);
            return Ok(products);
        }

        // GET: api/Product/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            EditProductDTO editProductDTO = await _productService.GetProductId(id);
            return Ok(editProductDTO);
        }

        // GET: api/Product/GetPublishProduct/5
        [HttpPost("GetPublishProduct/{id}")]
        public async Task<IActionResult> GetPublishProduct(int id, [FromBody] FilterProductDTO filterProductDTO)
        {
            ProductDTO productDTO = await _productService.GetPublishProductId(id, filterProductDTO);
            return Ok(productDTO);
        }

        // POST: api/Product
        [HttpPost]
        public async Task<IActionResult> Post([FromForm] CreateProductDTO value)
        {
            if (ModelState.IsValid)
            {
                await _productService.CreateProduct(value);
                return Ok(value);
            }
            return BadRequest();
        }

        // PUT: api/Product/5
        [HttpPut]
        public async Task<IActionResult> Put([FromForm] EditProductDTO value)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _productService.EditProduct(value);

            return Ok(value);
        }

        // DELETE: api/Product/5
        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            await _productService.RemoveProduct(id);
        }

        [HttpGet("GetProductPicturesId/{id}")]
        public async Task<IActionResult> GetProductPicturesId(int id)
        {
            var productPictureDTO = _productService.GetProductPictures(id);
            return Ok(productPictureDTO);
        }

        [HttpDelete("RemoveImageId/{id}")]
        public async Task RemoveImageId(int id)
        {
            await _productService.RemoveImageProduct(id);
        }

        // Get: api/Product/GetAllProductInShowOnHomaPage
        [HttpPost("GetAllProductInShowOnHomaPage")]
        public IActionResult GetAllProductInShowOnHomaPage(FilterProductDTO filterProductDTO)
        {
            var products = _productService.GetAllProductInShowOnHomaPage(filterProductDTO);
            return Ok(products);
        }

        // Get: api/Product/GetAllProductAsNew
        [HttpPost("GetAllProductAsNew")]
        public IActionResult GetAllProductAsNew(FilterProductDTO filterProductDTO)
        {
            var products = _productService.GetAllProductAsNew(filterProductDTO);
            return Ok(products);
        }
    }
}

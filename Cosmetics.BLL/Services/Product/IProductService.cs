using Cosmetics.BLL.DTO.ProductModelDTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Cosmetics.BLL.Services.Product
{
    public interface IProductService
    {
        IEnumerable<ProductDTO> GetAllProduct();

        Task CreateProduct(CreateProductDTO modelDTO);

        Task EditProduct(EditProductDTO modelDTO);

        Task RemoveProduct(int id);

        Task RemoveImageProduct(int id);

        Task<EditProductDTO> GetProductId(int id);

        Task<ProductDTO> GetPublishProductId(int id, FilterProductDTO filterProductDTO);

        ProductViewModelDTO GetAllProductsCategory(FilterProductDTO filterProductDTO);

        IEnumerable<ProductDTO> GetSearchProducts(string value, FilterProductDTO filterProductDTO);

        IEnumerable<ProductPictureDTO> GetProductPictures(int idProduct);

        IEnumerable<ProductDTO> GetAllProductInShowOnHomaPage(FilterProductDTO filterProductDTO);

        IEnumerable<ProductDTO> GetAllProductAsNew(FilterProductDTO filterProductDTO);
    }
}

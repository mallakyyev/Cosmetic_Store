using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Cosmetics.BLL.DTO.ProductModelDTO;
using Cosmetics.DAL.Data;
using Cosmetics.DAL.Models;
using Cosmetics.DAL.Models.Catalog;
using LazZiya.ImageResize;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using product = Cosmetics.DAL.Models.Catalog;

namespace Cosmetics.BLL.Services.Product
{
    public class ProductService : IProductService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _appEnvironment;

        public ProductService(ApplicationDbContext dbContext, IMapper mapper, IWebHostEnvironment appEnvironment)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _appEnvironment = appEnvironment;
        }

        public async Task CreateProduct(CreateProductDTO modelDTO)
        {
            List<CategoryAndProduct> categoryAndProducts = new List<CategoryAndProduct>();
            List<ProductPicture> productPictures = new List<ProductPicture>();

            product.Product prd = _mapper.Map<product.Product>(modelDTO);
            foreach (int catId in modelDTO.CategoriesId)
            {
                categoryAndProducts.Add(new CategoryAndProduct()
                {
                    CategoryId = catId,
                    ProductId = prd.Id
                });
            }
            prd.CategoryAndProducts = categoryAndProducts;

            if (modelDTO.FormFile != null)
            {
                string fileName = await UploadImage(modelDTO.FormFile);
                prd.PictureName = fileName;
            }
            if (modelDTO.FormFiles != null)
            {
                foreach (var file in modelDTO.FormFiles)
                {
                    string fileName = await UploadImage(file);
                    productPictures.Add(new ProductPicture()
                    {
                        ProductId = prd.Id,
                        PictureName = fileName
                    });
                }
                prd.ProductPictures = productPictures;
            }

            await _dbContext.Products.AddAsync(prd);
            await _dbContext.SaveChangesAsync();
        }

        public async Task EditProduct(EditProductDTO modelDTO)
        {
            List<ProductPicture> productPictures = new List<ProductPicture>();

            var product = await _dbContext.Products.Include(p => p.CategoryAndProducts)
                .Include(p => p.ProductTranslates)
                .FirstOrDefaultAsync(p => p.Id == modelDTO.Id);

            product.ProductTranslates.Clear();
            product.CategoryAndProducts.Clear();

            product.ProductTranslates = modelDTO.ProductTranslates
                .Select(p => new ProductTranslate
                {
                    LanguageCulture = p.LanguageCulture,
                    ProductId = p.ProductId,
                    ProductName = p.ProductName,
                    FullDescription = p.FullDescription,
                    MetaTitle = p.MetaTitle,
                    MetaDescription = p.MetaDescription,
                    MetaKeywords = p.MetaKeywords
                }).ToList();
            product.CategoryAndProducts = modelDTO.CategoriesId
                .Select(p => new CategoryAndProduct
                {
                    ProductId = product.Id,
                    CategoryId = p
                }).ToList();

            product.ProductCode = modelDTO.ProductCode;
            product.ManufactureId = modelDTO.ManufactureId;
            product.Price = modelDTO.Price;
            product.DeliveryRate = modelDTO.DeliveryRate;
            product.ShowOnHomePage = modelDTO.ShowOnHomePage;
            product.Published = modelDTO.Published;
            product.ProductAsNew = modelDTO.ProductAsNew;
            product.NewStartDate = modelDTO.NewStartDate;
            product.EndStartDate = modelDTO.EndStartDate;
            product.QuantityInSet = modelDTO.QuantityInSet;

            if (modelDTO.FormFile != null)
            {
                DeleteImageLarge(product.PictureName);
                DeleteImageMedium(product.PictureName);
                DeleteImageSmall(product.PictureName);

                string fileName = await UploadImage(modelDTO.FormFile);
                product.PictureName = fileName;
            }
            if (modelDTO.FormFiles != null)
            {
                foreach (var file in modelDTO.FormFiles)
                {
                    string fileName = await UploadImage(file);
                    productPictures.Add(new ProductPicture()
                    {
                        ProductId = product.Id,
                        PictureName = fileName
                    });
                }
                product.ProductPictures = productPictures;
            }

            _dbContext.Update(product);
            await _dbContext.SaveChangesAsync();
        }

        public IEnumerable<ProductDTO> GetAllProduct()
        {
            string culture = CultureInfo.CurrentCulture.TwoLetterISOLanguageName;
            var products = _dbContext.Products;
            var result = _dbContext.ProductTranslates
                .Where(p => p.LanguageCulture == culture).Join(products, p => p.ProductId, k => k.Id,
                (p, k) => new ProductDTO
                {
                    Id = k.Id,
                    ProductName = p.ProductName,
                    FullDescription = p.FullDescription,
                    MetaTitle = p.MetaTitle,
                    MetaKeywords = p.MetaKeywords,
                    MetaDescription = p.MetaDescription,
                    ProductCode = k.ProductCode,
                    Price = k.Price,
                    Published = k.Published,
                    PictureName = k.PictureName,
                    DeliveryRate = k.DeliveryRate
                });
            //var productDTOs = _mapper.Map<IEnumerable<ProductTranslate>, IEnumerable<ProductDTO>>(GetList());
            return result;
        }

        public ProductViewModelDTO GetAllProductsCategory(FilterProductDTO filter)
        {
            var translates = _dbContext.ProductTranslates.Where(p => p.LanguageCulture == filter.LanguageCulture);

            var products = _dbContext.Products
                .Include(p => p.CategoryAndProducts)
                .Where(p => p.CategoryAndProducts.Any(p => p.CategoryId == filter.CategoryId) && p.Published);

            var result = products.Join(translates, product => product.Id,
                                                  translate => translate.ProductId,
                                                  (product, translate) =>
                                                  new ProductDTO
                                                  {
                                                      Id = product.Id,
                                                      ProductName = translate.ProductName,
                                                      FullDescription = translate.FullDescription,
                                                      Price = product.Price,
                                                      ProductAsNew = product.ProductAsNew,
                                                      ProductCode = product.ProductCode,
                                                      PictureName = product.PictureName,
                                                      DeliveryRate = product.DeliveryRate
                                                  }).Skip((filter.page - 1) * filter.pageSize).Take(filter.pageSize);

            PageViewModelDTO pageView = new PageViewModelDTO(products.Count(), filter.page, filter.pageSize);
            ProductViewModelDTO viewModelDTO = new ProductViewModelDTO
            {
                ListProductDTO = result,
                PageViewModel = pageView
            };

            return viewModelDTO;
        }

        public async Task<EditProductDTO> GetProductId(int id)
        {
            product.Product prd = await _dbContext.Products
                .Include(i => i.ProductTranslates)
                .Include(i => i.CategoryAndProducts)
                .SingleOrDefaultAsync(s => s.Id == id);
            EditProductDTO productDTO = _mapper.Map<EditProductDTO>(prd);
            return productDTO;
        }

        public async Task<ProductDTO> GetPublishProductId(int id, FilterProductDTO filterProductDTO)
        {
            var prd = await _dbContext.Products.Include(i => i.ProductTranslates).Include(i => i.Manufacture).SingleOrDefaultAsync(s => s.Id == id &&
            s.Published == true);
            if (prd != null)
            {
                var translates = prd.ProductTranslates.SingleOrDefault(s => s.LanguageCulture == filterProductDTO.LanguageCulture);

                ProductDTO productDTO = new ProductDTO();
                productDTO.Id = prd.Id;
                productDTO.MetaTitle = translates.MetaTitle;
                productDTO.MetaKeywords = translates.MetaKeywords;
                productDTO.MetaDescription = translates.MetaDescription;
                productDTO.ProductName = translates.ProductName;
                productDTO.FullDescription = translates.FullDescription;
                productDTO.Price = prd.Price;
                productDTO.ManufactureId = prd.ManufactureId;
                productDTO.NameManufacture = prd.Manufacture.Name;
                productDTO.QuantityInSet = prd.QuantityInSet;
                productDTO.ProductCode = prd.ProductCode;
                productDTO.PictureName = prd.PictureName;
                productDTO.DeliveryRate = prd.DeliveryRate;

                return productDTO;
            }
            else
            {
                return null;
            }
        }

        public IEnumerable<ProductDTO> GetSearchProducts(string value, FilterProductDTO filterProductDTO)
        {
            var products = _dbContext.Products.Where(p => p.Published == true);
            var result = _dbContext.ProductTranslates
                .Where(p => p.LanguageCulture == filterProductDTO.LanguageCulture && p.ProductName.Contains(value))
                .Skip((filterProductDTO.page - 1) * filterProductDTO.pageSize).Take(filterProductDTO.pageSize)
                .Join(products, p => p.ProductId, k => k.Id,
                (p, k) => new ProductDTO
                {
                    Id = k.Id,
                    ProductName = p.ProductName,
                    FullDescription = p.FullDescription,
                    MetaTitle = p.MetaTitle,
                    MetaKeywords = p.MetaKeywords,
                    MetaDescription = p.MetaDescription,
                    ProductCode = k.ProductCode,
                    Price = k.Price,
                    Published = k.Published,
                    PictureName = k.PictureName,
                    DeliveryRate = k.DeliveryRate
                });

            return result;
        }

        //public IEnumerable<ProductTranslate> GetList()
        //{
        //    var prdTranslates = _dbContext.ProductTranslates.Include(i => i.Product).Where(s => s.LanguageId == 1).ToList();
        //    return prdTranslates;
        //}

        public async Task RemoveProduct(int id)
        {
            product.Product prd = await _dbContext.Products.FindAsync(id);
            var imagesProduct = _dbContext.ProductPictures.Where(p => p.ProductId == id);
            if (!string.IsNullOrEmpty(prd.PictureName))
            {
                DeleteImageLarge(prd.PictureName);
                DeleteImageMedium(prd.PictureName);
                DeleteImageSmall(prd.PictureName);
            }
            foreach (var img in imagesProduct)
            {
                DeleteImageLarge(img.PictureName);
                DeleteImageMedium(img.PictureName);
                DeleteImageSmall(img.PictureName);
            }
            _dbContext.Products.Remove(prd);
            await _dbContext.SaveChangesAsync();
        }

        public IEnumerable<ProductPictureDTO> GetProductPictures(int idProduct)
        {
            var productPictures = _dbContext.ProductPictures.Where(p => p.ProductId == idProduct)
                .Select(s => new ProductPictureDTO
                {
                    Id = s.Id,
                    PictureName = s.PictureName,
                    ProductId = s.ProductId
                });

            return productPictures;
        }

        public async Task RemoveImageProduct(int id)
        {
            var imageProduct = await _dbContext.ProductPictures.FindAsync(id);
            if (imageProduct != null)
            {
                DeleteImageLarge(imageProduct.PictureName);
                DeleteImageMedium(imageProduct.PictureName);
                DeleteImageSmall(imageProduct.PictureName);

                _dbContext.ProductPictures.Remove(imageProduct);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task<string> UploadImage(IFormFile formFile)
        {

            var fileName = Guid.NewGuid().ToString().Replace("-", "") + Path.GetExtension(formFile.FileName);

            string pathL = "/images/products/L/";
            string pathM = "/images/products/M/";
            string pathS = "/images/products/S/";

            // Uncomment to save the file
            if (!Directory.Exists(pathL))
            {
                Directory.CreateDirectory(_appEnvironment.WebRootPath + pathL);
            }

            using (var fileStream = new FileStream(_appEnvironment.WebRootPath + pathL + fileName, FileMode.Create))
            {
                await formFile.CopyToAsync(fileStream);
            }

            // Uncomment to save the file
            if (!Directory.Exists(pathM))
            {
                Directory.CreateDirectory(_appEnvironment.WebRootPath + pathM);
            }

            try
            {

                using (var img = Image.FromFile(@"wwwroot\images\products\L\" + fileName))
                {
                    var scale = ImageResize.ScaleAndCrop(img, 300, 300, TargetSpot.Center);
                    scale.SaveAs(_appEnvironment.WebRootPath + pathM + fileName);
                }
            }
            catch (Exception e)
            {
                var error = e;
            }

            // Uncomment to save the file
            if (!Directory.Exists(pathS))
            {
                Directory.CreateDirectory(_appEnvironment.WebRootPath + pathS);
            }

            try
            {

                using (var img = Image.FromFile(@"wwwroot\images\products\L\" + fileName))
                {
                    var scale = ImageResize.ScaleAndCrop(img, 77, 77, TargetSpot.Center);
                    scale.SaveAs(_appEnvironment.WebRootPath + pathS + fileName);
                }
            }
            catch (Exception e)
            {
                var error = e;
            }

            return fileName;
        }

        public bool DeleteImageLarge(string pictureName)
        {
            string path = _appEnvironment.WebRootPath + "/images/products/L/" + pictureName;

            if (!File.Exists(path)) return false;

            try
            {
                File.Delete(path);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool DeleteImageMedium(string pictureName)
        {
            string pathM = _appEnvironment.WebRootPath + "/images/products/M/" + pictureName;

            if (!File.Exists(pathM)) return false;

            try
            {
                File.Delete(pathM);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool DeleteImageSmall(string pictureName)
        {
            string pathS = _appEnvironment.WebRootPath + "/images/products/S/" + pictureName;

            if (!File.Exists(pathS)) return false;

            try
            {
                File.Delete(pathS);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public IEnumerable<ProductDTO> GetAllProductInShowOnHomaPage(FilterProductDTO filterProductDTO)
        {
            var products = _dbContext.Products.Where(p => p.Published == true && p.ShowOnHomePage == true);
            var result = _dbContext.ProductTranslates
                .Where(p => p.LanguageCulture == filterProductDTO.LanguageCulture).Join(products, p => p.ProductId, k => k.Id,
                (p, k) => new ProductDTO
                {
                    Id = k.Id,
                    ProductName = p.ProductName,
                    FullDescription = p.FullDescription,
                    MetaTitle = p.MetaTitle,
                    MetaKeywords = p.MetaKeywords,
                    MetaDescription = p.MetaDescription,
                    ProductCode = k.ProductCode,
                    Price = k.Price,
                    Published = k.Published,
                    PictureName = k.PictureName,
                    DeliveryRate = k.DeliveryRate
                });

            return result;
        }

        public IEnumerable<ProductDTO> GetAllProductAsNew(FilterProductDTO filterProductDTO)
        {
            var products = _dbContext.Products.Where(p => p.Published == true &&
            p.ProductAsNew == true && DateTime.Now >= p.NewStartDate && DateTime.Now <= p.EndStartDate);
            var result = _dbContext.ProductTranslates
                .Where(p => p.LanguageCulture == filterProductDTO.LanguageCulture).Join(products, p => p.ProductId, k => k.Id,
                (p, k) => new ProductDTO
                {
                    Id = k.Id,
                    ProductName = p.ProductName,
                    FullDescription = p.FullDescription,
                    MetaTitle = p.MetaTitle,
                    MetaKeywords = p.MetaKeywords,
                    MetaDescription = p.MetaDescription,
                    ProductCode = k.ProductCode,
                    Price = k.Price,
                    Published = k.Published,
                    PictureName = k.PictureName,
                    DeliveryRate = k.DeliveryRate
                });

            return result;
        }
    }
}

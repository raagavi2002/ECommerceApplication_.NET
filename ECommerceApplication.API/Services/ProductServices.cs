using ECommerce.Application.DTOs;
using ECommerce.Application.Interfaces;
using ECommerceApplication.Exceptions.Exceptions;
using ECommerceApplication.ExceptionsAndResults.Result;

namespace ECommerce.Application.Services
{
    public class ProductServices
    {
       private IProductRepository productRepository { get; set; }

       private ICategoryRepository categoryRepository { get; set; }
       public ProductServices (IProductRepository ProductRepository, ICategoryRepository categoryRepository)
       {
            this.productRepository = ProductRepository;
            this.categoryRepository = categoryRepository;
       }

        public async Task<List<ProductListingDto>> GetAllProductsForHomePageAsync()
        {
            var productDetails = await productRepository.GetAllProductsForHomePageAsync();
            if (productDetails == null)
            {
                throw new CustomException("productDetails", "No products are currently available to display");
            }

            return productDetails;
        }

        public async Task<List<ProductListingDto>> GetProductsBasedOnCategoryAsync(int categoryId)
        {
            bool isValidCategory = this.categoryRepository.IsCategoryExists(categoryId);
            if(!isValidCategory)
            {
                throw new CustomException("Not a valid category", "categoryId");
            }

            var productsBasedOnCategory = await this.productRepository.GetProductsBasedOnCategoryAsync(categoryId);
            if (productsBasedOnCategory == null)
            {
                throw new CustomException("No products found in this category", "categoryId");
            }
            return productsBasedOnCategory;
        }

        public async Task<CustomResult> AddProductsForSellingAsync(List<ProductListingDto> products)
        {
            var result = await this.productRepository.AddProductsForSellingAsync(products);
            return result;
        }

        public async Task<CustomResult> UpdateProductsDetailsAsync(int productId, ProductListingDto updateDetails)
        {
            if(updateDetails == null)
            {
                throw new CustomException("Update Details are empty", "updateDetails");
            }

            var updatedResult = await this.productRepository.UpdateProductsDetailsAsync(productId, updateDetails);
            if (!updatedResult.IsSuccess)
            {
                throw new CustomException(updatedResult.Message, nameof(updateDetails));
            }
            return updatedResult;
        }

        public async Task<CustomResult> DeleteProductDetailsAsync(int productId)
        {
            var isProductDeleted = await this.productRepository.DeleteProductDetailsAsync(productId);
            if (!isProductDeleted.IsSuccess)
            {
                throw new CustomException(isProductDeleted.Message, nameof(isProductDeleted));
            }
            return isProductDeleted;
        }
    }
}

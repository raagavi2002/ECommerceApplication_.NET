using ECommerce.Application.DTOs;
using ECommerceApplication.ExceptionsAndResults.Result;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Application.Interfaces
{
    public interface IProductServices
    {
        Task<List<ProductListingDto>> GetAllProductsForHomePageAsync();
        Task<List<ProductListingDto>> GetProductsBasedOnCategoryAsync(int categoryId);
        Task<CustomResult> AddProductsForSellingAsync(List<ProductListingDto> products);
        Task<CustomResult> UpdateProductsDetailsAsync(int productId, ProductListingDto updateDetails);
        Task<CustomResult> DeleteProductDetailsAsync(int productId);
    }
}

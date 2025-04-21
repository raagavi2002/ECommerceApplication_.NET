using ECommerce.Application.DTOs;
using ECommerceApplication.ExceptionsAndResults.Result;
using ECommerceApplication.Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Application.Interfaces
{
    public interface IProductRepository
    {
        Task<List<ProductListingDto>> GetAllProductsForHomePageAsync();
        Task<List<ProductListingDto>> GetProductsBasedOnCategoryAsync(int categoryId);
        Task<CustomResult> AddProductsForSellingAsync(List<ProductListingDto> products);
        Task<CustomResult> UpdateProductsDetailsAsync(int productId, ProductListingDto updateDetails);
        Task<CustomResult> DeleteProductDetailsAsync(int productId);
    }
}

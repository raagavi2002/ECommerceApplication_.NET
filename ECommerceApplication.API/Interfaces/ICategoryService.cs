using ECommerce.Application.DTOs;
using ECommerceApplication.ExceptionsAndResults.Result;

namespace ECommerce.Application.Interfaces
{
    public interface ICategoryService
    {
        Task<CustomResult> GetCategoryListAsync();
        Task<CustomResult> GetCategoryBasedOnId(int categoryId);
        Task<CustomResult> AddCategoryAsync(CategoryDetailsDto categoryDetails);
        Task<CustomResult> DeleteCategoryAsync(int categoryId);
        Task<CustomResult> UpdateCategoryAsync(CategoryDetailsDto categoryDetails);
    }
}

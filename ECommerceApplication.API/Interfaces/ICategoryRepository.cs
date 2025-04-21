using ECommerce.Application.DTOs;
using ECommerceApplication.ExceptionsAndResults.Result;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Application.Interfaces
{
    public interface ICategoryRepository
    {
        bool IsCategoryExists(int id);
        Task<CustomResult> GetCategoryListAsync();
        Task<CustomResult> GetCategoryBasedOnId(int categoryId);
        Task<CustomResult> AddCategoryAsync(CategoryDetailsDto categoryDetails);
        Task<CustomResult> DeleteCategoryAsync(int categoryId);
        Task<CustomResult> UpdateCategoryAsync(CategoryDetailsDto categoryDetails);
    }
}

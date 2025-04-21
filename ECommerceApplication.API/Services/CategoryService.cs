using ECommerce.Application.DTOs;
using ECommerce.Application.Interfaces;
using ECommerceApplication.Exceptions.Exceptions;
using ECommerceApplication.ExceptionsAndResults.Result;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Application.Services
{
    public class CategoryService : ICategoryService
    {
        private ICategoryRepository CategoryRepository;
        public CategoryService(ICategoryRepository categoryRepository)
        {
            this.CategoryRepository = categoryRepository;
        }

       public async Task<CustomResult> GetCategoryListAsync()
       {
          var categoryLists = await this.CategoryRepository.GetCategoryListAsync();
          if (!categoryLists.IsSuccess)
          {
             throw new CustomException(categoryLists.Message, nameof(categoryLists));
          }
          return categoryLists;
       }

       public async Task<CustomResult> GetCategoryBasedOnId(int categoryId)
       {
            if (categoryId < 0)
            {
                throw new CustomException("CategoryId Cannot be null", "categoryId");
            }

            var categoryDetails = await this.CategoryRepository.GetCategoryBasedOnId(categoryId);
            if (!categoryDetails.IsSuccess)
            {
                throw new CustomException("Category does not exists", "categoryId");
            }
            return categoryDetails;
       }

        public async Task<CustomResult> AddCategoryAsync(CategoryDetailsDto categoryDetails)
        {
            if (string.IsNullOrEmpty(categoryDetails.Name) || string.IsNullOrWhiteSpace(categoryDetails.Name))
            {
                throw new CustomException("Category Name cannot be null or empty or contain whitespaces", "categoryName");
            }

            var result = await this.CategoryRepository.AddCategoryAsync(categoryDetails);
            return result;
        }

        public async Task<CustomResult> DeleteCategoryAsync(int categoryId)
        {
            if (categoryId < 0)
            {
                throw new CustomException("CategoryId Cannot be null", "categoryId");
            }
            var categoryDetails = await this.CategoryRepository.DeleteCategoryAsync(categoryId);
            if (!categoryDetails.IsSuccess)
            {
                throw new CustomException(categoryDetails.Message, nameof(categoryDetails));
            }
            return categoryDetails;
        }
        public async Task<CustomResult> UpdateCategoryAsync(CategoryDetailsDto categoryDetails)
        {
            if(categoryDetails == null)
            {
                throw new CustomException("Category Details Cannot be null", "categoryDetails");
            }

            if (string.IsNullOrEmpty(categoryDetails.Name) || string.IsNullOrWhiteSpace(categoryDetails.Name))
            {
                throw new CustomException("Category Name cannot be null or empty or contain whitespaces", "categoryName");
            }

            var updatedCategory = await this.CategoryRepository.UpdateCategoryAsync(categoryDetails);
            if (!updatedCategory.IsSuccess)
            {
                throw new CustomException(updatedCategory.Message, nameof(updatedCategory));    
            }
            return updatedCategory;
        }
    }
}

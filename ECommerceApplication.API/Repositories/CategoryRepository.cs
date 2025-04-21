using ECommerce.Application.DTOs;
using ECommerce.Application.Interfaces;
using ECommerceApplication.ExceptionsAndResults.Result;
using ECommerceApplication.Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Application.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private ApplicationDBContext ApplicationDBContext { get; set; }

        public CategoryRepository(ApplicationDBContext applicationDBContext)
        {
            this.ApplicationDBContext = applicationDBContext;
        }

        public bool IsCategoryExists(int categoryId)
        {
            return this.ApplicationDBContext.Categories.Any(i => i.Id == categoryId && i.Isactive`q 2w);
        }

        public async Task<CustomResult> GetCategoryListAsync()
        {
            var categoryDetails = await this.ApplicationDBContext.Categories.Select(i => new CategoryDetailsDto
            {
                Id = i.Id,
                Name = i.Name,
            }).ToListAsync();

            if (!categoryDetails.Any())
            {
                return new CustomResult() { IsSuccess = false, Message = "Categories Not Found" };
            }

            return new CustomResult () { IsSuccess =  true, Message = "Category listed", ResultDetails = categoryDetails };
        }

        public async Task<CustomResult> GetCategoryBasedOnId(int categoryId)
        {
            var categoryDetails = await this.ApplicationDBContext.Categories.Where(i => i.Id == categoryId && i.Isactive).Select(i => new CategoryDetailsDto
            {
                Id = i.Id,
                Name = i.Name,
            }).FirstOrDefaultAsync();

            if (categoryDetails == null)
            {
                return new CustomResult { IsSuccess = false, Message = "Category does not exists" };
            }

            return new CustomResult { IsSuccess = true, Message = "Category deatils fetched", ResultDetails = categoryDetails };
        }

        public async Task<CustomResult> AddCategoryAsync(CategoryDetailsDto categoryDetails)
        {
            var newCategory = new Category()
            {
                Name = categoryDetails.Name,
                Isactive = true,
            };
            await this.ApplicationDBContext.Categories.AddRangeAsync(newCategory);
            await this.ApplicationDBContext.SaveChangesAsync();
            return new CustomResult() { IsSuccess = true, Message = "Category has been sucessfully added" };
        }

        public async Task<CustomResult> DeleteCategoryAsync(int categoryId)
        {
            var category = await this.ApplicationDBContext.Categories.FirstOrDefaultAsync(i => i.Id == categoryId && i.Isactive);
            if (category == null)
            {
                return new CustomResult() { IsSuccess = false, Message = "Category does not exists" };
            }

            category.Isactive = false;
            await this.ApplicationDBContext.SaveChangesAsync();
            return new CustomResult() { IsSuccess = true, Message = "Category has been sucessfully deleted" };
        }

        public async Task<CustomResult> UpdateCategoryAsync(CategoryDetailsDto categoryDetails)
        {
            var category = await this.ApplicationDBContext.Categories.FirstOrDefaultAsync(i => i.Id == categoryDetails.Id && i.Isactive);
            if (category == null)
            {
                return new CustomResult() { IsSuccess = false, Message = "Category does not exists" };
            }
            
            bool isUpdated = false;
            if (category.Name != categoryDetails.Name)
            {
                category.Name = categoryDetails.Name;
                isUpdated = true;
            }

            if(isUpdated)
            {
                await this.ApplicationDBContext.SaveChangesAsync();
                return new CustomResult() { IsSuccess = true, Message = "Category Updated Sucessfully" };
            }
            return new CustomResult() { IsSuccess = false, Message = "No Changes found" };
        }

    }
}

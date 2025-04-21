using ECommerce.Application.DTOs;
using ECommerceApplication.ExceptionsAndResults.Result;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Application.Interfaces
{
    public interface IReviewDetailsService
    {
        Task<CustomResult> GetReviewDetailsBasedOnProductIdAsync(int productId);
        Task<CustomResult> AddReviewsForProductAsync(int productId, ReviewDetailsDto updateReviewDetails);
        Task<CustomResult> DeleteReviewDetailsByIdAsync(int reviewId);
        Task<CustomResult> EditReviewDetailsAsync(int reviewId, ReviewDetailsDto updateDetails);    
    }
}

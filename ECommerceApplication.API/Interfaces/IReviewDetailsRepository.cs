using ECommerce.Application.DTOs;
using ECommerceApplication.ExceptionsAndResults.Result;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Application.Interfaces
{
    public interface IReviewDetailsRepository
    {   
        Task<CustomResult> GetReviewDetailsBasedOnProductId(int productId);
        Task<CustomResult> AddReviewsForProductAsync(int productId, ReviewDetailsDto updateReviewDetails);
        Task<CustomResult> DeleteReviewDetailsByIdAsync(int reviewId);
        Task<CustomResult> EditReviewDetailsAsync(int reviewId, ReviewDetailsDto updateDetails);
    }
}

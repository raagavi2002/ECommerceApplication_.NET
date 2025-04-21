using ECommerce.Application.DTOs;
using ECommerce.Application.Interfaces;
using ECommerceApplication.Exceptions.Exceptions;
using ECommerceApplication.ExceptionsAndResults.Result;

namespace ECommerce.Application.Services
{
    public class ReviewDetailsService : IReviewDetailsService
    {
        private IReviewDetailsRepository reviewDetailsRepository;
        public ReviewDetailsService(IReviewDetailsRepository reviewDetailsRepository) 
        {
            this.reviewDetailsRepository = reviewDetailsRepository;
        }
        public async Task<CustomResult> GetReviewDetailsBasedOnProductIdAsync(int productId)
        {
            var reviewDetails = await this.reviewDetailsRepository.GetReviewDetailsBasedOnProductId(productId);
            return reviewDetails;
        }

        public async Task<CustomResult> AddReviewsForProductAsync(int productId, ReviewDetailsDto updateReviewDetails)
        {
            if (updateReviewDetails == null)
            {
                throw new CustomException("Details cannot be empty", nameof(updateReviewDetails));
            }
            var reviewDetails = await this.reviewDetailsRepository.AddReviewsForProductAsync(productId, updateReviewDetails);
            return reviewDetails;
        }

        public async Task<CustomResult> DeleteReviewDetailsByIdAsync(int reviewId)
        {
            var reviewDetails = await this.reviewDetailsRepository.DeleteReviewDetailsByIdAsync(reviewId);
            if (!reviewDetails.IsSuccess)
            {
                throw new CustomException(reviewDetails.Message, nameof(reviewDetails));
            }
            return reviewDetails;
        }

        public async Task<CustomResult> EditReviewDetailsAsync(int reviewId, ReviewDetailsDto updateDetails)
        {
            if(updateDetails == null)
            {
                throw new CustomException("Update details are empty", "updateDetails");
            }
            
            var reviewDetails = await this.reviewDetailsRepository.EditReviewDetailsAsync(reviewId, updateDetails);
            if(!reviewDetails.IsSuccess)
            {
                throw new CustomException(reviewDetails.Message, nameof (reviewDetails));
            }
            return reviewDetails;
        }
    }
}

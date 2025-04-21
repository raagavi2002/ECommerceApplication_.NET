using ECommerce.Application.DTOs;
using ECommerce.Application.Interfaces;
using ECommerceApplication.ExceptionsAndResults.Result;
using ECommerceApplication.Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Application.Repositories
{
    public class ReviewDetailsRepository : IReviewDetailsRepository
    {
        private ApplicationDBContext ApplicationDBContext { get; set; }
        private IUserRepository UserRepository { get; set; }
        public ReviewDetailsRepository(ApplicationDBContext applicationDBContext, IUserRepository userRepository)
        {
            this.ApplicationDBContext = applicationDBContext;
            this.UserRepository = userRepository;
        }

        public async Task<CustomResult> GetReviewDetailsBasedOnProductId(int productId)
        {
            var rawReviews = await this.ApplicationDBContext.Reviewdetails.AsNoTracking().Where(i => i.Productid == productId && i.Isactive).ToListAsync().ConfigureAwait(false); // Get the raw data first

            var reviews = new List<ReviewDetailsDto>();

            foreach (var review in rawReviews)
            {
                var userName = this.UserRepository.GetUserNameFromUserId(review.Userid);

                reviews.Add(new ReviewDetailsDto
                {
                    Ratings = review.Ratings,
                    Comment = review.Comment,
                    CommentPostedBy = userName,
                    Postedat = review.Postedat,
                });
            }

            return new CustomResult() { IsSuccess = true, Message = "Reviews fetched", ResultDetails = reviews };
        }
        public async Task<CustomResult> AddReviewsForProductAsync(int productId, ReviewDetailsDto updateReviewDetails)
        {
            var result = new Reviewdetail()
            {
                Productid = productId,
                Ratings = updateReviewDetails.Ratings,
                Comment = updateReviewDetails.Comment,
                Isactive = true,
                Postedat = DateTime.Now,
                Userid = "abcd-efgh-ijjkl",
            };

            await this.ApplicationDBContext.Reviewdetails.AddAsync(result);
            await this.ApplicationDBContext.SaveChangesAsync();
            return new CustomResult { IsSuccess = true, Message = "Reviews Added", ResultDetails = result };
        }

        public async Task<CustomResult> DeleteReviewDetailsByIdAsync(int reviewId)
        {
            var result = await this.ApplicationDBContext.Reviewdetails.Where(i => i.Id == reviewId).FirstOrDefaultAsync();
            if (result == null)
            {
                return new CustomResult() { IsSuccess = false, Message = "Review Not Found" };
            }
            result.Isactive = false;
            await this.ApplicationDBContext.SaveChangesAsync();
            return new CustomResult() { IsSuccess = false, Message = "Review Deleted Successfully" };    
        }
        public async Task<CustomResult> EditReviewDetailsAsync(int reviewId, ReviewDetailsDto updateDetails)
        {
            var reviewDetails = await this.ApplicationDBContext.Reviewdetails.Where(i => i.Id == reviewId && i.Isactive).FirstOrDefaultAsync();
            if(reviewDetails == null)
            {
                return new CustomResult() { IsSuccess = false, Message = "Review does not exists" };
            }

            bool isUpdated = false;
            if (reviewDetails.Comment != updateDetails.Comment)
            {
                reviewDetails.Comment = updateDetails.Comment;
                isUpdated = true;
            }

            if (reviewDetails.Ratings != updateDetails.Ratings)
            {
                reviewDetails.Ratings = updateDetails.Ratings;
                isUpdated = true;
            }

            if (isUpdated) 
            {
                reviewDetails.Postedat = DateTime.Now;
                await this.ApplicationDBContext.SaveChangesAsync();
                return new CustomResult() { IsSuccess = true, Message = "Review Updated Sucessfully " };
            }
        
            return new CustomResult() { IsSuccess = false, Message = "No changes found "};  
        }
    }
}

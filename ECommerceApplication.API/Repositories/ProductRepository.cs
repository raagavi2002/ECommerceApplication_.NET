using ECommerce.Application.DTOs;
using ECommerce.Application.Interfaces;
using ECommerceApplication.ExceptionsAndResults.Result;
using ECommerceApplication.Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Application.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private ApplicationDBContext ApplicationDBContext { get; set; }
        private IUserRepository UserRepository { get; set; }
        public ProductRepository(ApplicationDBContext applicationDBContext, IUserRepository userRepository)
        {
            this.ApplicationDBContext = applicationDBContext;
            this.UserRepository = userRepository;
        }
        public async Task<List<ProductListingDto>> GetAllProductsForHomePageAsync() 
        {
            return await (from product in this.ApplicationDBContext.Productdetails.AsNoTracking()
                                  where product.Isactive
                                  select new ProductListingDto
                                  {
                                      ProductId = product.Id,
                                      ProductDescription = product.Description,
                                      ProductPrice = product.Price,
                                      ProductTitle = product.Title,
                                      Averagerating = product.Averagerating,
                                      Reviewdetails = GetReviewDetailsBasedOnProductId(product.Id),
                                  }).ToListAsync().ConfigureAwait(false);
            
        }

        public async Task<List<ProductListingDto>> GetProductsBasedOnCategoryAsync(int categoryId)
        {
           var productDetails = await this.ApplicationDBContext.Productdetails.AsNoTracking().Where(i => i.Categoryid == categoryId).Select(i => new ProductListingDto
           {
               ProductId = i.Id,
               ProductDescription = i.Description,
               ProductPrice= i.Price,
               ProductTitle = i.Title,
               Quantityavailable = i.Quantityavailable,
           }).ToListAsync();
           return productDetails;
        }

        public async Task<CustomResult> AddProductsForSellingAsync(List<ProductListingDto> products)
        {
            var executionStrategy = this.ApplicationDBContext.Database.CreateExecutionStrategy();
            await executionStrategy.Execute(async () =>
            {
                using (var transaction = this.ApplicationDBContext.Database.BeginTransaction())
                {
                    try
                    {
                        foreach (var product in products)
                        {
                            var newProduct = new Productdetail()
                            {
                                Title = product.ProductTitle,
                                Description = product.ProductDescription,
                                Price = product.ProductPrice,
                                Quantityavailable = product.Quantityavailable,
                                Isactive = true,
                                Categoryid = product.Categoryid,
                            };
                            await this.ApplicationDBContext.Productdetails.AddAsync(newProduct);
                        }
                        await transaction.CommitAsync();
                        await this.ApplicationDBContext.SaveChangesAsync();
                        return new CustomResult()
                        {
                            IsSuccess = true,
                            Message = "Products have been added successfully",
                        };
                    }
                    catch (Exception ex)
                    {
                        await transaction.RollbackAsync();
                        return new CustomResult()
                        {
                            IsSuccess = false,
                            Message = ex.Message,
                        };
                    }
                }
            });
            return new CustomResult()
            {
                IsSuccess = false,
                Message = "Products could not be added",
            };
        }

        public async Task<CustomResult> UpdateProductsDetailsAsync(int productId, ProductListingDto updateDetails)
        {
            var productDetails = await this.ApplicationDBContext.Productdetails
                .FirstOrDefaultAsync(i => i.Id == productId);

            if (productDetails == null)
            {
                return new CustomResult() { IsSuccess = false, Message = "Product not found." };
            }

            bool isUpdated = false;

            if (productDetails.Title != updateDetails.ProductTitle)
            {
                productDetails.Title = updateDetails.ProductTitle;
                isUpdated = true;
            }

            if (productDetails.Quantityavailable != updateDetails.Quantityavailable)
            {
                productDetails.Quantityavailable = updateDetails.Quantityavailable;
                isUpdated = true;
            }

            if (productDetails.Price != updateDetails.ProductPrice)
            {
                productDetails.Price = updateDetails.ProductPrice;
                isUpdated = true;
            }

            if (productDetails.Description != updateDetails.ProductDescription)
            {
                productDetails.Description = updateDetails.ProductDescription;
                isUpdated = true;
            }

            if (productDetails.Categoryid != updateDetails.Categoryid)
            {
                productDetails.Categoryid = updateDetails.Categoryid;
                isUpdated = true;
            }

            if (isUpdated)
            {
                await this.ApplicationDBContext.SaveChangesAsync();
                return new CustomResult() { IsSuccess = true, Message = "Product updated successfully." };
            }

            return new CustomResult() { IsSuccess = false, Message = "No changes made to the product." };
        }
        public async Task<CustomResult> DeleteProductDetailsAsync(int productId)
        {
            var productDetails = await this.ApplicationDBContext.Productdetails.FirstOrDefaultAsync(i => i.Id == productId);
            if (productDetails != null)
            {
                productDetails.Isactive = false;
                await this.ApplicationDBContext.SaveChangesAsync();
                return new CustomResult { IsSuccess = true, Message = "Product has been sucessfully deleleted" };
            }
            return new CustomResult { IsSuccess = false, Message = "Product Not Found" };
        }
    }
}

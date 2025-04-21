using ECommerceApplication.Infrastructure.Persistence;

namespace ECommerce.Application.DTOs
{
    public class ProductListingDto
    {
        public int ProductId { get; set; }

        public string ProductTitle { get; set; } = null!;

        public string ProductDescription { get; set; } = null!;

        public double ProductPrice { get; set; }

        public int Quantityavailable { get; set; }

        public int Categoryid { get; set; }

        public double? Averagerating { get; set; }

        public bool Isactive { get; set; }

        public List<ReviewDetailsDto> Reviewdetails { get; set; } = new List<ReviewDetailsDto>();

    }
}

namespace ECommerce.Application.DTOs
{
    public class ReviewDetailsDto
    {
        public int ReviewId { get; set; }

        public int Productid { get; set; }

        public string Userid { get; set; } = null!;

        public string CommentPostedBy { get; set; }

        public double Ratings { get; set; }

        public DateTime Postedat { get; set; }

        public string? Comment { get; set; }

        public bool Isactive { get; set; }
    }
}

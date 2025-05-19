using Baristasyon.Application.Dtos;

namespace Baristasyon.WebUI.Models
{
    public class CoffeeRecipeDetailViewModel
    {
        public ResultCoffeeRecipeDto Recipe { get; set; } = null!;
        public List<ResultReviewDto> Reviews { get; set; } = new();
        public CreateReviewDto NewReview { get; set; } = new();

        public double AverageRating { get; set; }
        public int RatingCount { get; set; }
        public int UserRating { get; set; } // örnek olarak şimdilik sabit atanacak
        public CreateRatingDto NewRating { get; set; } = new();
    }
}

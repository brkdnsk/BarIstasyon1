using Baristasyon.Application.Dtos;

namespace Baristasyonn.WebUI.Models
{
    public class CoffeeRecipeDetailViewModel
    {
        public ResultCoffeeRecipeDto Recipe { get; set; } = null!;
        public List<ResultReviewDto> Reviews { get; set; } = new();
        public CreateReviewDto NewReview { get; set; } = new();
        public double AverageRating { get; set; }   
    }

}

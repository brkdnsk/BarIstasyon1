using Baristasyon.Application.Dtos;

namespace Baristasyon.WebUI.Models
{
    public class CoffeeRecipeDetailViewModel
    {
        // 👇 Tarif bilgisi
        public ResultCoffeeRecipeDto Recipe { get; set; } = null!;

        // 👇 Yorumlar
        public List<ResultReviewDto> Reviews { get; set; } = new();
        public CreateReviewDto NewReview { get; set; } = new();

        // 👇 Puanlama
        public double AverageRating { get; set; }
        public int RatingCount { get; set; }
        public CreateRatingDto NewRating { get; set; } = new();

        // 👇 Favori durumu
        public bool IsFavorite { get; set; }

        // 👇 Kullanıcı bilgisi (şimdilik sabit atanıyor)
        public int CurrentUserId { get; set; }
    }
}

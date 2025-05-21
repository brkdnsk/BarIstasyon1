
    using Baristasyon.Application.Dtos;
    using global::Baristasyon.Application.Dtos;

    namespace Baristasyon.WebUI.Models
    {
        public class UserProfileViewModel
        {
            public List<ResultReviewDto> Comments { get; set; } = new();
            public List<ResultRatingDto> Ratings { get; set; } = new();
        }
    }


using Baristasyon.Application.Dtos;

namespace Baristasyon.WebUI.Models
{
    public class UserProfileViewModel
    {
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;

        

        public List<ResultReviewDto> Reviews { get; set; } = new();
        public List<ResultRatingDto> Ratings { get; set; } = new();
        public List<ResultReviewDto> Comments { get; set; } = new();
       
    }
}



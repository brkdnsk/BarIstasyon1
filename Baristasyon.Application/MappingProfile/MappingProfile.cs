using AutoMapper;
using Baristasyon.Application.Dtos;
using Baristasyon.Domain.Entities;

namespace Baristasyon.Application.MappingProfile
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // ☕ CoffeeRecipe
            CreateMap<CoffeeRecipe, ResultCoffeeRecipeDto>().ReverseMap();
            CreateMap<CreateCoffeeRecipeDto, CoffeeRecipe>();
            CreateMap<UpdateCoffeeRecipeDto, CoffeeRecipe>();
            CreateMap<CoffeeRecipe, ResultCoffeeRecipeDto>();


            // 🛠 Equipment
            CreateMap<Equipment, ResultEquipmentDto>().ReverseMap();
            CreateMap<CreateEquipmentDto, Equipment>();
            CreateMap<UpdateEquipmentDto, Equipment>();

            // 💖 FavoriteRecipe
            CreateMap<FavoriteRecipe, ResultFavoriteRecipeDto>().ReverseMap();
            CreateMap<CreateFavoriteRecipeDto, FavoriteRecipe>();
            CreateMap<UpdateFavoriteRecipeDto, FavoriteRecipe>();

            // 💼 JobPost
            CreateMap<JobPost, ResultJobPostDto>().ReverseMap();
            CreateMap<CreateJobPostDto, JobPost>();
            CreateMap<UpdateJobPostDto, JobPost>();

            // 🧑‍💻 User
            CreateMap<User, ResultUserDto>().ReverseMap();
            CreateMap<RegisterUserDto, User>();
            CreateMap<LoginUserDto, User>();
            CreateMap<UpdateUserProfileDto, User>();
            CreateMap<UpdatePasswordDto, User>();

            // ⭐ Rating
            CreateMap<Rating, ResultRatingDto>().ReverseMap();
            CreateMap<CreateRatingDto, Rating>();

            // 💬 Review
            CreateMap<Review, ResultReviewDto>().ReverseMap();
            CreateMap<CreateReviewDto, Review>();
        }
    }
}

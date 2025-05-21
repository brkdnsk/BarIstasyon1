using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Baristasyon.Application.Dtos
{
    public class ResultRatingDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int CoffeeRecipeId { get; set; }
        public int Score { get; set; }
        public DateTime RatedAt { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Username { get; set; } = null!;
        public string RecipeTitle { get; set; } = null!;
    }
}

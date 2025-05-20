using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Baristasyon.Application.Dtos
{
    public class ResultFavoriteRecipeDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int RecipeTitle { get; set; }
        public int CoffeeRecipeId { get; set; }
        public DateTime AddedAt { get; set; }
    }
}

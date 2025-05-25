using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Baristasyon.Application.Dtos
{
    public class ResultCoffeeRecipeDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string Method { get; set; } = null!;
        public string BrewTime { get; set; } = null!;
        public string ImageUrl { get; set; } = string.Empty;
        public string Ingredients { get; set; } = null!;
        public string? VideoUrl { get; set; }
    }

}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Baristasyon.Domain.Entities
{
    public class CoffeeRecipe
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string Method { get; set; } = null!; // demleme yöntemi
        public int BrewTime { get; set; } // dakika cinsinden
        public string Ingredients { get; set; } = null!;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public string? ImageUrl { get; set; }

    }
}


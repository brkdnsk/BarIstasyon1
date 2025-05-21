using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Baristasyon.Domain.Entities
{
    public class Review
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int CoffeeRecipeId { get; set; }
        public string Comment { get; set; } = null!;
        public DateTime CreatedAt { get; set; }

        // 👇 Bu navigation property'yi ekle
        public User User { get; set; } = null!;
    }


}

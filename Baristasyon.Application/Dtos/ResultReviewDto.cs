using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Baristasyon.Application.Dtos
{
    public class ResultReviewDto
    {
      
            public int Id { get; set; }
            public int UserId { get; set; }
            public int CoffeeRecipeId { get; set; }
            public string Comment { get; set; } = null!;
            public DateTime CreatedAt { get; set; }

            public string Username { get; set; } = null!; // 👈 Yeni ekle
        


    }
}

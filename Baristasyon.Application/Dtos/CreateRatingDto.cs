using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Baristasyon.Application.Dtos
{
    public class CreateRatingDto
    {
        public int UserId { get; set; }
        public int CoffeeRecipeId { get; set; }
        public int Score { get; set; }
    }
}

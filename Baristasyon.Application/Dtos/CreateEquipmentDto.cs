using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Baristasyon.Application.Dtos
{
    public class CreateEquipmentDto
    {
        public int UserId { get; set; }
        public string Name { get; set; } = null!;

        public string Description { get; set; } = null!;
        public int CoffeeRecipeId { get; set; }

        public string ImageUrl { get; set; } = null!;
        public string Usage { get; set; } = null!;
       
    }
}

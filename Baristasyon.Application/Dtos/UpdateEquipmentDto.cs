using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Baristasyon.Application.Dtos
{
    public class UpdateEquipmentDto
    {
        public string Name { get; set; } = null!;

        public string Id { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string ImageUrl { get; set; } = null!;
        public string Usage { get; set; } = null!;
    }
}

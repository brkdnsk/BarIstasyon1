﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Baristasyon.Application.Dtos
{
    public class CreateCoffeeRecipeDto
    {
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string Method { get; set; } = null!;
        public int BrewTime { get; set; }
        public string Ingredients { get; set; } = null!;
        public string ImageUrl { get; set; } = null!;
        public string? VideoUrl { get; set; }
    }

}

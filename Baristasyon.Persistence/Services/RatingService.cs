using Baristasyon.Application.Dtos;
using Baristasyon.Application.Interfaces.Services;
using Baristasyon.Domain.Entities;
using Baristasyon.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Baristasyon.Persistence.Services
{
    public class RatingService : IRatingService
    {
        private readonly BaristasyonDbContext _context;

        public RatingService(BaristasyonDbContext context)
        {
            _context = context;
        }

        public async Task<bool> CreateAsync(CreateRatingDto dto)
        {
            var existing = await _context.Ratings
                .FirstOrDefaultAsync(x => x.UserId == dto.UserId && x.CoffeeRecipeId == dto.CoffeeRecipeId);

            if (existing != null)
            {
                existing.Score = dto.Score;
                existing.CreatedAt = DateTime.UtcNow;
            }
            else
            {
                var rating = new Rating
                {
                    UserId = dto.UserId,
                    CoffeeRecipeId = dto.CoffeeRecipeId,
                    Score = dto.Score
                };
                _context.Ratings.Add(rating);
            }

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<AverageRatingDto> GetAverageAsync(int recipeId)
        {
            var ratings = await _context.Ratings
                .Where(x => x.CoffeeRecipeId == recipeId)
                .ToListAsync();

            if (!ratings.Any())
                return new AverageRatingDto { Average = 0, Count = 0 };

            return new AverageRatingDto
            {
                Average = ratings.Average(x => x.Score),
                Count = ratings.Count
            };
        }
    }

}

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
    public class ReviewService : IReviewService
    {
        private readonly BaristasyonDbContext _context;

        public ReviewService(BaristasyonDbContext context)
        {
            _context = context;
        }

        public async Task<List<ResultReviewDto>> GetByRecipeIdAsync(int recipeId)
        {
            var comments = await _context.Reviews
                .Include(navigationPropertyPath: r => r.User)
                .Where(r => r.CoffeeRecipeId == recipeId)
                .Select(r => new ResultReviewDto
                {
                    Id = r.Id,
                    UserId = r.UserId,
                    CoffeeRecipeId = r.CoffeeRecipeId,
                    Comment = r.Comment,
                    CreatedAt = r.CreatedAt,
                    Username = r.User.Username // 👈 Burada User tablosundan çekiyoruz
                })
                .ToListAsync();

            return comments;
        }

        public async Task<bool> CreateAsync(CreateReviewDto dto)
        {
            var review = new Review
            {   
                UserId = dto.UserId,
                CoffeeRecipeId = dto.CoffeeRecipeId,
                Comment = dto.Comment,
                CreatedAt = DateTime.UtcNow
            };

            _context.Reviews.Add(review);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}

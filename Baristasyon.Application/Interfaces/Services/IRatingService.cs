using Baristasyon.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Baristasyon.Application.Interfaces.Services
{
    public interface IRatingService
    {
        Task<bool> CreateAsync(CreateRatingDto dto);
        Task<AverageRatingDto> GetAverageAsync(int recipeId);
    }

}

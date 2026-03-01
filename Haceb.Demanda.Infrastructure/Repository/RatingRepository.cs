using Haceb.Demanda.Domain.Entities;
using Haceb.Demanda.Domain.Enum;
using Haceb.Demanda.Domain.IRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Haceb.Demanda.Infrastructure.Repository
{
    public class RatingRepository(HacebDbContext _context) : IRatingRepository
    {
        public async Task<RatingEntity?> GetByIdAsync(int id) =>
            await _context.Ratings.AsNoTracking()
            .FirstOrDefaultAsync(d => d.Id == id);

        public async Task<List<RatingEntity>> GetListAsync() =>
            await _context.Ratings.AsNoTracking()
            .OrderByDescending(x => x.Description)
            .ToListAsync();
    }
}

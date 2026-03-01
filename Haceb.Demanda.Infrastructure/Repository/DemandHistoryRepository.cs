using Haceb.Demanda.Domain.Entities;
using Haceb.Demanda.Domain.IRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Haceb.Demanda.Infrastructure.Repository
{
    public class DemandHistoryRepository(HacebDbContext _context) : IDemandHistoryRepository
    {
        public async Task AddAsync(DemandHistoryEntity history)
        {
            await _context.DemandHistories.AddAsync(history);
        }

        public async Task<IReadOnlyCollection<DemandHistoryEntity>>GetByDemandIdAsync(int demandId)
        {
            return await _context.DemandHistories.AsNoTracking()
                .Include(h => h.UserEntity)
                .Where(h => h.DemandId == demandId)
                .OrderByDescending(h => h.DateRegistry)
                .ToListAsync();
        }
    }
}

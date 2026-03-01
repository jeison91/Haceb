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
    public class DemandTypeRepository(HacebDbContext _context) : IDemandTypeRepository
    {
        public async Task<DemandTypeEntity?> GetByIdAsync(int id) =>
            await _context.DemandTypes.AsNoTracking()
            .FirstOrDefaultAsync(d => d.Id == id);

        public async Task<List<DemandTypeEntity>> GetListAsync() =>
            await _context.DemandTypes.AsNoTracking()
            .OrderByDescending(x => x.Description)
            .ToListAsync();
    }
}

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
    public class DemandRepository(HacebDbContext _context) : IDemandRepository
    {
        public async Task Create(DemandEntity entity)
        {
            await _context.Demands.AddAsync(entity);
        }

        public Task Update(DemandEntity entity)
        {
            _context.Demands.Update(entity);
            return Task.CompletedTask;
        }

        public async Task<DemandEntity?> GetByIdAsync(int id) =>
            await _context.Demands
            .Include(d => d.Rating)
            .Include(d => d.TypeEntity)
            .Include(d => d.UserEntity)
            .Include(d => d.HistoryEntities).ThenInclude(h => h.UserEntity)
            .FirstOrDefaultAsync(d => d.Id == id);

        public async Task<List<DemandEntity>> GetByTypeStatusAsync(int idType, DemandStatus status) =>
            await _context.Demands.AsNoTracking()
            .Include(d => d.Rating)
            .Include(d => d.TypeEntity)
            .Include(d => d.UserEntity)
            .Include(d => d.HistoryEntities).ThenInclude(h=> h.UserEntity)
            .Where(x => x.TypeId == idType && x.Status == status).ToListAsync();

        public async Task<List<DemandEntity>> GetListAsync(int? pageNumber = null, int? pageSize = null)
        {
            IQueryable<DemandEntity> demands;
            if (pageNumber.HasValue && pageSize.HasValue)
            {
                demands = _context.Demands.AsNoTracking()
                    .Include(d => d.Rating)
                    .Include(d => d.TypeEntity)
                    .Include(d => d.UserEntity)
                    .Include(d => d.HistoryEntities).ThenInclude(h => h.UserEntity)
                    .OrderBy(x => x.Id)
                    .Skip((pageNumber.Value - 1) * pageSize.Value)
                    .Take(pageSize.Value);
            }
            else
            {
                demands = _context.Demands.AsNoTracking()
                    .Include(d => d.Rating)
                    .Include(d => d.TypeEntity)
                    .Include(d => d.UserEntity)
                    .Include(d => d.HistoryEntities).ThenInclude(h => h.UserEntity)
                    .OrderBy(x => x.Id);
            }

            return await demands.ToListAsync();
        }
    }
}

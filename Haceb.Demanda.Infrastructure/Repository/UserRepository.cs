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
    public class UserRepository(HacebDbContext _context) : IUserRepository
    {
        public async Task<UserEntity?> GetByIdAsync(int id) =>
            await _context.Users.AsNoTracking()
            .FirstOrDefaultAsync(d => d.Id == id);

        public async Task<List<UserEntity>> GetListAsync() =>
            await _context.Users.AsNoTracking()
            .Where(x => x.IsActive == true)
            .OrderBy(x => x.Id)
            .ToListAsync();

        public async Task Create(UserEntity entity)
        {
            await _context.Users.AddAsync(entity);
            await _context.SaveChangesAsync();
        }
    }
}

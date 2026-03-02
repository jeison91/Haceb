using Haceb.Demanda.Domain.Entities;
using Haceb.Demanda.Domain.Unit;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace Haceb.Demanda.Infrastructure
{
    public class HacebDbContext(DbContextOptions<HacebDbContext> option) : DbContext(option), IUnitOfWork
    {
        public DbSet<UserRolEntity> UserRols { get; set; }
        public DbSet<UserEntity> Users { get; set; }
        public DbSet<RatingEntity> Ratings { get; set; }
        public DbSet<DemandTypeEntity> DemandTypes { get; set; }
        public DbSet<DemandEntity> Demands { get; set; }
        public DbSet<DemandHistoryEntity> DemandHistories { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(HacebDbContext).Assembly);

            modelBuilder.Entity<DemandTypeEntity>().HasData(
                new DemandTypeEntity { Id = 1, Description = "Proyectos" },
                new DemandTypeEntity { Id = 2, Description = "Requerimientos" },
                new DemandTypeEntity { Id = 3, Description = "Soportes" },
                new DemandTypeEntity { Id = 4, Description = "Incidentes" },
                new DemandTypeEntity { Id = 5, Description = "Temas legales" },
                new DemandTypeEntity { Id = 6, Description = "Vulnerabilidades" },
                new DemandTypeEntity { Id = 7, Description = "Apoyos" }
            );

            modelBuilder.Entity<RatingEntity>().HasData(
                new RatingEntity { Id = 1, Description = "Tecnológica" },
                new RatingEntity { Id = 2, Description = "Legal" },
                new RatingEntity { Id = 3, Description = "Operativa" },
                new RatingEntity { Id = 4, Description = "Comercial" }
            );

            modelBuilder.Entity<UserRolEntity>().HasData(
                new UserRolEntity { Id = 1, Name = "Admin", Code = "ADM" },
                new UserRolEntity { Id = 2, Name = "User", Code = "USR" }
            );

            modelBuilder.Entity<UserEntity>().HasData(
                new UserEntity { Id = 1, Username = "Tecnología", Password = "$2a$11$7QZLqxZsMNMfBVQ.MHgJbepYtK8bfwOvFsFxPhXftPnf.9xx6Kiwm", Email = "tecnologia@mail.com", Role = 1, IsActive = true },
                new UserEntity { Id = 2, Username = "Financiera", Password = "$2a$11$7QZLqxZsMNMfBVQ.MHgJbepYtK8bfwOvFsFxPhXftPnf.9xx6Kiwm", Email = "Financiera@mail.com", Role = 2, IsActive = true },
                new UserEntity { Id = 3, Username = "Negociación", Password = "$2a$11$7QZLqxZsMNMfBVQ.MHgJbepYtK8bfwOvFsFxPhXftPnf.9xx6Kiwm", Email = "Negociacion@mail.com", Role = 2, IsActive = true },
                new UserEntity { Id = 4, Username = "Operaciones", Password = "$2a$11$7QZLqxZsMNMfBVQ.MHgJbepYtK8bfwOvFsFxPhXftPnf.9xx6Kiwm", Email = "Operaciones@mail.com", Role = 2, IsActive = true },
                new UserEntity { Id = 5, Username = "I + D + I", Password = "$2a$11$7QZLqxZsMNMfBVQ.MHgJbepYtK8bfwOvFsFxPhXftPnf.9xx6Kiwm", Email = "IDI@mail.com", Role = 2, IsActive = true },
                new UserEntity { Id = 6, Username = "Comercial", Password = "$2a$11$7QZLqxZsMNMfBVQ.MHgJbepYtK8bfwOvFsFxPhXftPnf.9xx6Kiwm", Email = "Comercial@mail.com", Role = 2, IsActive = true },
                new UserEntity { Id = 7, Username = "Mercadeo", Password = "$2a$11$7QZLqxZsMNMfBVQ.MHgJbepYtK8bfwOvFsFxPhXftPnf.9xx6Kiwm", Email = "Mercadeo@mail.com", Role = 2, IsActive = true },
                new UserEntity { Id = 8, Username = "Talento Humano", Password = "$2a$11$7QZLqxZsMNMfBVQ.MHgJbepYtK8bfwOvFsFxPhXftPnf.9xx6Kiwm", Email = "Talentohumano@mail.com", Role = 2, IsActive = true }
            );
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}
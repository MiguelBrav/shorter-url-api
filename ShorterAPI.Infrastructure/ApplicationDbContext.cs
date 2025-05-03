using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ShorterAPI.DTO.Entities;
using System.Reflection.Emit;

namespace ShorterAPI.Infrastructure
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<FavoriteShorty>()
            .HasKey(f => new { f.ShortyId, f.UserId });

            builder.Entity<FavoriteShorty>()
                .HasOne(f => f.Shorty)
                .WithMany()
                .HasForeignKey(f => f.ShortyId);
        }

        public DbSet<Shorty> Shorty { get; set; }
        public DbSet<LogRedirect> LogRedirect { get; set; }
        public DbSet<FavoriteShorty> FavoriteShorty { get; set; }

    }
}

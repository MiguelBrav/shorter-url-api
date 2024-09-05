using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ShorterAPI.DTO.Entities;

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

            builder.Entity<Shorty>()
                      .HasKey(x => x.Id);

            builder.Entity<Shorty>()
                   .HasIndex(u => u.ShortUrl)
                   .IsUnique();
        }

        public DbSet<Shorty> Shorty { get; set; }
        public DbSet<LogRedirect> LogRedirect { get; set; }
    }
}

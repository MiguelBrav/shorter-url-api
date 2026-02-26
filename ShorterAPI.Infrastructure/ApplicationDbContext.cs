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

            builder.Entity<ShortyTag>()
           .HasIndex(st => new { st.ShortyId, st.TagId })
           .IsUnique();

            builder.Entity<ShortyTag>()
                .HasOne<Shorty>()
                .WithMany()
                .HasForeignKey(st => st.ShortyId);

            builder.Entity<ShortyTag>()
                .HasOne(st => st.Tag)
                .WithMany()
                .HasForeignKey(st => st.TagId);
        }

        public DbSet<Shorty> Shorty { get; set; }
        public DbSet<LogRedirect> LogRedirect { get; set; }
        public DbSet<FavoriteShorty> FavoriteShorty { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<ShortyTag> ShortyTags { get; set; }

    }
}

using Microsoft.EntityFrameworkCore;
using ShorterAPI.Domain.Interfaces;
using ShorterAPI.DTO.Entities;
using ShorterAPI.DTO.Responses;

namespace ShorterAPI.Infrastructure.Repositories;

public class FavoriteShortyRepository : IFavoriteShortyRepository
{
    private readonly ApplicationDbContext _context;

    public FavoriteShortyRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<bool> Create(FavoriteShorty favShorty)
    {
        await _context.FavoriteShorty.AddAsync(favShorty);

        return true;
    }

    public async Task<FavoriteShorty> ByIdByUser(string userId, int Id)
    {
        return await _context.FavoriteShorty.AsNoTracking().FirstOrDefaultAsync(s => s.ShortyId == Id && s.UserId == userId);
    }

    public async Task<IEnumerable<FavoriteShortyResponse>> ByUser(string userId, int pageNumber, int pageSize)
    {
        return await _context.FavoriteShorty
            .AsNoTracking()
            .Where(f => f.UserId == userId && f.Shorty != null && !f.Shorty.IsDeleted)
            .Include(f => f.Shorty)
            .OrderByDescending(f => f.CreatedDate) 
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .Select(f => new FavoriteShortyResponse
            {
                Id = f.ShortyId,
                Title = f.Shorty!.Title,
                ShortUrl = f.Shorty.ShortUrl,
                FullUrl = f.Shorty.FullUrl,
                CreatedDate = f.CreatedDate 
            })
            .ToListAsync();
    }

    public async Task<bool> DeleteByIdByUser(string userId, int id)
    {
        FavoriteShorty shorty = await _context.FavoriteShorty.FirstOrDefaultAsync(s => s.ShortyId == id && s.UserId == userId);

        if (shorty == null)
            return false;

        _context.FavoriteShorty.Remove(shorty);
        return true;
    }

    public async Task<bool> DeleteByIdsByUser(string userId, List<int> ids)
    {
        List<FavoriteShorty> favorites = await _context.FavoriteShorty
            .Where(f => f.UserId == userId && ids.Contains(f.ShortyId))
            .ToListAsync();

        if (!favorites.Any())
            return false;

        _context.FavoriteShorty.RemoveRange(favorites);
        await _context.SaveChangesAsync();
        return true;
    }

}

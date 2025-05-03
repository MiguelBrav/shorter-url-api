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


}

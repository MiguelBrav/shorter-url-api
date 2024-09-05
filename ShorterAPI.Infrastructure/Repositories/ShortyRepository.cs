using Microsoft.EntityFrameworkCore;
using ShorterAPI.Domain.Interfaces;
using ShorterAPI.DTO.Entities;

namespace ShorterAPI.Infrastructure.Repositories;

public class ShortyRepository : IShortyRepository
{
    private readonly ApplicationDbContext _context;

    public ShortyRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Shorty> Create(Shorty shorty)
    {
        await _context.Shorty.AddAsync(shorty);

        return shorty;
    }

    public async Task<bool> isExists(string shortyUri)
    {
       return await _context.Shorty.AnyAsync(s => s.ShortUrl == shortyUri && !s.IsDeleted);
        
    }
}

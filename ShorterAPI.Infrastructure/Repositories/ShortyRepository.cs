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
    public async Task<Shorty> ById(int Id)
    {
        return await _context.Shorty.FirstOrDefaultAsync(s => s.Id == Id && !s.IsDeleted);
    }

    public async Task<Shorty> isExistsShorty(string shortyUri)
    {
        return await _context.Shorty.FirstOrDefaultAsync(s => s.ShortUrl == shortyUri && !s.IsDeleted);
    }

    public async Task<Shorty> Update(Shorty shorty)
    {
       _context.Shorty.Update(shorty);

        return shorty;
    }

    public async Task<IEnumerable<Shorty>> GetAllByUser(string userId, int pageNumber, int pageSize)
    {
        return await _context.Shorty.Where(s => s.CreatedUser == userId && !s.IsDeleted).AsNoTracking().Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
    }
}

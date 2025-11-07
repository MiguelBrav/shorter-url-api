using Microsoft.EntityFrameworkCore;
using ShorterAPI.Domain.Interfaces;
using ShorterAPI.DTO.Entities;
using ShorterAPI.DTO.Responses;

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
    public async Task<Shorty> ByIdByUser(string userId, int Id)
    {
        return await _context.Shorty.AsNoTracking().FirstOrDefaultAsync(s => s.Id == Id && s.CreatedUser == userId && !s.IsDeleted);
    }
    public async Task<List<Shorty>> ByIdsByUser(string userId, List<int> ids)
    {
        return await _context.Shorty.Where(s => ids.Contains(s.Id) && s.CreatedUser == userId && !s.IsDeleted).ToListAsync();
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

    public async Task<IEnumerable<ShortyTopResponse>> GetTopShortys(int limit)
    {
        return await _context.Shorty
            .AsNoTracking()
            .Where(s => !s.IsDeleted)
            .Select(s => new ShortyTopResponse
            {
                Id = s.Id,
                ShortUrl = s.ShortUrl,
                CreatedDate = s.CreatedDate,
                AccessCount = _context.LogRedirect.Count(l => l.ShortyId == s.Id)
            })
            .OrderByDescending(s => s.AccessCount)
            .Take(limit)
            .ToListAsync();
    }

    public async Task<IEnumerable<ShortyLeastUsedResponse>> GetLeastUsedShortys(int limit)
    {
        return await _context.Shorty
            .AsNoTracking()
            .Where(s => !s.IsDeleted)
            .Select(s => new ShortyLeastUsedResponse
            {
                Id = s.Id,
                ShortUrl = s.ShortUrl,
                CreatedDate = s.CreatedDate,
                AccessCount = _context.LogRedirect.Count(l => l.ShortyId == s.Id)
            })
            .OrderBy(s => s.AccessCount)
            .Take(limit)
            .ToListAsync();
    }

    public async Task<IEnumerable<LastShortyResponse>> GetLastShortys(int limit)
    {
        return await _context.Shorty
            .AsNoTracking()
            .Where(s => !s.IsDeleted)
            .OrderByDescending(s => s.CreatedDate)
            .Take(limit)
            .Select(s => new LastShortyResponse
            {
                Id = s.Id,
                ShortUrl = s.ShortUrl,
                CreatedDate = s.CreatedDate
            })
            .ToListAsync();
    }

    public async Task<IEnumerable<RandomShortyResponse>> GetRandomShortys(int limit)
    {
        List<int> validIds = await _context.Shorty
            .AsNoTracking()
            .Where(s => !s.IsDeleted)
            .Select(s => s.Id)
            .ToListAsync();

        if (!validIds.Any() || limit <= 0)
            return Enumerable.Empty<RandomShortyResponse>();

        int idsToSelect = Math.Min(limit, validIds.Count);

        Random random = new Random();
        List<int> selectedIds = validIds
            .OrderBy(_ => random.Next())
            .Take(idsToSelect)
            .ToList();

        List<RandomShortyResponse> result = await _context.Shorty
            .AsNoTracking()
            .Where(s => selectedIds.Contains(s.Id))
            .Select(s => new RandomShortyResponse
            {
                Id = s.Id,
                ShortUrl = s.ShortUrl,
                CreatedDate = s.CreatedDate
            })
            .ToListAsync();

        return result;
    }




}

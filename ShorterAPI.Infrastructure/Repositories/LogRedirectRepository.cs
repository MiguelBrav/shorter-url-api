using Microsoft.EntityFrameworkCore;
using ShorterAPI.Domain.Interfaces;
using ShorterAPI.DTO.Entities;

namespace ShorterAPI.Infrastructure.Repositories;

public class LogRedirectRepository : ILogRedirectRepository
{
    private readonly ApplicationDbContext _context;

    public LogRedirectRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<LogRedirect> Create(LogRedirect logRedirect)
    {
        await _context.LogRedirect.AddAsync(logRedirect);

        return logRedirect;
    }
    public async Task<IEnumerable<LogRedirect>> ByShorty(int shortyId)
    {
        return await _context.LogRedirect.Where(s => s.ShortyId == shortyId).AsNoTracking().ToListAsync();
    }

    public async Task<IEnumerable<LogRedirect>> ByShortyIds(IEnumerable<int> shortyIds)
    {
        return await _context.LogRedirect.Where(s => shortyIds.Contains(s.ShortyId)).AsNoTracking().ToListAsync();
    }

}

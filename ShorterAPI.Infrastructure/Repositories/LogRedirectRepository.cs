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
   
}

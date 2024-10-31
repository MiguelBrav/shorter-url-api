using ShorterAPI.Domain.Interfaces;
using ShorterAPI.Domain.UOW;

namespace ShorterAPI.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        public IShortyRepository ShortyRepository { get; }
        public ILogRedirectRepository LogRepository { get; }

        private readonly ApplicationDbContext _context;

        public UnitOfWork(ApplicationDbContext context, IShortyRepository shortyRepository, ILogRedirectRepository logRepository)
        {
            _context = context;
            ShortyRepository = shortyRepository;
            LogRepository = logRepository;
        }

        public async Task<int> Save()
            => await _context.SaveChangesAsync();

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}

using ShorterAPI.Domain.Interfaces;
using ShorterAPI.Domain.UOW;

namespace ShorterAPI.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        public IShortyRepository ShortyRepository { get; }
        public ILogRedirectRepository LogRepository { get; }
        public IFavoriteShortyRepository FavoriteShortyRepository { get; }

        private readonly ApplicationDbContext _context;

        public UnitOfWork(ApplicationDbContext context, IShortyRepository shortyRepository, ILogRedirectRepository logRepository, IFavoriteShortyRepository favoriteShortyRepository)
        {
            _context = context;
            ShortyRepository = shortyRepository;
            LogRepository = logRepository;
            FavoriteShortyRepository = favoriteShortyRepository;
        }

        public async Task<int> Save()
            => await _context.SaveChangesAsync();

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}

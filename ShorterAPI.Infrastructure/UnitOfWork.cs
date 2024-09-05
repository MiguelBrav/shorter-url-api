using ShorterAPI.Domain.Interfaces;
using ShorterAPI.Domain.UOW;

namespace ShorterAPI.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        public IShortyRepository ShortyRepository { get; }
        private readonly ApplicationDbContext _context;

        public UnitOfWork(ApplicationDbContext context, IShortyRepository shortyRepository)
        {
            _context = context;
            ShortyRepository = shortyRepository;
        }

        public async Task<int> Save()
            => await _context.SaveChangesAsync();

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}

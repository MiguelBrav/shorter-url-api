
using ShorterAPI.Domain.Interfaces;

namespace ShorterAPI.Domain.UOW
{
    public interface IUnitOfWork : IDisposable
    {
        IShortyRepository ShortyRepository { get; }
        ILogRedirectRepository LogRepository { get; }
        IFavoriteShortyRepository FavoriteShortyRepository { get; }

        Task<int> Save();
    }

}


using ShorterAPI.Domain.Interfaces;

namespace ShorterAPI.Domain.UOW
{
    public interface IUnitOfWork : IDisposable
    {
        IShortyRepository ShortyRepository { get; }
        ILogRedirectRepository LogRepository { get; }

        Task<int> Save();
    }

}

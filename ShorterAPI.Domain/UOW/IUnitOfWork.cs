
using ShorterAPI.Domain.Interfaces;

namespace ShorterAPI.Domain.UOW
{
    public interface IUnitOfWork : IDisposable
    {
        IShortyRepository ShortyRepository { get; }
        Task<int> Save();
    }

}

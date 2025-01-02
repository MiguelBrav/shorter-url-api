using ShorterAPI.DTO.Entities;

namespace ShorterAPI.Domain.Interfaces;

public interface ILogRedirectRepository
{
    Task<LogRedirect> Create(LogRedirect logRedirect);
    Task<IEnumerable<LogRedirect>> ByShorty(int shortyId);

    Task<IEnumerable<LogRedirect>> ByShortyIds(IEnumerable<int> shortyIds);
}

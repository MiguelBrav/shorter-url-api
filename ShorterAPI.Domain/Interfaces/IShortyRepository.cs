using ShorterAPI.DTO.Entities;
using ShorterAPI.DTO.Responses;

namespace ShorterAPI.Domain.Interfaces;

public interface IShortyRepository
{
    Task<Shorty> Create(Shorty shorty);

    Task<Shorty> Update(Shorty shorty);

    Task<Shorty> ById(int Id);

    Task<Shorty> ByIdByUser(string userId, int id);
    Task<List<Shorty>> ByIdsByUser(string userId, List<int> ids);
    Task<Shorty> isExistsShorty(string shortyUri);

    Task<bool> isExists(string shortyUri);

    Task<IEnumerable<Shorty>> GetAllByUser(string userId, int pageNumber, int pageSize);
    Task<IEnumerable<ShortyTopResponse>> GetTopShortys(int limit);
    Task<IEnumerable<ShortyLeastUsedResponse>> GetLeastUsedShortys(int limit);
    Task<IEnumerable<LastShortyResponse>> GetLastShortys(int limit);
    Task<IEnumerable<RandomShortyResponse>> GetRandomShortys(int limit);
    Task<IEnumerable<ShortyNeverUsedResponse>> GetNeverUsedShortys(int limit);
}

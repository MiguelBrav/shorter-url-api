using ShorterAPI.DTO.Entities;

namespace ShorterAPI.Domain.Interfaces;

public interface IShortyRepository
{
    Task<Shorty> Create(Shorty shorty);

    Task<Shorty> Update(Shorty shorty);

    Task<Shorty> ById(int Id);

    Task<Shorty> isExistsShorty(string shortyUri);

    Task<bool> isExists(string shortyUri);
}

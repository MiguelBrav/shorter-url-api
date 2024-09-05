using ShorterAPI.DTO.Entities;

namespace ShorterAPI.Domain.Interfaces;

public interface IShortyRepository
{
    Task<Shorty> Create(Shorty shorty);
}

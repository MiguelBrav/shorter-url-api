using ShorterAPI.DTO.Entities;
using ShorterAPI.DTO.Responses;

namespace ShorterAPI.Domain.Interfaces;

public interface IFavoriteShortyRepository
{
    Task<bool> Create(FavoriteShorty favoriteShorty);
    Task<FavoriteShorty> ByIdByUser(string userId, int id);
    Task<IEnumerable<FavoriteShortyResponse>> ByUser(string userId, int pageNumber, int pageSize);
    Task<bool> DeleteByIdByUser(string userId, int id);
}

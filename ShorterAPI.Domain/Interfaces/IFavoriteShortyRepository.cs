using ShorterAPI.DTO.Entities;
using ShorterAPI.DTO.Responses;

namespace ShorterAPI.Domain.Interfaces;

public interface IFavoriteShortyRepository
{
    Task<bool> Create(FavoriteShorty favoriteShorty);
    Task<bool> CreateList(List<FavoriteShorty> favoriteShortys);
    Task<FavoriteShorty> ByIdByUser(string userId, int id);
    Task<List<FavoriteShorty>> ByUser(string userId);
    Task<IEnumerable<FavoriteShortyResponse>> ByUser(string userId, int pageNumber, int pageSize);
    Task<bool> DeleteByIdByUser(string userId, int id);
    Task<bool> DeleteByIdsByUser(string userId, List<int> ids);
    Task<bool> DeleteAllByUser(string userId);

}

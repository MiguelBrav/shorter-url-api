using ShorterAPI.DTO.Entities;

namespace ShorterAPI.Domain.Interfaces;

public interface IFavoriteShortyRepository
{
    Task<bool> Create(FavoriteShorty favoriteShorty);
    Task<FavoriteShorty> ByIdByUser(string userId, int id);

}

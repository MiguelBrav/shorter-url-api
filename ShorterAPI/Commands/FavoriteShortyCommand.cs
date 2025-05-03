using MediatR;
using ShorterAPI.DTO.DTOs;

namespace ShorterAPI.Commands;

public class FavoriteShortyCommand : IRequest<IResult>
{
    public string UserName { get; set; } = string.Empty;
    public CreateFavoriteDTO FavShorty { get; set; } = new CreateFavoriteDTO();
}

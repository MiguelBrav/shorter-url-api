using Mediator;
using ShorterAPI.DTO.DTOs;

namespace ShorterAPI.Commands;

public class DeleteFavShortyCommand : IRequest<IResult>
{
    public string UserName { get; set; } = string.Empty;
    public ShortyIdDTO FavShorty { get; set; } = new ShortyIdDTO();
}

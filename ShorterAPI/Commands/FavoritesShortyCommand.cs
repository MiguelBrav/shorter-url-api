using MediatR;
using ShorterAPI.DTO.DTOs;

namespace ShorterAPI.Commands;

public class FavoritesShortyCommand : IRequest<IResult>
{
    public string UserName { get; set; } = string.Empty;
    public List<int> ShortyIds { get; set; } = new List<int>();
}

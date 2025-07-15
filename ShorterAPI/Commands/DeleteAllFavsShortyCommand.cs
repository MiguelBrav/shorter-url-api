using MediatR;
using ShorterAPI.DTO.DTOs;

namespace ShorterAPI.Commands;

public class DeleteAllFavsShortyCommand : IRequest<IResult>
{
    public string UserName { get; set; } = string.Empty;
}


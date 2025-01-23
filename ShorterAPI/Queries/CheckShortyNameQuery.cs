using MediatR;
using ShorterAPI.DTO.DTOs;

namespace ShorterAPI.Queries;

public class CheckShortyNameQuery : IRequest<IResult>
{
    public ShortyUrlDTO ShortyUrl { get; set; } = new ShortyUrlDTO();
}

using MediatR;
using ShorterAPI.DTO.DTOs;

namespace ShorterAPI.Commands;

public class ShortyCommand : IRequest<IResult>
{
    public string UserName {  get; set; } = string.Empty;
    public CreateShortyDTO Shorty { get; set; } = new CreateShortyDTO();
}

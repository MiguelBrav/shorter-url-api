using MediatR;
using ShorterAPI.DTO.DTOs;

namespace ShorterAPI.Commands;

public class GenerateShortyCommand : IRequest<IResult>
{
    public string UserName {  get; set; } = string.Empty;
    public GenerateShortyDTO Shorty { get; set; } = new GenerateShortyDTO();
}

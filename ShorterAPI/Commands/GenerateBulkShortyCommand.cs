using MediatR;
using ShorterAPI.DTO.DTOs;

namespace ShorterAPI.Commands;

public class GenerateBulkShortyCommand : IRequest<IResult>
{
    public string UserName {  get; set; } = string.Empty;
    public List<GenerateShortyDTO> Shortys { get; set; } = new List<GenerateShortyDTO>();
}

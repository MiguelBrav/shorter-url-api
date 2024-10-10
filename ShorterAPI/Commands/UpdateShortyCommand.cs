using MediatR;
using ShorterAPI.DTO.DTOs;

namespace ShorterAPI.Commands;

public class UpdateShortyCommand : IRequest<IResult>
{
    public string UserName {  get; set; } = string.Empty;
    public UpdateShortyDTO Shorty { get; set; } = new UpdateShortyDTO();
}

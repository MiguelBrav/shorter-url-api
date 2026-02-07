using MediatR;
using ShorterAPI.DTO.DTOs;

namespace ShorterAPI.Commands;

public class CloneShortyCommand : IRequest<IResult>
{
    public string UserName {  get; set; } = string.Empty;
    public CloneShortyDTO Shorty { get; set; } = new CloneShortyDTO();
}

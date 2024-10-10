using MediatR;
using ShorterAPI.DTO.DTOs;

namespace ShorterAPI.Commands;

public class DeleteShortyCommand : IRequest<IResult>
{
    public string UserName {  get; set; } = string.Empty;
    public ShortyIdDTO Shorty { get; set; } = new ShortyIdDTO();
}

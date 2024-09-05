using MediatR;
using ShorterAPI.DTO.DTOs;
using ShorterAPI.DTO.Responses;

namespace ShorterAPI.Commands
{
    public class ShortyCommand : IRequest<ApiResponse>
    {
        public string UserId {  get; set; } = string.Empty;
        public CreateShortyDTO Shorty { get; set; } = new CreateShortyDTO();
    }
}

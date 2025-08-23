using MediatR;

namespace ShorterAPI.Queries;

public class FreeQrShortyByUrlQuery : IRequest<IResult>
{
    public string UserName { get; set; } = string.Empty;
    public string FullUrl { get; set; } = string.Empty;
}

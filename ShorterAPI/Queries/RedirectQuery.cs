using MediatR;

namespace ShorterAPI.Queries;

public class RedirectQuery : IRequest<IResult>
{
    public string ShortyUrl { get; set; } = string.Empty;

}

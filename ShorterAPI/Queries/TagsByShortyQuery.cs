using MediatR;

namespace ShorterAPI.Queries;

public class TagsByShortyQuery : IRequest<IResult>
{
    public string UserName { get; set; } = string.Empty;
    public int ShortyId { get; set; }
}

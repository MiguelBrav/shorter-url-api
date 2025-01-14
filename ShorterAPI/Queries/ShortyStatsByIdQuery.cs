using MediatR;

namespace ShorterAPI.Queries;

public class ShortyStatsByIdQuery : IRequest<IResult>
{
    public string UserName { get; set; } = string.Empty;
    public int ShortyId { get; set; }
}

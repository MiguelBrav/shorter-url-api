using MediatR;

namespace ShorterAPI.Queries;

public class LeastUsedShortysReportQuery : IRequest<IResult>
{
    public int Limit { get; set; }
}

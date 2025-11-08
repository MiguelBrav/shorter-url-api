using MediatR;

namespace ShorterAPI.Queries;

public class LeastUsedShortysReportCsvQuery : IRequest<IResult>
{
    public int Limit { get; set; }
}

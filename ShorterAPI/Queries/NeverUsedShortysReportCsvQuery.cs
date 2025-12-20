using MediatR;

namespace ShorterAPI.Queries;

public class NeverUsedShortysReportCsvQuery : IRequest<IResult>
{
    public int Limit { get; set; }
}

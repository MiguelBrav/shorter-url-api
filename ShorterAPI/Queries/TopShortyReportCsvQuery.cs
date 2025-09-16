using MediatR;

namespace ShorterAPI.Queries;

public class TopShortyReportCsvQuery : IRequest<IResult>
{
    public int Limit { get; set; }
}

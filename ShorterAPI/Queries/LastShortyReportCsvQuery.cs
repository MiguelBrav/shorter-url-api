using MediatR;

namespace ShorterAPI.Queries;

public class LastShortyReportCsvQuery : IRequest<IResult>
{
    public int Limit { get; set; }
}

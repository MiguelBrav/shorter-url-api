using MediatR;

namespace ShorterAPI.Queries;

public class RandomShortyReportCsvQuery : IRequest<IResult>
{
    public int Limit { get; set; }
}

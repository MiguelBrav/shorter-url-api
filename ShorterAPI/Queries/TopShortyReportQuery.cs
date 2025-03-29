using MediatR;

namespace ShorterAPI.Queries;

public class TopShortyReportQuery : IRequest<IResult>
{
    public int Limit { get; set; }
}

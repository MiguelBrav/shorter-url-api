using MediatR;

namespace ShorterAPI.Queries;

public class RandomShortyReportQuery : IRequest<IResult>
{
    public int Limit { get; set; }
}

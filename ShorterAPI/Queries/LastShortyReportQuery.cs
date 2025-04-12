using MediatR;

namespace ShorterAPI.Queries;

public class LastShortyReportQuery : IRequest<IResult>
{
    public int Limit { get; set; }
}

using MediatR;

namespace ShorterAPI.Queries;

public class NeverUsedShortysReportQuery : IRequest<IResult>
{
    public int Limit { get; set; }
}

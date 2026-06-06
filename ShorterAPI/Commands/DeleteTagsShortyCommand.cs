using MediatR;

namespace ShorterAPI.Commands;

public class DeleteTagsShortyCommand : IRequest<IResult>
{
    public string UserName { get; set; } = string.Empty;
    public int ShortyId { get; set; }
    public List<string> Tags { get; set; } = new List<string>();
}

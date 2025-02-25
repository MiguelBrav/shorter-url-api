using MediatR;
using ShorterAPI.Domain.UOW;
using ShorterAPI.DTO.Entities;
using ShorterAPI.Events;

namespace ShorterAPI.Queries;

public class RedirectQueryHandler : IRequestHandler<RedirectQuery, IResult>
{
    private readonly IUnitOfWork _unitOfWork;

    private readonly IMediator _mediator;

    public RedirectQueryHandler(IUnitOfWork unitOfWork, IMediator mediator)
    {
        _unitOfWork = unitOfWork;
        _mediator = mediator;
    }
    public async Task<IResult> Handle(RedirectQuery request, CancellationToken cancellationToken)
    {
        Shorty shorty = await _unitOfWork.ShortyRepository.isExistsShorty(request.ShortyUrl);

        if (shorty is null)
        {
            return TypedResults.NoContent();
        }

        try 
        {         
            await _mediator.Publish(new RedirectEvent(shorty));
        }
        catch (Exception)
        {
            // TODO: - add logger serilog or other
            throw;
        }

        return TypedResults.Redirect(shorty.FullUrl);
    }
}

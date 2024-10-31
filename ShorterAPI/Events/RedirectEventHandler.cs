using MediatR;
using ShorterAPI.Domain.UOW;
using ShorterAPI.DTO.Entities;

namespace ShorterAPI.Events;

public class RedirectEventHandler : INotificationHandler<RedirectEvent>
{

    private readonly IUnitOfWork _unitOfWork;

    public RedirectEventHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(RedirectEvent logRedirect, CancellationToken cancellationToken)
    {
        LogRedirect logRedirectShorty = new LogRedirect(logRedirect.logShorty.Id);

        try
        {
            await _unitOfWork.LogRepository.Create(logRedirectShorty);
            await _unitOfWork.Save();

        }
        catch (Exception)
        {
            throw;
        }
    }
}

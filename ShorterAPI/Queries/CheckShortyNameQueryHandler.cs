using MediatR;
using Microsoft.AspNetCore.Identity;
using ShorterAPI.Domain.UOW;
using ShorterAPI.DTO.Entities;
using ShorterAPI.DTO.Responses;

namespace ShorterAPI.Queries;

public class CheckShortyNameQueryHandler : IRequestHandler<CheckShortyNameQuery, IResult>
{
    private readonly IUnitOfWork _unitOfWork;

    public CheckShortyNameQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;

    }
    public async Task<IResult> Handle(CheckShortyNameQuery request, CancellationToken cancellationToken)
    {
        Shorty shorty = await _unitOfWork.ShortyRepository.isExistsShorty(request.ShortyUrl.ShortyName);

        if (shorty is not null)
        {
            return TypedResults.BadRequest($"Shorty url: {shorty.ShortUrl}, is already taken, try another.");
        }
        
        return TypedResults.Ok($"Shorty url: {request.ShortyUrl.ShortyName}, is available");
    }
}

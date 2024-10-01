﻿using MediatR;
using ShorterAPI.DTO.DTOs;

namespace ShorterAPI.Commands;

public class ShortyCommand : IRequest<IResult>
{
    public string UserId {  get; set; } = string.Empty;
    public CreateShortyDTO Shorty { get; set; } = new CreateShortyDTO();
}

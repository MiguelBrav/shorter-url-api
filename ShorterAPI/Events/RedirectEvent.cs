using MediatR;
using ShorterAPI.DTO.Entities;

namespace ShorterAPI.Events;

public class RedirectEvent : INotification
{ 
    public Shorty logShorty { get; set; }

    public RedirectEvent(Shorty shorty)
    {
        logShorty = shorty;     
    }
}

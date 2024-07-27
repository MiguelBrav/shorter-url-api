using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace ShorterAPI.Endpoints;

public static class UrlEndpoints
{
    public static RouteGroupBuilder MapUrl(this RouteGroupBuilder group)
    {
        // TODO 
        group.MapGet("/", () => "All URLs from User")
              .RequireAuthorization(); 

        group.MapGet("/{route}", (string route) => $"URL with shorty: {route} going redirect To =>");

        group.MapPost("/", () => $"URL saved, your new url is =>").RequireAuthorization();

        group.MapPut("/", () => $"URL updated, your updated url is =>").RequireAuthorization();

        group.MapDelete("/{id}", (int id) => $"URL {id} deleted").RequireAuthorization();

        return group;
    }
}

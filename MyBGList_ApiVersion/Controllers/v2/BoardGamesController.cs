using Microsoft.AspNetCore.Mvc;
using MyBGList_ApiVersion.DTO.v2;

namespace MyBGList_ApiVersion.Controllers.v2;

[Route("v{version:apiVersion}/[controller]")] // https://github.com/dotnet/aspnet-api-versioning/wiki/Versioning-via-the-URL-Path
[ApiController]
[ApiVersion("2.0")]
public class BoardGamesController : ControllerBase
{
    private readonly ILogger<BoardGamesController> _logger;

    public BoardGamesController(ILogger<BoardGamesController> logger)
    {
        _logger = logger;
    }

    // [HttpGet("GetBoardGames")] //local:PORT/boardgames/GetBoardGames 
    [HttpGet] // local:PORT/boardgames
    [ResponseCache(Location = ResponseCacheLocation.Client, Duration = 120)]
    public RestDTO<BoardGame[]> Get()
    {
        return new()
        {
            Items = new[]
            {
                new BoardGame(),
                
            },
            Links = new List<LinkDTO[]>
            {
                new[]
                {
                    new LinkDTO(
                        href: Url.Action("Get", "BoardGames", null, Request.Scheme)!,
                        rel: "self",
                        type: "GET"
                    ),
                }
            }
        };
    }
}
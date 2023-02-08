using Microsoft.AspNetCore.Mvc;
using MyBGList_ApiVersion.DTO.v3;

namespace MyBGList_ApiVersion.Controllers.v3;

// ==> v3.1/Name_Controller -> will be added into v3.1/swagger.json
[Route("v{version:apiVersion}/[controller]")] // https://github.com/dotnet/aspnet-api-versioning/wiki/Versioning-via-the-URL-Path
[ApiController]
[ApiVersion("3.1")]
public class BoardGamesController : ControllerBase
{
    private readonly ILogger<BoardGamesController> _logger;

    public BoardGamesController(ILogger<BoardGamesController> logger)
    {
        _logger = logger;
    }

    // [HttpGet("GetBoardGames")] //local:PORT/boardgames/GetBoardGames 
    [HttpGet] // local:PORT/boardgames
    [ResponseCache(Location = ResponseCacheLocation.Any, Duration = 60)]
    public RestDTO<BoardGame[]> Get()
    {
        return new()
        {
            Items = new[]
            {
                new BoardGame(),
                
            },
            Links = new()
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
namespace MyBGList_ApiVersion;

public class BoardGame
{
    public int Id { get; set; }
    
    public string? Name { get; set; }
    
    public int? Year { get; set; }

    public int? MaxPlayers { get; init; }
    
    public int? MinPlayers { get; init; }
}

// public record BoardGame(int Id, string? Name, int? Year);


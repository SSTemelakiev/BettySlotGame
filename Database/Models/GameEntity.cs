namespace Database.Models;

public class GameEntity
{
    public int Id { get; set; }
    public decimal Balance { get; set; }
    public DateTime StartedAt { get; set; } = DateTime.UtcNow;
}
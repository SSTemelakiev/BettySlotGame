namespace Database.Models;

public class GameSessionEntity
{
    public int Id { get; set; }
    public decimal Balance { get; set; }
    public DateTime StartedAt { get; set; } = DateTime.UtcNow;
}
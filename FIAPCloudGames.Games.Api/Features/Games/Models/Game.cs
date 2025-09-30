namespace FIAPCloudGames.Games.Api.Features.Games.Models;

public class Game 
{
    private Game() { }
    public Guid Id { get; init; }
    public DateTime CreatedAt { get; init; }
    public string Name { get; private set; } = string.Empty;
    public string Description { get; private set; } = string.Empty;
    public DateTime ReleasedAt { get; private set; }
    public decimal Price { get; private set; }
    public bool IsActive { get; private set; }
    public string Genre { get; private set; } = string.Empty;
    public DateTime? UpdatedAt { get; private set; }
    public Guid? PromotionId { get; private set; }

    public static Game Create(string name, string description, DateTime releasedAt, decimal price, string genre)
    {
        return new Game
        {
            Id = Guid.NewGuid(),
            CreatedAt = DateTime.UtcNow,
            Name = name,
            Description = description,
            ReleasedAt = releasedAt,
            Price = price,
            IsActive = true,
            Genre = genre
        };
    }

    public void Delete()
    {
        IsActive = false;
        UpdatedAt = DateTime.UtcNow;
    }

    public void Update(string name, string description, DateTime releasedAt, decimal price, string genre)
    {
        Name = name;
        Description = description;
        ReleasedAt = releasedAt;
        Price = price;
        Genre = genre;
        UpdatedAt = DateTime.UtcNow;
    }

    public void ApplyPromotion(Guid promotionId)
    {
        PromotionId = promotionId;
        UpdatedAt = DateTime.UtcNow;
    }

    public void RemovePromotion()
    {
        PromotionId = null;
        UpdatedAt = DateTime.UtcNow;
    }
}

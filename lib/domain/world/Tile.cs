namespace CityBuilder.World;

public enum TileType
{
    Land,
    Water,
    Tree,
    Rock,
    Coal,
    Iron,
}

public struct Tile
{
    public required TileType Type;
    public required int? ResourceAmount;
}

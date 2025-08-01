namespace CityBuilder.World;

public class Map
{
    public int Width { get; }
    public int Height { get; }
    public Tile[,] Tiles { get; }

    public Map(int width, int height)
    {
        Width = width;
        Height = height;
        Tiles = new Tile[width, height];

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Tiles[x, y] = new Tile { Type = TileType.Land, ResourceAmount = null };
            }
        }
    }

    public ref Tile GetTileRef(int x, int y) => ref Tiles[x, y];

    public void SetTile(int x, int y, Tile tile) => Tiles[x, y] = tile;
}

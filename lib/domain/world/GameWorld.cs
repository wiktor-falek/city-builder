using Microsoft.Xna.Framework;

namespace CityBuilder.World;

public class GameWorld
{
    public Map Map;

    public void Initialize()
    {
        int worldSeed = 42069;
        Map = MapGenerator.Generate(50, 50, worldSeed);
    }

    public void Update(GameTime gameTime) { }
}

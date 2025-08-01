using System;
using System.Collections.Generic;
using System.Linq;

namespace CityBuilder.World;

public static class MapGenerator
{
    public static Map Generate(int width, int height, int seed)
    {
        Random rng = new(seed);
        Map map = new(width, height);

        /*
            FUTURE:
            - Generate bodies of water
            - Generate deposit clusters on land
            - Populate leftover land with trees
        */
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Dictionary<int, TileType> weights = new()
                {
                    { 1000, TileType.Land },
                    { 100, TileType.Tree },
                };

                TileType tileType = weights.WeightedRandom(rng);
                Tile tile = new() { Type = tileType, ResourceAmount = 0 };
                map.SetTile(x, y, tile);
            }
        }

        return map;
    }

    private static T WeightedRandom<T>(this Dictionary<int, T> weights, Random rng)
    {
        int diceRoll = rng.Next(1, weights.Keys.Sum() + 1);
        int cumulative = 0;
        foreach (var kvp in weights)
        {
            cumulative += kvp.Key;
            if (diceRoll <= cumulative)
            {
                return kvp.Value;
            }
        }

        throw new Exception("Weighted random fallthrough");
    }
}

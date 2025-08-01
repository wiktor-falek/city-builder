using System;
using System.Linq;
using CityBuilder.World;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace CityBuilder.Rendering;

public class TileRenderer
{
    private const int TILE_SIZE = 10;
    private Model _groundGrassModel;
    private Model _treeModel;

    public void LoadContent(ContentManager content)
    {
        _groundGrassModel = content.Load<Model>("ground_grass");
        _treeModel = content.Load<Model>("tree_small");
    }

    public void DrawTiles(Map map, Camera camera)
    {
        Matrix view = camera.ViewMatrix;

        Matrix projection = Matrix.CreatePerspectiveFieldOfView(
            fieldOfView: MathHelper.PiOver4,
            aspectRatio: 16 / 9,
            nearPlaneDistance: 0.1f,
            farPlaneDistance: 1000f
        );

        for (int x = 0; x < map.Width; x++)
        {
            for (int y = 0; y < map.Height; y++)
            {
                DrawTile(map, view, projection, x, y);
            }
        }
    }

    private void DrawTile(Map map, Matrix view, Matrix projection, int x, int y)
    {
        ref readonly Tile tile = ref map.GetTileRef(x, y);

        switch (tile.Type)
        {
            case TileType.Land:
                Draw3DModel(_groundGrassModel, view, projection, x * TILE_SIZE, y * TILE_SIZE);
                break;

            case TileType.Tree:
                Draw3DModel(_groundGrassModel, view, projection, x * TILE_SIZE, y * TILE_SIZE);
                Draw3DModel(_treeModel, view, projection, x * TILE_SIZE, y * TILE_SIZE);
                break;

            default:
                throw new Exception("Unhandled tile rendering");
        }
    }

    private static void Draw3DModel(Model model, Matrix view, Matrix projection, int x, int y)
    {
        Matrix scale = Matrix.CreateScale(1f);
        Matrix rotation = Matrix.CreateRotationX(MathHelper.ToRadians(90));
        Matrix translation = Matrix.CreateTranslation(new Vector3(x, y, 0f));
        Matrix world = scale * rotation * translation; // positioning of the object

        foreach (ModelMesh mesh in model.Meshes)
        {
            foreach (BasicEffect effect in mesh.Effects.Cast<BasicEffect>())
            {
                effect.World = world;
                effect.View = view;
                effect.Projection = projection;
                effect.EnableDefaultLighting();
            }

            mesh.Draw();
        }
    }
}

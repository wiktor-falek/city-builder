using System;
using System.Linq;
using CityBuilder.Input;
using CityBuilder.Rendering;
using CityBuilder.World;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace CityBuilder.Scene;

public class GameScene(InputManager inputManager, GameWorld gameWorld) : IScene
{
    private InputManager _inputManager = inputManager;
    private Camera _camera = new(inputManager, viewportWidth: 1280, viewportHeight: 720);
    private Texture2D _whiteTexture;
    private GameWorld _gameWorld = gameWorld;
    private Model _groundGrassModel;
    private Model _treeModel;

    public void Initialize()
    {
        _gameWorld.Initialize();
    }

    public void LoadContent(GraphicsDevice graphicsDevice, ContentManager contentManager)
    {
        _groundGrassModel = contentManager.Load<Model>("ground_grass");
        _treeModel = contentManager.Load<Model>("tree_small");

        _whiteTexture = new Texture2D(graphicsDevice, 1, 1);
        _whiteTexture.SetData([Color.White]);
    }

    public void Update(GameTime gameTime)
    {
        _camera.Update(gameTime);
    }

    public void Draw()
    {
        const int TILE_SIZE = 10;

        for (int x = 0; x < _gameWorld.Map.Width; x++)
        {
            for (int y = 0; y < _gameWorld.Map.Height; y++)
            {
                ref readonly Tile tile = ref _gameWorld.Map.GetTileRef(x, y);

                switch (tile.Type)
                {
                    case TileType.Land:
                        Draw3DModel(_groundGrassModel, x * TILE_SIZE, y * TILE_SIZE);
                        break;

                    case TileType.Tree:
                        Draw3DModel(_groundGrassModel, x * TILE_SIZE, y * TILE_SIZE);
                        Draw3DModel(_treeModel, x * TILE_SIZE, y * TILE_SIZE);
                        break;

                    default:
                        throw new Exception("Unhandled tile rendering");
                }
            }
        }
    }

    public void Dispose()
    {
        throw new NotImplementedException();
    }

    private void Draw3DModel(Model model, int x, int y)
    {
        Matrix scale = Matrix.CreateScale(1f);
        Matrix rotation =
            Matrix.CreateRotationX(MathHelper.ToRadians(90))
            * Matrix.CreateRotationY(MathHelper.ToRadians(0))
            * Matrix.CreateRotationZ(MathHelper.ToRadians(0));
        Matrix translation = Matrix.CreateTranslation(new Vector3(x, y, 0f));
        Matrix world = scale * rotation * translation; // positioning of the object

        Matrix view = _camera.ViewMatrix;

        float fieldOfView = MathHelper.PiOver4;
        float aspectRatio = 16 / 9;
        float nearPlaneDistance = 0.1f;
        float farPlaneDistance = 1000f;
        Matrix projection = Matrix.CreatePerspectiveFieldOfView(
            fieldOfView,
            aspectRatio,
            nearPlaneDistance,
            farPlaneDistance
        ); // transforms into 2D image

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

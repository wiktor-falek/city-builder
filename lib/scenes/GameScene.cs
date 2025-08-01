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

    public void Initialize()
    {
        _gameWorld.Initialize();
    }

    public void LoadContent(GraphicsDevice graphicsDevice, ContentManager contentManager)
    {
        _whiteTexture = new Texture2D(graphicsDevice, 1, 1);
        _whiteTexture.SetData(new[] { Color.White });
    }

    public void Update(GameTime gameTime)
    {
        _camera.Update(gameTime);
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Begin(SpriteSortMode.BackToFront, transformMatrix: _camera.ViewMatrix);

        for (int x = 0; x < _gameWorld.Map.Width; x++)
        {
            for (int y = 0; y < _gameWorld.Map.Height; y++)
            {
                ref readonly Tile tile = ref _gameWorld.Map.GetTileRef(x, y);

                Color color = tile.Type switch
                {
                    TileType.Land => Color.SandyBrown,
                    TileType.Water => Color.LightBlue,
                    TileType.Tree => Color.Green,
                    TileType.Rock => Color.SlateGray,
                    TileType.Coal => Color.Black,
                    TileType.Iron => Color.DarkGray,
                    _ => Color.White,
                };

                int tileSize = 32;

                Rectangle destination = new(x * tileSize, y * tileSize, tileSize, tileSize);

                spriteBatch.Draw(_whiteTexture, destination, color);
            }
        }

        spriteBatch.End();
    }

    public void Dispose()
    {
        throw new System.NotImplementedException();
    }
}

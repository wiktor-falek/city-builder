using CityBuilder.Input;
using CityBuilder.Rendering;
using CityBuilder.World;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace CityBuilder.Scene;

public class GameScene(InputManager inputManager, GameWorld gameWorld) : IScene
{
    private readonly InputManager _inputManager = inputManager;
    private readonly Camera _camera = new(inputManager, viewportWidth: 1280, viewportHeight: 720);
    private readonly GameWorld _gameWorld = gameWorld;
    private readonly TileRenderer _tileRenderer = new();

    public void Initialize()
    {
        _gameWorld.Initialize();
    }

    public void LoadContent(ContentManager contentManager)
    {
        _tileRenderer.LoadContent(contentManager);
    }

    public void Update(GameTime gameTime)
    {
        _camera.Update(gameTime);
    }

    public void Draw()
    {
        _tileRenderer.DrawTiles(_gameWorld.Map, _camera);
    }
}

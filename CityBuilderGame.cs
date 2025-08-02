using CityBuilder.Input;
using CityBuilder.Scene;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CityBuilder;

public class CityBuilderGame : Game, ISceneManager
{
    private InputManager _inputManager = new();
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    private IScene _scene;
    private bool _hasLoadedContent = false;

    public CityBuilderGame()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    public void SetScene(IScene newScene)
    {
        Content.Unload();
        _scene = newScene;

        if (_hasLoadedContent)
            _scene.LoadContent(Content);

        _scene.Initialize();
    }

    protected override void Initialize()
    {
        _inputManager.Initialize();
        SetScene(new MenuScene(this));

        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        _hasLoadedContent = true;
        _scene?.LoadContent(Content);
    }

    protected override void Update(GameTime gameTime)
    {
        _inputManager.Update();
        _scene.Update(gameTime);

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.Black);

        _scene.Draw(_spriteBatch);

        base.Draw(gameTime);
    }
}

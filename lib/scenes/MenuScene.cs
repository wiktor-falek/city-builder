using CityBuilder.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace CityBuilder.Scene;

public class MenuScene(ISceneManager sceneManager) : IScene
{
    private readonly ISceneManager _sceneManager = sceneManager;
    private InputManager _inputManager = new();

    // private SpriteFont _font;

    public void Initialize() { }

    public void LoadContent(ContentManager content)
    {
        // _font = content.Load<SpriteFont>("MonogramExtended");
    }

    public void Update(GameTime gameTime)
    {
        if (_inputManager.IsActionPressed(Action.SPACEBAR))
        {
            IScene scene = new GameScene(_sceneManager, new InputManager(), new World.GameWorld());
            _sceneManager.SetScene(scene);
        }
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        // string text = "Press spacebar to continue";
        // Vector2 textSize = _font.MeasureString(text);
        // Vector2 screenCenter = new(1280 / 2f, 720 / 2f);
        // spriteBatch.DrawString(_font, text, screenCenter - textSize / 2f, Color.White);
    }
}

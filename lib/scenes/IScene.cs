using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace CityBuilder.Scene;

public interface IScene
{
    void Initialize();
    void LoadContent(ContentManager contentManager);
    void Update(GameTime gameTime);
    void Draw(SpriteBatch spriteBatch);
}

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace CityBuilder.Scene;

public interface IScene
{
    void Initialize();
    void LoadContent(ContentManager contentManager);
    void Update(GameTime gameTime);
    void Draw();
}

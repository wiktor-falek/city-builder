using CityBuilder.Input;
using Microsoft.Xna.Framework;

namespace CityBuilder.Rendering;

public class Camera(InputManager inputManager, int viewportWidth, int viewportHeight)
{
    public int ViewportWidth = viewportWidth;
    public int ViewportHeight = viewportHeight;
    public Vector2 Position = Vector2.Zero;

    private InputManager _inputManager = inputManager;

    public void Update(GameTime gameTime)
    {
        float speed = 300f;
        float movementDelta = speed * (float)gameTime.ElapsedGameTime.TotalSeconds;

        if (_inputManager.IsActionPressed(Action.CAMERA_UP))
        {
            Position.Y -= movementDelta;
        }
        if (_inputManager.IsActionPressed(Action.CAMERA_DOWN))
        {
            Position.Y += movementDelta;
        }
        if (_inputManager.IsActionPressed(Action.CAMERA_LEFT))
        {
            Position.X -= movementDelta;
        }
        if (_inputManager.IsActionPressed(Action.CAMERA_RIGHT))
        {
            Position.X += movementDelta;
        }
    }

    public Matrix ViewMatrix
    {
        get
        {
            return Matrix.CreateTranslation(new Vector3(-Position.X, -Position.Y, 0f))
                * Matrix.CreateTranslation(
                    new Vector3(ViewportWidth / 2f, ViewportHeight / 2f, 0f)
                );
        }
    }
}

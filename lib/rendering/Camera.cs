using System;
using CityBuilder.Input;
using Microsoft.Xna.Framework;

namespace CityBuilder.Rendering;

public class Camera(InputManager inputManager, int viewportWidth, int viewportHeight)
{
    public int ViewportWidth = viewportWidth;
    public int ViewportHeight = viewportHeight;
    public Vector3 Position = new(0f, 0f, 80f);

    private InputManager _inputManager = inputManager;

    public void Update(GameTime gameTime)
    {
        float speed = 150f;
        float movementDelta = speed * (float)gameTime.ElapsedGameTime.TotalSeconds;

        float zoomSpeed = 100f;
        float zoomDelta = zoomSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;

        if (_inputManager.IsActionPressed(Input.Action.CAMERA_UP))
        {
            Position.Y += movementDelta;
        }
        if (_inputManager.IsActionPressed(Input.Action.CAMERA_DOWN))
        {
            Position.Y -= movementDelta;
        }
        if (_inputManager.IsActionPressed(Input.Action.CAMERA_LEFT))
        {
            Position.X -= movementDelta;
        }
        if (_inputManager.IsActionPressed(Input.Action.CAMERA_RIGHT))
        {
            Position.X += movementDelta;
        }
        if (_inputManager.IsActionPressed(Input.Action.CAMERA_ZOOM_IN))
        {
            Position.Z -= zoomDelta;
            Position.Z = Math.Max(Position.Z, -30);
        }
        if (_inputManager.IsActionPressed(Input.Action.CAMERA_ZOOM_OUT))
        {
            Position.Z += zoomDelta;
            Position.Z = Math.Min(Position.Z, 120);
        }
    }

    public Matrix ViewMatrix
    {
        get
        {
            Vector3 cameraOffset = new(0f, -100f, 100f);
            Vector3 position = Position + cameraOffset;
            Vector3 target = new(Position.X, Position.Y, 0f);
            Vector3 up = Vector3.Up;
            return Matrix.CreateLookAt(position, target, up);
        }
    }
}

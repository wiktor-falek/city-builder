using System.Collections.Generic;
using Microsoft.Xna.Framework.Input;

namespace CityBuilder;

public class InputManager
{
    private Dictionary<Keys, Action> _keybinds;
    private KeyboardState _currentKeyboardState;
    private KeyboardState _previousKeyboardState;

    public InputManager()
    {
        _keybinds = new()
        {
            { Keys.W, Action.CAMERA_UP },
            { Keys.S, Action.CAMERA_DOWN },
            { Keys.A, Action.CAMERA_LEFT },
            { Keys.D, Action.CAMERA_RIGHT },
        };
    }

    public void Initialize()
    {
        _currentKeyboardState = Keyboard.GetState();
        _previousKeyboardState = _currentKeyboardState;
    }

    public void Update()
    {
        _previousKeyboardState = _currentKeyboardState;
        _currentKeyboardState = Keyboard.GetState();
    }

    public bool IsActionPressed(Action action)
    {
        foreach (var kvp in _keybinds)
        {
            if (kvp.Value == action && _currentKeyboardState.IsKeyDown(kvp.Key))
                return true;
        }

        return false;
    }
}

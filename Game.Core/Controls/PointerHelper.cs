using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;

namespace withLuckAndWisdomProject.Controls
{
    /// <summary>
    /// Pointer input merged from mouse (desktop) and touch (mobile).
    /// TouchPanel.GetState() returns an empty collection on desktop, and
    /// Mouse.GetState() is ignored once touch input has been seen, so this
    /// is safe to call from any control on every platform, any number of
    /// times per frame.
    /// </summary>
    public struct PointerState
    {
        public Point Position;
        public bool IsPressed;
    }

    public static class PointerHelper
    {
        private static bool _usingTouch;
        private static Point _lastPosition;

        public static PointerState GetState()
        {
            TouchCollection touches = TouchPanel.GetState();

            foreach (TouchLocation touch in touches)
            {
                if (touch.State == TouchLocationState.Pressed ||
                    touch.State == TouchLocationState.Moved)
                {
                    _usingTouch = true;
                    _lastPosition = touch.Position.ToPoint();
                    return new PointerState { Position = _lastPosition, IsPressed = true };
                }
            }

            // A just-released touch still carries its position for one frame,
            // so taps register a release exactly like a mouse click does.
            foreach (TouchLocation touch in touches)
            {
                if (touch.State == TouchLocationState.Released)
                {
                    _usingTouch = true;
                    _lastPosition = touch.Position.ToPoint();
                    return new PointerState { Position = _lastPosition, IsPressed = false };
                }
            }

            if (_usingTouch)
            {
                // Touch device with no active touches: hold last position, released.
                return new PointerState { Position = _lastPosition, IsPressed = false };
            }

            MouseState mouse = Mouse.GetState();
            return new PointerState
            {
                Position = mouse.Position,
                IsPressed = mouse.LeftButton == ButtonState.Pressed
            };
        }
    }
}

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;

namespace withLuckAndWisdomProject.Controls
{
    /// <summary>
    /// Pointer input merged from mouse (desktop) and touch (mobile).
    /// All positions are returned in VIRTUAL (1280x720 design) coordinates,
    /// matching the letterboxed render area, so hit-tests work on any
    /// screen size/aspect.
    /// TouchPanel.GetState() returns an empty collection on desktop, and
    /// Mouse.GetState() idles at (0,0) on mobile, so this is safe to call
    /// from any control on every platform, any number of times per frame.
    /// </summary>
    public struct PointerState
    {
        public Point Position;
        public bool IsPressed;
    }

    public static class PointerHelper
    {
        private static bool _usingTouch;
        private static Point _lastDevicePos;
        private static Point _prevMousePos;
        private static bool _initialized;

        public static PointerState GetState()
        {
            Singleton viewport = Singleton.Instance;

            TouchCollection touches = TouchPanel.GetState();

            foreach (TouchLocation touch in touches)
            {
                if (touch.State == TouchLocationState.Pressed ||
                    touch.State == TouchLocationState.Moved)
                {
                    _usingTouch = true;
                    _lastDevicePos = touch.Position.ToPoint();
                    return ToPointer(viewport, touch.Position, true);
                }
            }

            // A just-released touch still carries its position for one frame,
            // so taps register a release exactly like a mouse click does.
            foreach (TouchLocation touch in touches)
            {
                if (touch.State == TouchLocationState.Released)
                {
                    _usingTouch = true;
                    _lastDevicePos = touch.Position.ToPoint();
                    return ToPointer(viewport, touch.Position, false);
                }
            }

            MouseState mouse = Mouse.GetState();
            bool mousePressed = mouse.LeftButton == ButtonState.Pressed;
            bool mouseMoved = !_initialized || mouse.Position != _prevMousePos;
            _prevMousePos = mouse.Position;
            _initialized = true;

            // On touch devices Mouse.GetState() sits at (0,0) — ignore it unless
            // the mouse actually moved or a button is down, otherwise hold the
            // last touch position (critical on the tap-release frame so buttons
            // and drags still hit-test at the finger location).
            if (_usingTouch && !mouseMoved && !mousePressed)
                return ToPointer(viewport, new Vector2(_lastDevicePos.X, _lastDevicePos.Y), false);

            return ToPointer(viewport, new Vector2(mouse.Position.X, mouse.Position.Y), mousePressed);
        }

        private static PointerState ToPointer(Singleton viewport, Vector2 devicePixels, bool pressed)
        {
            Vector2 virtualPos = viewport.ToVirtual(devicePixels);
            return new PointerState
            {
                Position = new Point((int)virtualPos.X, (int)virtualPos.Y),
                IsPressed = pressed
            };
        }
    }
}

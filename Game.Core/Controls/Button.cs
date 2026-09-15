using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Threading;

namespace withLuckAndWisdomProject.Controls
{
    public class Button : Component
    {
        private bool _wasPressed;

        private SpriteFont _font;

        private bool _isHovering;

        private Texture2D _texture;

        public Color colour { get; set; }

        public event EventHandler Click;

        public bool Clicked { get; private set; }

        public Color PenColour { get; set; }

        public Vector2 Position { get; set; }

        public Rectangle Rectangle
        {
            get
            {
                return new Rectangle((int)Position.X, (int)Position.Y, _texture.Width, _texture.Height);
            }
        }

        public string Text { get; set; }

        public Button(Texture2D texture, SpriteFont font)
        {
            _texture = texture;

            _font = font;

            PenColour = Color.Black;

            colour = Color.White;

        }

        public Button(Texture2D texture)
        {
            _texture = texture;

            colour = Color.White;

        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {

            if (_isHovering) {
                spriteBatch.Draw(_texture, Rectangle, Color.Gray);
            }
            else
            {
                spriteBatch.Draw(_texture, Rectangle, colour);
            }


            if (!string.IsNullOrEmpty(Text))
            {
                var x = (Rectangle.X + (Rectangle.Width / 2)) - (_font.MeasureString(Text).X / 2);
                var y = (Rectangle.Y + (Rectangle.Height / 2)) - (_font.MeasureString(Text).Y / 2);

                spriteBatch.DrawString(_font, Text, new Vector2(x, y), PenColour);
            }
        }

        public override void Update(GameTime gameTime)
        {
            // Unified mouse (desktop) + touch (mobile) pointer.
            PointerState pointer = PointerHelper.GetState();

            _isHovering = Rectangle.Contains(pointer.Position);

            if (_isHovering && !pointer.IsPressed && _wasPressed)
            {
                AudioManager.PlaySound("MC");
                //naive fix sound delay
                Thread.Sleep(100);
                Click?.Invoke(this, new EventArgs());
            }

            _wasPressed = pointer.IsPressed;
        }
    }
}

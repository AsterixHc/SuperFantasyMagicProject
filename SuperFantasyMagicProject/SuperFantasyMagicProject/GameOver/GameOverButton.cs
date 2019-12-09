using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SuperFantasyMagicProject.Creatures;
using SuperFantasyMagicProject.GameOver;

namespace SuperFantasyMagicProject
{
    class GameOverButton : Componet
    {
        private MouseState previousMS = Mouse.GetState();
        private MouseState currentMS;
        private bool isHovering;
        public SpriteFont font1;

        private Texture2D texture1;

        public Color PenColour { get; set; }
        public Vector2 Position { get; set; }
        public string Text { get; set; }
        public EventHandler Click;

        public Rectangle rectangle
        {
            get
            {
                return new Rectangle((int)Position.X, (int)Position.Y, texture1.Width, texture1.Height);
            }
        }

        public GameOverButton(Texture2D texture, SpriteFont font)
        {
            texture1 = texture;
            font1 = font;
            PenColour = Color.White;
        }

        public void LoadContent()
        {
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            var colour = Color.White;

            if (isHovering)
            {
                colour = Color.Gray;
            }

            spriteBatch.Draw(texture1, rectangle, colour);

            if (!string.IsNullOrEmpty(Text))
            {
                var x = (rectangle.X + (rectangle.Width / 2)) - (font1.MeasureString(Text).X / 2);
                var y = (rectangle.Y + (rectangle.Height / 2)) - (font1.MeasureString(Text).Y / 2);

                spriteBatch.DrawString(font1, Text, new Vector2(x, y), PenColour);
            }
        }

        public override void Update(GameTime gameTime)
        {
            currentMS = Mouse.GetState();
            var mouseRectangle = new Rectangle(currentMS.X, currentMS.Y, 1, 1);

            isHovering = false;

            if (mouseRectangle.Intersects(rectangle))
            {
                isHovering = true;
                if (currentMS.LeftButton == ButtonState.Released && previousMS.LeftButton == ButtonState.Pressed)
                {
                    Click?.Invoke(this, new EventArgs());
                }
            }
        }
    }
}

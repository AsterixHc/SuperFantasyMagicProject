using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace SuperFantasyMagicProject
{
    class MenuButton
    {
        //Input
        private MouseState previousMS = Mouse.GetState();
        private MouseState newMS;

        //Text
        private string text;
        private SpriteFont font;
        private string fontPath = "Menus/Button/Font";
        private Vector2 textDimensions;
        private Color textColor = Color.White;

        //Button
        private Texture2D texture, inactiveTexture, activeTexture, downTexture;
        private string inactivePath = "Menus/Button/Inactive";
        private string activePath = "Menus/Button/Active";
        private string downPath = "Menus/Button/Down";
        private Rectangle collisionBox;
        private Vector2 origin;
        private bool isDown;
        public Vector2 Position { get; set; }
        public bool Activated { get; private set; }

#if DEBUG
        private Texture2D collisionTexture;
#endif

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="text">Text to appear on the button</param>
        public MenuButton(string text)
        {
            Position = Vector2.Zero;
            this.text = text;
            isDown = false;
            Activated = false;
        }

        public void LoadContent()
        {
            activeTexture = MenuManager.Content.Load<Texture2D>(activePath);
            inactiveTexture = MenuManager.Content.Load<Texture2D>(inactivePath);
            downTexture = MenuManager.Content.Load<Texture2D>(downPath);
            font = MenuManager.Content.Load<SpriteFont>(fontPath);
            textDimensions = font.MeasureString(text);
            texture = inactiveTexture;
            origin = new Vector2(texture.Width / 2, texture.Height / 2);
            collisionBox = new Rectangle((int)(Position.X - origin.X), (int)(Position.Y - origin.Y), texture.Width, texture.Height);

#if DEBUG
            collisionTexture = MenuManager.Content.Load<Texture2D>("CollisionTexture");
#endif
        }

        public void UnloadContent()
        {
            MenuManager.Content.Unload();
        }

        public void Update()
        {
            HandleInput();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, Position, null, Color.White, 0, origin, 1, SpriteEffects.None, 1f);
            spriteBatch.DrawString(font, text, Position - (textDimensions / 2), textColor); ;
#if DEBUG
            DrawCollisionBox(spriteBatch);
            DrawMousePosition(spriteBatch);
#endif
        }

        /// <summary>
        /// Handles user input. Changes button textures based on mouse interaction.
        /// </summary>
        private void HandleInput()
        {
            newMS = Mouse.GetState();
            Rectangle mouseRectangle = new Rectangle(newMS.X, newMS.Y, 1, 1);

            if (mouseRectangle.Intersects(collisionBox))
            {
                //Click event, button is up
                if (newMS.LeftButton == ButtonState.Pressed && previousMS.LeftButton == ButtonState.Released && !isDown)
                {
                    isDown = true;
                }
                //Release event, button is down
                else if (newMS.LeftButton == ButtonState.Released && previousMS.LeftButton == ButtonState.Pressed && isDown)
                {
                    isDown = false;
                    Activated = true;
                }

                if (!isDown && texture != activeTexture)
                {
                    texture = activeTexture;
                }
                else if (isDown && texture != downTexture)
                {
                    texture = downTexture;
                }
            }
            else
            {

                //Release event, button is down
                if (newMS.LeftButton == ButtonState.Released && previousMS.LeftButton == ButtonState.Pressed && isDown)
                {
                    isDown = false;
                }

                if (texture != inactiveTexture)
                {
                    texture = inactiveTexture;

                }
            }

            previousMS = newMS;
        }

        /// <summary>
        /// Debugging purposes.
        /// </summary>
        /// <param name="spriteBatch"></param>
        private void DrawCollisionBox(SpriteBatch spriteBatch)
        {
            Rectangle top = new Rectangle((int)Position.X - (int)origin.X, (int)Position.Y - (int)origin.Y, collisionBox.Width, 1);
            Rectangle bottom = new Rectangle((int)Position.X - (int)origin.X, (int)Position.Y - (int)origin.Y + collisionBox.Height, collisionBox.Width, 1);
            Rectangle right = new Rectangle((int)Position.X - (int)origin.X + collisionBox.Width, (int)Position.Y - (int)origin.Y, 1, collisionBox.Height);
            Rectangle left = new Rectangle((int)Position.X - (int)origin.X, (int)Position.Y - (int)origin.Y, 1, collisionBox.Height);

            spriteBatch.Draw(collisionTexture, top, null, Color.Red);
            spriteBatch.Draw(collisionTexture, bottom, null, Color.Red);
            spriteBatch.Draw(collisionTexture, right, null, Color.Red);
            spriteBatch.Draw(collisionTexture, left, null, Color.Red);
        }

        /// <summary>
        /// Debugging purposes.
        /// </summary>
        /// <param name="spriteBatch"></param>
        private void DrawMousePosition(SpriteBatch spriteBatch)
        {
            Rectangle top = new Rectangle((int)newMS.X, (int)newMS.Y, 2, 2);

            spriteBatch.Draw(collisionTexture, top, null, Color.Red);
        }
    }
}

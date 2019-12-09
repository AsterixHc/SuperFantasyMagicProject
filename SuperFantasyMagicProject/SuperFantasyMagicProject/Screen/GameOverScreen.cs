using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SuperFantasyMagicProject.Creatures;

namespace SuperFantasyMagicProject.Screen
{
    class GameOverScreen : GameScreen
    {
        private MouseState previousMS = Mouse.GetState();
        private MouseState newMS;

        private Texture2D background;
        private Texture2D texture, respawnInactive, respawnActive, respawnDown;
        private Texture2D texture2, quitInactive, quitActive, quitDown;
        private string backgroundPath = "GameOverScreen/gameover1";
        private string respawnInactivePath = "GameOverScreen/Respawn1";
        private string respawnActivePath = "GameOverScreen/Respawn2";
        private string respawnDownPath = "GameOverScreen/Respawn3";
        private string quitInactivePath = "GameOverScreen/Quit1";
        private string quitActivePath = "GameOverScreen/Quit2";
        private string quitDownPath = "GameOverScreen/Quit3";

        private Rectangle collisionBox;
        private Vector2 origin;

        private bool isDown;
        private Vector2 firstELeftButton;
        public Vector2 Position { get; set; }
        public Vector2 Position2 { get; set; }
        public bool Activated { get; private set; }

        public GameOverScreen()
        {
            Position = Vector2.Zero;
            Position2 = Vector2.Zero;
            isDown = false;
            Activated = false;
        }


        public override void LoadContent()
        {
            base.LoadContent();
            background = gameScreenContent.Load<Texture2D>(backgroundPath);
            respawnInactive = MenuManager.Content.Load<Texture2D>(respawnInactivePath);
            respawnActive = MenuManager.Content.Load<Texture2D>(respawnActivePath);
            respawnDown = MenuManager.Content.Load<Texture2D>(respawnDownPath);
            quitInactive = MenuManager.Content.Load<Texture2D>(quitInactivePath);
            quitActive = MenuManager.Content.Load<Texture2D>(quitActivePath);
            quitDown = MenuManager.Content.Load<Texture2D>(quitDownPath);
            texture = respawnInactive;
            texture2 = quitInactive;
            origin=new Vector2(texture.Height / 2, texture.Width / 2);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(background, Vector2.Zero, Color.White);
            spriteBatch.Draw(texture, Position, null, Color.White, 0, origin, 1, SpriteEffects.None, 1f);
            spriteBatch.Draw(texture2, Position2, null, Color.White, 0, origin, 1, SpriteEffects.None, 1f);
        }

        public void Update()
        {
            HandleInput();
        }

        public override void HandleInput()
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

                if (!isDown && texture != quitActive)
                {
                    texture = quitActive;
                }
                else if (isDown && texture != quitDown)
                {
                    texture = quitDown;
                }
                if (!isDown && texture != respawnActive)
                {
                    texture = respawnActive;
                }
                else if (isDown && texture != respawnDown)
                {
                    texture = respawnDown;
                }
            }
            else
            {
                texture = quitInactive;

                //Release event, button is down
                if (newMS.LeftButton == ButtonState.Released && previousMS.LeftButton == ButtonState.Pressed && isDown)
                {
                    isDown = false;
                }

                texture = respawnInactive;

                if (newMS.LeftButton == ButtonState.Released && previousMS.LeftButton == ButtonState.Pressed && isDown)
                {
                    isDown = false;
                }
            }

            previousMS = newMS;
        }
    }
}

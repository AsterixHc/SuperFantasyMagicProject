using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace SuperFantasyMagicProject.Screen
{
    class GameOverScreen : GameScreen
    {
        
        private Texture2D background;
        private string backgroundPath = "GameOverScreen/gameover1";

        private Vector2 buttonPosition;

        private List<GameOverButton> buttons;
        private GameOverButton respawnButton, quitButton;
        private Vector2 mostLeftButton;

        public GameOverScreen()
        {
            buttonPosition = new Vector2(ScreenManager.ScreenDimensions.X / 3, ScreenManager.ScreenDimensions.Y / 2);
            mostLeftButton = new Vector2(buttonPosition.X + 12, buttonPosition.Y + 315);
            buttons = new List<GameOverButton>();
            respawnButton = new GameOverButton("Respawn");
            quitButton = new GameOverButton("Quit");
            buttons.Add(respawnButton);
            buttons.Add(quitButton);
            AlignButton();
        }

        public override void LoadContent()
        {
            base.LoadContent();
            background = gameScreenContent.Load<Texture2D>(backgroundPath);

            foreach (GameOverButton button in buttons)
            {
                button.LoadContent();
            }
        }

        public override void UnloadContent()
        {
            base.UnloadContent();

            foreach (GameOverButton button in buttons)
            {
                button.UnloadContent();
            }
            MenuManager.UnloadContent();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            HandleInput();

            foreach (GameOverButton button in buttons)
            {
                button.Update();
            }

            if (respawnButton.Activated)
            {
                //Respawn player
            }

            if (quitButton.Activated)
            {
                //Exit game
            }

        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(background, Vector2.Zero, Color.White);

            foreach (GameOverButton button in buttons)
            {
                button.Draw(spriteBatch);
            }
        }
        public void HandleInput()
        {
            
        }

        private void AlignButton()
        {
            for (int i = 0; i < buttons.Count; i++)
            {
                buttons[i].Position = new Vector2(mostLeftButton.X + (600 * i), mostLeftButton.Y);
            }
        }
    }
}

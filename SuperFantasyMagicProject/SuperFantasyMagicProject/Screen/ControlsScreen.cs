using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperFantasyMagicProject.Screen
{
    class ControlsScreen : GameScreen
    {
        private KeyboardState previousKS = Keyboard.GetState();

        private Texture2D background;
        private string path = "ControlScreen";

        public ControlsScreen()
        {

        }

        public override void LoadContent()
        {
            base.LoadContent();
            background = gameScreenContent.Load<Texture2D>(path);
        }

        public override void UnloadContent()
        {
            base.UnloadContent();
        }
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            HandleInput();
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(background, Vector2.Zero, Color.White);
        }


        public override void HandleInput()
        {
            KeyboardState KS = Keyboard.GetState();

            if (KS.IsKeyDown(Keys.Back) && previousKS.IsKeyUp(Keys.Back))
            {
                ScreenManager.ChangeScreenTo(new TitleScreen());
            }

            previousKS = KS;
        }
    }
}

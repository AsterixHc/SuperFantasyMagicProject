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
    class TitleScreen : GameScreen
    {
        private KeyboardState previousKS = Keyboard.GetState();

        //Variables for handling graphics
        private Texture2D background;
        private string backgroundPath = "TitleScreen/Background";

        public TitleScreen()
        {
            if (!MenuManager.IsMenuOpen)
            {
                MenuManager.OpenMenu("TitleMenu");
            }
        }

        public override void LoadContent()
        {
            base.LoadContent();
            background = gameScreenContent.Load<Texture2D>(backgroundPath);
        }

        public override void UnloadContent()
        {
            base.UnloadContent();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(background, Vector2.Zero, Color.White);
        }

        public override void HandleInput()
        {
            //KeyboardState newKS = Keyboard.GetState();

            //if (newKS.IsKeyDown(Keys.Enter) && previousKS.IsKeyUp(Keys.Enter))
            //{
            //    //TODO: Modify for proper game flow if/when world map works.
            //    ScreenManager.ChangeScreenTo(new BattleScreen(new Bat(), new Bat(), new Bat(), 100));
            //}

            //previousKS = newKS;
        }
    }
}

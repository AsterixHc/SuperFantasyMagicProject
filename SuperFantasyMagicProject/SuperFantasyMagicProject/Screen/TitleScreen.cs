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
            //Set mouse cursor to visible.
            ScreenManager.IsMouseVisible = true;

            base.LoadContent();

            //Background
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
    }
}

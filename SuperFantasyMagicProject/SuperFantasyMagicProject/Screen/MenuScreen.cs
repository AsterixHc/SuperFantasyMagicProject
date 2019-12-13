using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SuperFantasyMagicProject.Screen
{
    /// <summary>
    /// Button for Play
    /// Button for Pause
    /// Button for UnPause
    /// Button for Exit
    /// </summary>
    class MenuScreen : GameScreen
    {
        //Background image for the menu screen.
        private Texture2D background;
        private string path = "MenuScreen/Background";

        /// <summary>
        /// Default constructor.
        /// </summary>
        public MenuScreen()
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

        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(background, Vector2.Zero, Color.White);
        }
    }
}

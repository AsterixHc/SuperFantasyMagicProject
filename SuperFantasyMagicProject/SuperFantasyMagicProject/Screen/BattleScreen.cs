using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SuperFantasyMagicProject.Screen
{
    class BattleScreen : GameScreen
    {

        private int expValue;
        public int ExpValue { get => expValue; }
        //Background image for the splash screen.
        private Texture2D background;
        //Path to the background image.
        private string path = "BattleScreen/Background";

        /// <summary>
        /// Default constructor.
        /// </summary>
        public BattleScreen()
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

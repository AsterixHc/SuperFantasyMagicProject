using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SuperFantasyMagicProject.Screen
{
    class SplashScreen : GameScreen
    {
        public override void LoadContent()
        {
            base.LoadContent();
            gameScreenContent.Load<Texture2D>("Content");
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
            base.Draw(spriteBatch);
        }

        public override void HandleInput()
        {
            throw new NotImplementedException();
        }
    }
}

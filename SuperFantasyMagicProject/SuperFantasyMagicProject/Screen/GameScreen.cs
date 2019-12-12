using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

namespace SuperFantasyMagicProject.Screen
{
    abstract class GameScreen
    {
        //Local ContentManager, unique to each GameScreen object, that handles its content.
        protected ContentManager gameScreenContent;


        public GameScreen()
        {

        }

        public virtual void LoadContent()
        {
            //Refers gameScreenContent to ScreenManager's ServiceProvider, and sets the root directory.
            gameScreenContent = new ContentManager(ScreenManager.ContentManager.ServiceProvider, "Content");
        }

        /// <summary>
        /// Is to be called for each instance of GameScreen when that instance is no longer in use. Unloads the content of GameScreen.
        /// </summary>
        public virtual void UnloadContent()
        {
            gameScreenContent.Unload();
        }

        public virtual void Update(GameTime gameTime)
        {

        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {

        }
    }
}

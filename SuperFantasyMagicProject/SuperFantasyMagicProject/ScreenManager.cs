using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using SuperFantasyMagicProject.Screen;
using SuperFantasyMagicProject.Creatures;

namespace SuperFantasyMagicProject
{
    static class ScreenManager
    {
        //ContentManager for handling all content in the game.
        private static ContentManager contentManager;

        //Dimensions of the game window (Width, height).
        private static Vector2 screenDimensions = new Vector2(1920, 1080);
        

        //The GameScreen that is currently being displayed.
        private static GameScreen currentScreen;

        public static ContentManager ContentManager { get => contentManager; private set => contentManager = value; }
        public static Vector2 ScreenDimensions { get => screenDimensions; private set => screenDimensions = value; }

        /// <summary>
        /// Sets initial screen parameters when game starts.
        /// </summary>
        public static void Initialize()
        {
            currentScreen = new BattleScreen(new Bat(new Vector2(1610, 160)), new Bat(new Vector2(1610, 400)), new Bat(new Vector2(1610, 640)));
        }

        /// <summary>
        /// Loads content required by the current game screen.
        /// </summary>
        /// <param name="contentManager"></param>
        public static void LoadContent(ContentManager contentManager)
        {
            ContentManager = new ContentManager(contentManager.ServiceProvider, "Content");
            currentScreen.LoadContent();
        }

        /// <summary>
        /// Unloads content no longer needed by the game whenever current screen changes.
        /// </summary>
        public static void UnloadContent()
        {

        }

        /// <summary>
        /// Updates game elements on the screen.
        /// </summary>
        /// <param name="gameTime"></param>
        public static void Update(GameTime gameTime)
        {
            currentScreen.Update(gameTime);
        }

        /// <summary>
        /// Draws the screen.
        /// </summary>
        /// <param name="spriteBatch"></param>
        public static void Draw(SpriteBatch spriteBatch)
        {
            currentScreen.Draw(spriteBatch);
        }


    }
}

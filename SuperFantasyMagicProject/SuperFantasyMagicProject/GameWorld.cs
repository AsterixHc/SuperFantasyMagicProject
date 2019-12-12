using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SuperFantasyMagicProject.Screen;

namespace SuperFantasyMagicProject
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class GameWorld : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        ContentManager content;

        public GameWorld()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            graphics.PreferredBackBufferWidth = (int)ScreenManager.ScreenDimensions.X;
            graphics.PreferredBackBufferHeight = (int)ScreenManager.ScreenDimensions.Y;
            //graphics.IsFullScreen = true;
            graphics.HardwareModeSwitch = false; //why does this work in fullscreen and not in widowed?
            graphics.ApplyChanges();

            ScreenManager.Initialize();
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here

            //Load the content of the initial game screen.
            ScreenManager.LoadContent(Content); //TODO: Check if this is supposed to be 'content', not 'Content'
            //Load menu settings and MenuManager
            MenuManager.LoadContent(Content);

        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here

            //Unload the content of the last game screen.
            ScreenManager.UnloadContent();
            MenuManager.UnloadContent();
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            ScreenManager.Update(gameTime);
            MenuManager.Update(gameTime);

            //Set mouse cursor to visible/invisible based on screen and menu state.
            if (ScreenManager.IsMouseVisible && !IsMouseVisible)
            {
                IsMouseVisible = true;
            }
            else if (MenuManager.IsMenuOpen && !IsMouseVisible)
            {
                IsMouseVisible = true;
            }
            else if (!MenuManager.IsMenuOpen && IsMouseVisible)
            {
                IsMouseVisible = false;
            }

            //Check if exiting from menu
            if (MenuManager.ExitFromMenu)
            {
                Exit();
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            spriteBatch.Begin();
            ScreenManager.Draw(spriteBatch);
            MenuManager.Draw(spriteBatch);
            spriteBatch.End();

            base.Draw(gameTime);
        }

        public void Instantiate(ContentManager content)
        {

        }
    }
}

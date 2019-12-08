using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;

namespace SuperFantasyMagicProject
{
    static class MenuManager
    {
        private static Menu menu;
        public static bool IsMenuOpen { get; private set; }

        //ContentManager for handling all menu related content in the game.
        public static ContentManager Content { get; private set; }

        public static void Initialize()
        {

        }

        public static void LoadContent(ContentManager contentManager)
        {
            Content = new ContentManager(contentManager.ServiceProvider, "Content");
        }

        public static void UnloadContent()
        {
            if (menu != null)
            {
                menu.UnloadContent();
            }
        }

        public static void Update(GameTime gameTime)
        {
            if (menu != null)
            {
                menu.Update(gameTime);
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.M))
            {
                OpenMenu("GameMenu");
            }
        }

        public static void Draw(SpriteBatch spriteBatch)
        {
            if (menu != null)
            {
                menu.Draw(spriteBatch);
            }
        }

        public static void OpenMenu(string menuType)
        {
            menu = new Menu(menuType);
            IsMenuOpen = true;
            menu.LoadContent();
        }

        public static void CloseMenu()
        {
            if (menu != null)
            {
                menu.UnloadContent();
                menu = null;
                IsMenuOpen = false;
            }
        }

        public static void ChangeMenuTo(string menuType)
        {
            if (menu != null)
            {
                menu.UnloadContent();
                menu = new Menu(menuType);
                menu.LoadContent();
            }
        }
    }
}

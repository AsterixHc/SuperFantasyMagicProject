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
        public static bool ExitFromMenu { get; set; } = false;

        //ContentManager for handling all menu related content in the game.
        public static ContentManager Content { get; private set; }

        public static void LoadContent(ContentManager contentManager)
        {
            Content = new ContentManager(contentManager.ServiceProvider, "Content");
        }

        public static void UnloadContent()
        {
            if (menu != null)
            {
                menu.UnloadContent(Content);
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

        /// <summary>
        /// Opens a menu of the specified type.
        /// </summary>
        /// <param name="menuType">Type of menu to be opened. Valid arguments are: GameMenu, TitleMenu</param>
        public static void OpenMenu(string menuType)
        {
            if (menu == null)
            {
                menu = new Menu(menuType);
                IsMenuOpen = true;
                menu.LoadContent(Content);
            }
            else
            {
                throw new Exception("MenuManager.OpenMenu was called, but MenuManager.menu was not null.");
            }
        }

        /// <summary>
        /// Closes the current menu.
        /// </summary>
        public static void CloseMenu()
        {
            if (menu != null)
            {
                menu.UnloadContent(Content);
                menu = null;
                IsMenuOpen = false;
            }
            else
            {
                throw new Exception("MenuManager.CloseMenu was called, but MenuManager.menu was null.");
            }
        }

        /// <summary>
        /// Changes between menus.
        /// </summary>
        /// <param name="menuType">Type of menu to be changed to. Valid arguments are: GameMenu, TitleMenu</param>
        public static void ChangeMenuTo(string menuType)
        {
            if (menu != null)
            {
                menu.UnloadContent(Content);
                menu = new Menu(menuType);
                menu.LoadContent(Content);
            }
            else
            {
                throw new Exception("MenuManager.ChangeMenuTo was called, but MenuManager.menu was null.");
            }
        }
    }
}

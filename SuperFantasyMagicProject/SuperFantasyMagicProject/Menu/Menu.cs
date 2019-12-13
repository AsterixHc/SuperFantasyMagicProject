using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using SuperFantasyMagicProject.Creatures;
using SuperFantasyMagicProject.Screen;

namespace SuperFantasyMagicProject
{
    enum MenuType { GameMenu, TitleMenu}

    class Menu
    {
        public MenuType Type { get; private set; }

        private Texture2D texture;
        private string path;
        private Vector2 position;
        private Vector2 origin;

        private List<MenuButton> buttons;
        private MenuButton playButton, titleButton, quitButton, returnButton, controlButton, characterButton;
        private Vector2 topButtonPosition;

        public Menu(string menuType)
        {
            if (menuType == "GameMenu")
            {
                Type = MenuType.GameMenu;
                path = "Menus/GameMenu";
                position = new Vector2(ScreenManager.ScreenDimensions.X / 2, ScreenManager.ScreenDimensions.Y / 2);
                topButtonPosition = new Vector2(position.X, position.Y - 150);
                buttons = new List<MenuButton>();
                returnButton = new MenuButton("Return to Game");
                characterButton = new MenuButton("Character Screen");
                titleButton = new MenuButton("Title Screen");
                quitButton = new MenuButton("Quit Game");
                buttons.Add(returnButton);
                buttons.Add(characterButton);
                buttons.Add(titleButton);
                buttons.Add(quitButton);
                AlignButtons();
            }
            else if (menuType == "TitleMenu")
            {
                path = "Menus/TitleMenu";
                Type = MenuType.TitleMenu;
                position = new Vector2(ScreenManager.ScreenDimensions.X / 2, ScreenManager.ScreenDimensions.Y / 2);
                topButtonPosition = new Vector2(position.X, position.Y - 100);
                buttons = new List<MenuButton>();
                playButton = new MenuButton("Play");
                controlButton = new MenuButton("Controls");
                quitButton = new MenuButton("Quit Game");
                buttons.Add(playButton);
                buttons.Add(controlButton);
                buttons.Add(quitButton);
                AlignButtons();
            }
            else
            {
                throw new ArgumentException("Invalid argument used for class Menu constructor. Valid arguments are: GameMenu, TitleMenu");
            }
        }

        public void LoadContent()
        {
            texture = MenuManager.Content.Load<Texture2D>(path);
            origin = new Vector2(texture.Width / 2, texture.Height / 2);

            foreach (MenuButton button in buttons)
            {
                button.LoadContent();

            }
        }

        public void UnloadContent()
        {
            foreach (MenuButton button in buttons)
            {
                button.UnloadContent();
            }

            MenuManager.Content.Unload();
        }

        public void Update(GameTime gameTime)
        {
            foreach (MenuButton button in buttons)
            {
                button.Update();
            }

            //Button functionality.
            if (Type == MenuType.GameMenu)
            {
                if (returnButton.Activated)
                {
                    MenuManager.CloseMenu();
                }
                else if (characterButton.Activated)
                {
                    //show character sheet
                }
                else if (titleButton.Activated)
                {
                    ScreenManager.ChangeScreenTo(new TitleScreen());
                    MenuManager.ChangeMenuTo("TitleMenu");
                }
                else if (quitButton.Activated)
                {
                    //quit game
                }
            }
            else if (Type == MenuType.TitleMenu)
            {
                if (playButton.Activated)
                {
                    //TODO: Change this for proper game flow
                    ScreenManager.ChangeScreenTo(new BattleScreen(new Bat(), new Bat(), new Bat(), 100));
                    MenuManager.CloseMenu();
                }
                else if (controlButton.Activated)
                {
                    ScreenManager.ChangeScreenTo(new ControlsScreen());
                    MenuManager.CloseMenu();
                }
                else if (quitButton.Activated)
                {
                    //quit game
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, null, Color.White, 0, origin, 1, SpriteEffects.None, 1f);
            foreach (MenuButton button in buttons)
            {
                button.Draw(spriteBatch);
            }
        }

        /// <summary>
        /// Sets the position of buttons to align with the center of the menu
        /// </summary>
        private void AlignButtons()
        {
            for (int i = 0; i < buttons.Count; i++)
            {
                buttons[i].Position = new Vector2(topButtonPosition.X, topButtonPosition.Y + (100 * i));
            }
        }
    }
}

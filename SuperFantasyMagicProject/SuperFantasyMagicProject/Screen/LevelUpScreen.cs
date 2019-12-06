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
    enum ClassType { Rogue, Warrior, Mage }

    class LevelUpScreen : GameScreen
    {
        //Background image for the level up screen.
        private Texture2D background;
        private string backgroundPath = "LevelUpScreen/Background";

        //Character full body images
        private Texture2D rogueImage;
        private string rogueImagePath = "LevelUpScreen/RogueFullBody";
        private Texture2D warriorImage;
        private string warriorImagePath = "LevelUpScreen/WarriorFullBody";
        private Texture2D mageImage;
        private string mageImagePath = "LevelUpScreen/MageFullBody";

        //Enum for tracking the active character
        private ClassType activeCharacter = ClassType.Rogue;
        private Texture2D activeCharacterImage;

        //Keyboard state
        private KeyboardState previousKS = Keyboard.GetState();

        public LevelUpScreen()
        { 

        }

        public override void LoadContent()
        {
            base.LoadContent();
            background = gameScreenContent.Load<Texture2D>(backgroundPath);
            rogueImage = gameScreenContent.Load<Texture2D>(rogueImagePath);
            warriorImage = gameScreenContent.Load<Texture2D>(warriorImagePath);
            mageImage = gameScreenContent.Load<Texture2D>(mageImagePath);
        }

        public override void UnloadContent()
        {
            base.UnloadContent();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            if (activeCharacter == ClassType.Rogue)
            {
                activeCharacterImage = rogueImage;
                //display current stats
            }
            else if (activeCharacter == ClassType.Warrior)
            {
                activeCharacterImage = warriorImage;
                //display current stats
            }
            else
            {
                activeCharacterImage = mageImage;
                //display current stats
            }

            HandleInput();
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(background, Vector2.Zero, Color.White);
            spriteBatch.Draw(activeCharacterImage, new Vector2(100, 300), Color.White);
            //spriteBatch.DrawString();
        }

        public override void HandleInput()
        {
            KeyboardState KS = Keyboard.GetState();

            if (KS.IsKeyDown(Keys.Enter) && previousKS.IsKeyUp(Keys.Enter))
            {
                if (activeCharacter == ClassType.Rogue)
                {
                    activeCharacter = ClassType.Warrior;
                }
                else if (activeCharacter == ClassType.Warrior)
                {
                    activeCharacter = ClassType.Mage;
                }
                else
                {
                    activeCharacter = ClassType.Rogue;
                }
            }
            previousKS = KS;
        }

        public void IncreaseStat()
        {
            //Select Stats to Increase
        }

    }
}

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
        private KeyboardState previousKS = Keyboard.GetState();

        //Graphics relevant variables
        private SpriteFont font;
        private string fontPath = "LevelUpScreen/LevelUpScreenFont";
        private Texture2D background;
        private string backgroundPath = "LevelUpScreen/Background";
        private Texture2D rogueImage;
        private string rogueImagePath = "LevelUpScreen/RogueFullBody";
        private Texture2D warriorImage;
        private string warriorImagePath = "LevelUpScreen/WarriorFullBody";
        private Texture2D mageImage;
        private string mageImagePath = "LevelUpScreen/MageFullBody";

        //Variables relevant to handling the currently active character.
        private ClassType activeCharacter = ClassType.Rogue;
        private Texture2D activeCharacterImage;

        /// <summary>
        /// Property that accesses the statPoints variable in the character stat classes.
        /// </summary>
        private int activeCharacterStatPoints
        {
            get
            {
                if (activeCharacter == ClassType.Rogue)
                {
                    return RogueStats.StatPoints;
                }
                else if (activeCharacter == ClassType.Warrior)
                {
                    return WarriorStats.StatPoints;
                }
                else
                {
                    return MageStats.StatPoints;
                }
            }
            set
            {
                if (activeCharacter == ClassType.Rogue)
                {
                    RogueStats.StatPoints = value;
                }
                else if (activeCharacter == ClassType.Warrior)
                {
                    WarriorStats.StatPoints = value;
                }
                else
                {
                    MageStats.StatPoints = value;
                }
            }
        }

        /// <summary>
        /// Read-only property that accesses the level variable in the character stat classes.
        /// </summary>
        private int activeCharacterLevel
        {
            get
            {
                if (activeCharacter == ClassType.Rogue)
                {
                    return RogueStats.Level;
                }
                else if (activeCharacter == ClassType.Warrior)
                {
                    return WarriorStats.Level;
                }
                else
                {
                    return MageStats.Level;
                }
            }
        }

        public LevelUpScreen()
        {
            activeCharacter = ClassType.Rogue;
        }

        public override void LoadContent()
        {
            base.LoadContent();
            font = gameScreenContent.Load<SpriteFont>(fontPath);
            background = gameScreenContent.Load<Texture2D>(backgroundPath);
            rogueImage = gameScreenContent.Load<Texture2D>(rogueImagePath);
            warriorImage = gameScreenContent.Load<Texture2D>(warriorImagePath);
            mageImage = gameScreenContent.Load<Texture2D>(mageImagePath);
            activeCharacterImage = rogueImage;
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
                //display current stats (?? hvad har jeg ment med dette?)
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
            spriteBatch.Draw(activeCharacterImage, new Vector2(280, 210), Color.White);
            spriteBatch.DrawString(font, "Level " + activeCharacterLevel, new Vector2(900, 200), Color.Sienna);
            spriteBatch.DrawString(font, "Stat points remaining: " + activeCharacterStatPoints, new Vector2(900, 300), Color.Sienna);
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

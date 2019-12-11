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

        private int activeCharacterHealth
        {
            get
            {
                if(activeCharacter == ClassType.Rogue)
                {
                    return RogueStats.MaxHealth;
                }
                else if (activeCharacter == ClassType.Warrior)
                {
                    return WarriorStats.MaxHealth;
                }
                else
                {
                    return MageStats.MaxHealth;
                }
            }
        }

        private int activeCharacterSpeed
        {
            get
            {
                if (activeCharacter == ClassType.Rogue)
                {
                    return RogueStats.TurnSpeed;
                }
                else if (activeCharacter == ClassType.Warrior)
                {
                    return WarriorStats.TurnSpeed;
                }
                else
                {
                    return MageStats.TurnSpeed;
                }
            }
        }

        #region Stats that can be changed
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
        
        private int activeCharacterStrenght
        {
            get
            {
                if (activeCharacter == ClassType.Rogue)
                {
                    return RogueStats.Strenght;
                }
                else if (activeCharacter == ClassType.Warrior)
                {
                    return WarriorStats.Strength;
                }
                else
                {
                    return MageStats.Strength;
                }
            }
            set
            {
                if (activeCharacter == ClassType.Rogue)
                {
                    RogueStats.Strenght = value;
                }
                else if (activeCharacter == ClassType.Warrior)
                {
                    WarriorStats.Strength = value;
                }
                else
                {
                    MageStats.Strength = value;
                }
            }
        }

        private int activeCharacterAgility
        {
            get
            {
                if (activeCharacter == ClassType.Rogue)
                {
                    return RogueStats.Agility;
                }
                else if (activeCharacter == ClassType.Warrior)
                {
                    return WarriorStats.Agility;
                }
                else
                {
                    return MageStats.Agility;
                }
            }
            set
            {
                if (activeCharacter == ClassType.Rogue)
                {
                    RogueStats.Agility = value;
                }
                else if (activeCharacter == ClassType.Warrior)
                {
                    WarriorStats.Agility = value;
                }
                else
                {
                    MageStats.Agility = value;
                }
            }
        }

        private int activeCharacterIntelligence
        {
            get
            {
                if (activeCharacter == ClassType.Rogue)
                {
                    return RogueStats.Intelligence;
                }
                else if (activeCharacter == ClassType.Warrior)
                {
                    return WarriorStats.Intelligence;
                }
                else
                {
                    return MageStats.Intelligence;
                }
            }
            set
            {
                if (activeCharacter == ClassType.Rogue)
                {
                    RogueStats.Intelligence = value;
                }
                else if (activeCharacter == ClassType.Warrior)
                {
                    WarriorStats.Intelligence = value;
                }
                else
                {
                    MageStats.Intelligence = value;
                }
            }
        }

        #endregion

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
            spriteBatch.DrawString(font, "Health " + activeCharacterHealth, new Vector2(900, 300), Color.Sienna);
            spriteBatch.DrawString(font, "Stat points remaining: " + activeCharacterStatPoints, new Vector2(900, 400), Color.Sienna);
            spriteBatch.DrawString(font, "Strenght: " + activeCharacterStrenght, new Vector2(900, 500), Color.Sienna);
            spriteBatch.DrawString(font, "Agility: " + activeCharacterAgility, new Vector2(900, 600), Color.Sienna);
            spriteBatch.DrawString(font, "Intelligence: " + activeCharacterIntelligence, new Vector2(900, 700), Color.Sienna);
            spriteBatch.DrawString(font, "Speed: " + activeCharacterSpeed, new Vector2(900, 800), Color.Sienna);

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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SuperFantasyMagicProject.Screen
{
    enum ClassType { Rogue, Warrior, Mage }

    class LevelUpScreen : GameScreen
    {
        //Buttons
        MenuButton commitButton, returnButton;
        ArrowButton prevCharacterButton, nextCharacterButton;

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
        private ClassType activeCharacter;
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
            commitButton = new MenuButton("Commit Changes");
            returnButton = new MenuButton("Return");
            prevCharacterButton = new ArrowButton("Left");
            nextCharacterButton = new ArrowButton("Right");
            activeCharacter = ClassType.Rogue;
            PositionButtons();
        }

        public override void LoadContent()
        {
            //Set mouse cursor to visible.
            ScreenManager.IsMouseVisible = true;

            base.LoadContent();

            //Background
            background = gameScreenContent.Load<Texture2D>(backgroundPath);

            //Text
            font = gameScreenContent.Load<SpriteFont>(fontPath);
            
            //Character standing images
            rogueImage = gameScreenContent.Load<Texture2D>(rogueImagePath);
            warriorImage = gameScreenContent.Load<Texture2D>(warriorImagePath);
            mageImage = gameScreenContent.Load<Texture2D>(mageImagePath);
            activeCharacterImage = rogueImage;

            //Buttons
            prevCharacterButton.LoadContent(gameScreenContent);
            nextCharacterButton.LoadContent(gameScreenContent);
            returnButton.LoadContent(gameScreenContent);
            commitButton.LoadContent(gameScreenContent);
        }

        public override void UnloadContent()
        {
            base.UnloadContent();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            //Update buttons.
            commitButton.Update();
            returnButton.Update();
            prevCharacterButton.Update();
            nextCharacterButton.Update();

            HandleInput();
            UpdateStandingImage();
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            //Draw backgrund and character image
            spriteBatch.Draw(background, Vector2.Zero, Color.White);
            spriteBatch.Draw(activeCharacterImage, new Vector2(280, 210), Color.White);

            //Draw buttons
            prevCharacterButton.Draw(spriteBatch);
            nextCharacterButton.Draw(spriteBatch);
            returnButton.Draw(spriteBatch);
            commitButton.Draw(spriteBatch);

            //Draw stats
            spriteBatch.DrawString(font, "Level " + activeCharacterLevel, new Vector2(900, 200), Color.Sienna);
            spriteBatch.DrawString(font, "Health " + activeCharacterHealth, new Vector2(900, 300), Color.Sienna);
            spriteBatch.DrawString(font, "Stat points remaining: " + activeCharacterStatPoints, new Vector2(900, 400), Color.Sienna);
            spriteBatch.DrawString(font, "Strenght: " + activeCharacterStrenght, new Vector2(900, 500), Color.Sienna);
            spriteBatch.DrawString(font, "Agility: " + activeCharacterAgility, new Vector2(900, 600), Color.Sienna);
            spriteBatch.DrawString(font, "Intelligence: " + activeCharacterIntelligence, new Vector2(900, 700), Color.Sienna);
            spriteBatch.DrawString(font, "Speed: " + activeCharacterSpeed, new Vector2(900, 800), Color.Sienna);
        }

        /// <summary>
        /// Acts on player input.
        /// </summary>
        public void HandleInput()
        {
            if (prevCharacterButton.Activated)
            {
                //Cycle to previous character.
                switch (activeCharacter)
                {
                    case ClassType.Rogue:
                        activeCharacter = ClassType.Mage;
                        break;
                    case ClassType.Warrior:
                        activeCharacter = ClassType.Rogue;
                        break;
                    case ClassType.Mage:
                        activeCharacter = ClassType.Warrior;
                        break;
                    default:
                        break;
                }
            }
            else if (nextCharacterButton.Activated)
            {
                //Cycle to next character.
                switch (activeCharacter)
                {
                    case ClassType.Rogue:
                        activeCharacter = ClassType.Warrior;
                        break;
                    case ClassType.Warrior:
                        activeCharacter = ClassType.Mage;
                        break;
                    case ClassType.Mage:
                        activeCharacter = ClassType.Rogue;
                        break;
                    default:
                        break;
                }
            }
            else if (returnButton.Activated)
            {
                //Return to the previous screen from which LevelUpScreen was opened.
                ScreenManager.LoadCachedScreen();
            }
            else if (commitButton.Activated)
            {
                //Commit any pending changes to stats.
                //TODO: Implement this.
            }
        }

        public void IncreaseStat()
        {
            //Select Stats to Increase
        }

        /// <summary>
        /// Sets position of buttons on LevelUpScreen.
        /// </summary>
        private void PositionButtons()
        {
            commitButton.Position = new Vector2(1080, 950);
            returnButton.Position = new Vector2(1430, 950);
            prevCharacterButton.Position = new Vector2(175, 535);
            nextCharacterButton.Position = new Vector2(1750, 535);
        }

        /// <summary>
        /// Changes the standing character image if active character has changed.
        /// </summary>
        private void UpdateStandingImage()
        {
            if (activeCharacter == ClassType.Rogue && activeCharacterImage != rogueImage)
            {
                activeCharacterImage = rogueImage;
            }
            else if (activeCharacter == ClassType.Warrior && activeCharacterImage != warriorImage)
            {
                activeCharacterImage = warriorImage;
            }
            else if (activeCharacter == ClassType.Mage && activeCharacterImage != mageImage)
            {
                activeCharacterImage = mageImage;
            }
        }
    }
}

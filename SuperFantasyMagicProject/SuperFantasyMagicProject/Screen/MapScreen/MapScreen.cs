using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SuperFantasyMagicProject.Screen
{
    class MapScreen : GameScreen
    {
        //Graphics
        private Texture2D background;
        private string path = "MapScreen/Background";

        //Encounters
        private EncounterMarker[] encounters = new EncounterMarker[11];
        ///If an encounter marker is clicked, values are assigned to the following 4 variables.
        ///They are then passed as arguements to change screens.
        EncounterMarker activatedMarker = null;
        Character enemy0 = null;
        Character enemy1 = null;
        Character enemy2 = null;

        public MapScreen()
        {
            DefineEncounters();
            PositionEncounters();
        }

        public override void LoadContent()
        {
            //Set mouse cursor to visible.
            ScreenManager.IsMouseVisible = true;

            base.LoadContent();
            background = gameScreenContent.Load<Texture2D>(path);

            foreach (EncounterMarker encounter in encounters)
            {
                encounter.LoadContent(gameScreenContent);
            }
        }

        public override void UnloadContent()
        {
            base.UnloadContent();
        }

        public override void Update(GameTime gameTime)
        {
            foreach (EncounterMarker encounter in encounters)
            {
                encounter.Update(gameTime);
            }

            HandleInput();
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(background, Vector2.Zero, null, Color.White);

            foreach (EncounterMarker encounter in encounters)
            {
                encounter.Draw(spriteBatch);
            }
        }


        /// <summary>
        /// Checks and acts on player input. This is where the content of battle encounters is specified.
        /// </summary>
        private void HandleInput()
        {
            //Loop through each encounter marker, and check if it has been pressed.
            foreach (EncounterMarker encounter in encounters)
            {
                if (encounter.Activated)
                {
                    ///Using the Activator, create 3 new instances of the enemy types called for by the encounter.
                    ///Since the Activator needs to know the type of the object to be instantiated, Type.GetType is used.
                    ///
                    ///Type.GetType requires an assembly-qualified name (ie. SuperFantasyMagicProject.Bat) to perform its search,
                    ///so "SuperFantasyMagicProject." is added in the front of the string containing the class name.
                    enemy0 = (Character)Activator.CreateInstance(Type.GetType("SuperFantasyMagicProject." + encounter.Enemy0));
                    enemy1 = (Character)Activator.CreateInstance(Type.GetType("SuperFantasyMagicProject." + encounter.Enemy1));
                    enemy2 = (Character)Activator.CreateInstance(Type.GetType("SuperFantasyMagicProject." + encounter.Enemy2));
                    activatedMarker = encounter;
                    break;
                }
            }

            //If an encounter marker was pressed, change screens.
            if (activatedMarker != null)
            {
                ScreenManager.ChangeScreenTo(new BattleScreen(enemy0, enemy1, enemy2, activatedMarker.ExperienceValue));
            }
        }

        /// <summary>
        /// Defines the battle encounters on the map.
        /// </summary>
        private void DefineEncounters()
        {
            encounters[0] = new EncounterMarker("Bat", "Bat", "Bat", 100, 1, true);
            encounters[1] = new EncounterMarker("Hornet", "Scorpion", "Hornet", 180, 2, true);
            encounters[2] = new EncounterMarker("Hornet", "Scorpion", "DemonFlower", 220, 2, true);
            encounters[3] = new EncounterMarker("Bat", "Hayo", "Bat", 260, 3, true);
            encounters[4] = new EncounterMarker("DemonFlower", "Hayo", "DemonFlower", 360, 3, true);
            encounters[5] = new EncounterMarker("Sangshi", "Scorpion", "Sangshi", 410, 1, true);
            encounters[6] = new EncounterMarker("Hayo", "Sangshi", "Hayo", 460, 1, true);
            encounters[7] = new EncounterMarker("Sanghshi", "DemonFlower", "Hayo", 500, 3, true);
            encounters[8] = new EncounterMarker("Sangshi", "Sangshi", "Sangshi", 510, 1, true);
            encounters[9] = new EncounterMarker("DemonFlower", "DemonFlower", "DemonFlower", 420, 3, true);
            encounters[10] = new EncounterMarker("Hornet", "Hayo", "Hornet", 320, 2, true);
        }

        /// <summary>
        /// Assigns a position to each encounter on the map.
        /// </summary>
        private void PositionEncounters()
        {
            encounters[0].Position = new Vector2(500, 322);
            encounters[1].Position = new Vector2(647, 354);
            encounters[2].Position = new Vector2(619, 497);
            encounters[3].Position = new Vector2(595, 593);
            encounters[4].Position = new Vector2(834, 723);
            encounters[5].Position = new Vector2(1141, 510);
            encounters[6].Position = new Vector2(1158, 423);
            encounters[7].Position = new Vector2(1259, 493);
            encounters[8].Position = new Vector2(1280, 573);
            encounters[9].Position = new Vector2(1337, 613);
            encounters[10].Position = new Vector2(1372, 731);
        }
    }
}

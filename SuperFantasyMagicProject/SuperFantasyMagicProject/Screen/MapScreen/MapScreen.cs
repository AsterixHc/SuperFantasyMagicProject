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
        private EncounterMarker[] encounters = new EncounterMarker[10];
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
            encounters[1] = new EncounterMarker("Bat", "Bat", "Bat", 100, 1, false);
            encounters[2] = new EncounterMarker("Bat", "Bat", "Bat", 100, 1, false);
            encounters[3] = new EncounterMarker("Bat", "Bat", "Bat", 100, 1, false);
            encounters[4] = new EncounterMarker("Bat", "Bat", "Bat", 100, 1, false);
            encounters[5] = new EncounterMarker("Bat", "Bat", "Bat", 100, 1, false);
            encounters[6] = new EncounterMarker("Bat", "Bat", "Bat", 100, 1, false);
            encounters[7] = new EncounterMarker("Bat", "Bat", "Bat", 100, 1, false);
            encounters[8] = new EncounterMarker("Bat", "Bat", "Bat", 100, 1, false);
            encounters[9] = new EncounterMarker("Bat", "Bat", "Bat", 100, 1, false);
        }

        /// <summary>
        /// Assigns a position to each encounter on the map.
        /// </summary>
        private void PositionEncounters()
        {
            encounters[0].Position = new Vector2(100, 100);
            encounters[1].Position = new Vector2(200, 200);
            encounters[2].Position = new Vector2(300, 300);
            encounters[3].Position = new Vector2(400, 400);
            encounters[4].Position = new Vector2(500, 500);
            encounters[5].Position = new Vector2(600, 600);
            encounters[6].Position = new Vector2(700, 700);
            encounters[7].Position = new Vector2(800, 800);
            encounters[8].Position = new Vector2(900, 900);
            encounters[9].Position = new Vector2(1000, 1000);
        }
    }
}

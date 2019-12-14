using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace SuperFantasyMagicProject
{
    class EncounterMarker
    {
        //Input
        private MouseState previousMS = Mouse.GetState();
        private MouseState newMS;

        //Map marker
        private Texture2D texture, unavailableTexture, availableTexture, activeTexture;
        private string unavailablePath, availablePath, activePath;
        private Rectangle collisionBox;
        private Vector2 origin;
        public Vector2 Position { get; set; }
        public bool Available { get; set; }
        public bool Activated { get; private set; }

        //Encounter
        public string Enemy0, Enemy1, Enemy2;
        public int ExperienceValue;

        /// <summary>
        /// Constructor for a map encounter.
        /// </summary>
        /// <param name="enemy0">Name of the first enemy (name of the class, case sensitive)</param>
        /// <param name="enemy1">Name of the second enemy (name of the class, case sensitive)</param>
        /// <param name="enemy2">Name of the third enemy (name of the class, case sensitive)</param>
        /// <param name="experienceValue">Amount of experience granted for clearing this encounter</param>
        /// <param name="markerType">The type of marker sprite to use for this encounter (type 1,2 or 3)</param>
        /// <param name="startsAsAvailable">If true, the encounter starts out as available to the player</param>
        public EncounterMarker(string enemy0, string enemy1, string enemy2, int experienceValue, int markerType, bool startsAsAvailable)
        {
            Enemy0 = enemy0;
            Enemy1 = enemy1;
            Enemy2 = enemy2;
            ExperienceValue = experienceValue;
            Available = startsAsAvailable;
            Activated = false;

            //Set texture paths depending on the marker type.
            if (markerType == 1)
            {
                unavailablePath = "MapScreen/Encounters/Encounter1/Unavailable";
                availablePath = "MapScreen/Encounters/Encounter1/Available";
                activePath = "MapScreen/Encounters/Encounter1/Active";
            }
            else if (markerType == 2)
            {
                unavailablePath = "MapScreen/Encounters/Encounter2/Unavailable";
                availablePath = "MapScreen/Encounters/Encounter2/Available";
                activePath = "MapScreen/Encounters/Encounter2/Active";
            }
            else if (markerType == 3)
            {
                unavailablePath = "MapScreen/Encounters/Encounter3/Unavailable";
                availablePath = "MapScreen/Encounters/Encounter3/Available";
                activePath = "MapScreen/Encounters/Encounter3/Active";
            }
            else
            {
                throw new ArgumentException("Invalid argument for 'markerType' in MapEncounter's constructor. " +
                        "Valid arguments are integers 1, 2, 3.");
            }
        }

        public void LoadContent(ContentManager contentManager)
        {
            unavailableTexture = contentManager.Load<Texture2D>(unavailablePath);
            availableTexture = contentManager.Load<Texture2D>(availablePath);
            activeTexture = contentManager.Load<Texture2D>(activePath);

            if (Available)
            {
                texture = availableTexture;
            }
            else
            {
                texture = unavailableTexture;
            }

            origin = new Vector2(texture.Width / 2, texture.Height / 2);
            collisionBox = new Rectangle((int)(Position.X - origin.X), (int)(Position.Y - origin.Y), texture.Width, texture.Height);
        }

        public void UnloadContent()
        {
            
        }

        public void Update(GameTime gameTime)
        {
            if (Available)
            {
                HandleInput();
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, Position, null, Color.White, 0 , origin, 1, SpriteEffects.None, 1f);
        }

        protected void HandleInput()
        {
            newMS = Mouse.GetState();
            Rectangle mouseRectangle = new Rectangle(newMS.X, newMS.Y, 1, 1);

            if (mouseRectangle.Intersects(collisionBox))
            {
                //Click event
                if (newMS.LeftButton == ButtonState.Pressed && previousMS.LeftButton == ButtonState.Released)
                {
                    Activated = true;
                }

                //Set texture
                if (texture != activeTexture)
                {
                    texture = activeTexture;
                }
            }
            else
            {
                //Set texture
                if (texture != availableTexture)
                {
                    texture = availableTexture;
                }
            }

            previousMS = newMS;
        }
    }
}

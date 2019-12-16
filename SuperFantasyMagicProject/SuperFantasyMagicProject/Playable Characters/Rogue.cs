using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace SuperFantasyMagicProject.Playable_Characters
{
    class Rogue : Character
    {
        /// <summary>
        /// Default constructor.
        /// </summary>
        public Rogue()
        {
            texturePath = "Player/Jeremy/Jeremy pink/JeremyPinkWalk/JeremyPinkWalkRight";

            UpdateStats();
        }

        #region Properties

        ///The folliwing properties don't set the local variable when.
        ///Instead, they set the corresponsing stat in the RogueStats class,
        ///and then update the local variables to match them.

        public override int Strength
        {
            get
            {
                return strength;
            }
            set
            {
                RogueStats.Strenght = value;
                UpdateStats();
            }
        }

        public override int Agility
        {
            get
            {
                return agility;
            }
            set
            {
                RogueStats.Agility = value;
                UpdateStats();
            }
        }

        public override int Intelligence
        {
            get
            {
                return intelligence;
            }
            set
            {
                RogueStats.Intelligence = value;
                UpdateStats();
            }
        }

        public override int CurrentHealth
        {
            get
            {
                return currentHealth;
            }
            set
            {
                RogueStats.CurrentHealth = value;
                UpdateStats();
            }
        }

        public override int CurrentMana
        {
            get
            {
                return currentMana;
            }
            set
            {
                RogueStats.CurrentMana = value;
                UpdateStats();
            }
        }

        #endregion

        public override void LoadContent(ContentManager content)
        {
            //Load textures.
            textures = new Texture2D[3];

            for (int i = 0; i < textures.Length; i++)
            {
                textures[i] = content.Load<Texture2D>(texturePath + (i + 1));
            }

            texture = textures[0];

            //Set origin.
            origin = new Vector2(texture.Width / 2, texture.Height / 2);
        }

        /// <summary>
        /// This override is identical to the general method inherited from Character.cs,
        /// except it sets a different texture scale.
        /// </summary>
        /// <param name="spriteBatch"></param>
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, Position, null, Color.White, 0f, origin, 2, SpriteEffects.None, 1f);
        }

        public override void SpecialAttack()
        {
            //Item is always the same from the same monster
            //Steal Item from choosen enemy in the enemy array

        }

        public override void LevelUp()
        {
            //Checks amount of Exprience gained
            //If Exprience gained is higher than Exprience needed to "Levelup"
            //Trigger LevelUp screen
            //Increase Stats
        }

        public override void Flee()
        {
            //Escape from Combat
        }

        public override void UseItem(Item item)
        {
            //Checks List for items
            //If item selected is on List
            //Call Item Script (f.eks. Item.Potion)
            //Gain Effect
            //Check if Item Effect Gained
        }

        /// <summary>
        /// Updates all Rogue stats to correspond with the stats saved in the static class RogueStats.
        /// </summary>
        public void UpdateStats()
        {
            strength = RogueStats.Strenght;
            agility = RogueStats.Agility;
            intelligence = RogueStats.Intelligence;
            maxHealth = RogueStats.MaxHealth;
            currentHealth = RogueStats.CurrentHealth;
            maxMana = RogueStats.MaxMana;
            currentMana = RogueStats.CurrentMana;
            damage = RogueStats.Damage;
            critical = RogueStats.Critical;
            turnSpeed = RogueStats.TurnSpeed;
        }
    }
}

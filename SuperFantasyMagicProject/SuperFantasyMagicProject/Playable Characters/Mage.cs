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
    class Mage : Character
    {
        public Mage()
        {
            texturePath = "Player/Martha/Martha green/MarthaGreenWalk/MarthaGreenWalkRight";

            UpdateStats();
        }

        #region Properties

        ///The folliwing properties don't set the local variable when.
        ///Instead, they set the corresponsing stat in the MageStats class,
        ///and then update the local variables to match them.

        public override int Strength
        {
            get
            {
                return strength;
            }
            set
            {
                MageStats.Strength = value;
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
                MageStats.Agility = value;
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
                MageStats.Intelligence = value;
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
                MageStats.CurrentHealth = value;
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
                MageStats.CurrentMana = value;
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
            //Healing Self or Allies
            //Heal should be about 20% of the full HP of the character used on
            //Low chance to heal full party
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
        /// Updates all Rogue stats to correspond with the stats saved in the static class MageStats.
        /// </summary>
        public void UpdateStats()
        {
            strength = MageStats.Strength;
            agility = MageStats.Agility;
            intelligence = MageStats.Intelligence;
            maxHealth = MageStats.MaxHealth;
            currentHealth = MageStats.CurrentHealth;
            maxMana = MageStats.MaxMana;
            currentMana = MageStats.CurrentMana;
            damage = MageStats.Damage;
            critical = MageStats.Critical;
            turnSpeed = MageStats.TurnSpeed;
        }
    }
}

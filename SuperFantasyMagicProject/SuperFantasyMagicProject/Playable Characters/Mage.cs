using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace SuperFantasyMagicProject.Playable_Characters
{
    class Mage : Character
    {
        public Mage()
        {
            texturePath = "Player/Martha/Martha blonde/MarthaBlondeWalk/MarthaBlondeWalkRight2";
            position = Vector2.Zero;
            origin = Vector2.Zero;

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

        public override int Attack()
        {
            //Choose an Enemy from the Enemy Array
            //Attack the chosen enemy in the Array
            return 0;
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

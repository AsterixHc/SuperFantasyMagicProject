using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace SuperFantasyMagicProject.Playable_Characters
{
    class Rogue : Character
    {
        /// <summary>
        /// Default constructor.
        /// </summary>
        public Rogue()
        {
            texturePath = "Player/Jeremy/Jeremy pink/JeremyPinkAttack/JeremyPinkAttackRight1";
            position = Vector2.Zero;
            origin = Vector2.Zero;

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

        public override int Attack()
        {

            //Choose an Enemy from the enemy array
            //Choose enemy from enemy array
            return 0;
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

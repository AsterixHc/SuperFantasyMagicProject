using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;


namespace SuperFantasyMagicProject.Playable_Characters
{
    class Warrior : Character
    {
        /// <summary>
        /// Default constructor.
        /// </summary>
        public Warrior()
        {
            texturePath = "Player / Knight / Standing / KnightStanding1";
            position = Vector2.Zero;
            origin = Vector2.Zero;

            UpdateStats();
        }

        #region Properties

        ///The folliwing properties don't set the local variable when.
        ///Instead, they set the corresponsing stat in the WarriorStats class,
        ///and then update the local variables to match them.

        public override int Strength
        {
            get
            {
                return strength;
            }
            set
            {
                WarriorStats.Strength = value;
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
                WarriorStats.Agility = value;
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
                WarriorStats.Intelligence = value;
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
                WarriorStats.CurrentHealth = value;
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
                WarriorStats.CurrentMana = value;
                UpdateStats();
            }
        }

        #endregion

        public override int Attack()
        {
            //Choose an Enemy from enemy array
            //Attack the chosen enemy in the Array
            //Character damage = 20 + (10% * Player.Strength)

            return 0; // Needs to be fixed sooner rather than later!!

            //ScreenManager.currentScreen.
        }

        public override void SpecialAttack()
        {
            //Attack all Enemies
            //DMG should be about 25% of the basic attack
            //Over all dmg should be about 75% of a full basic attack
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
        /// Updates all Rogue stats to correspond with the stats saved in the static class WarriorStats.
        /// </summary>
        public void UpdateStats()
        {
            strength = WarriorStats.Strength;
            agility = WarriorStats.Agility;
            intelligence = WarriorStats.Intelligence;
            maxHealth = WarriorStats.MaxHealth;
            currentHealth = WarriorStats.CurrentHealth;
            maxMana = WarriorStats.MaxMana;
            currentMana = WarriorStats.CurrentMana;
            damage = WarriorStats.Damage;
            critical = WarriorStats.Critical;
            turnSpeed = WarriorStats.TurnSpeed;
        }
    }
}

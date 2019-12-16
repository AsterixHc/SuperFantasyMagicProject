using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperFantasyMagicProject
{
    public static class RogueStats
    {
        private static int baseHealth = 100;
        private static int baseMana = 100;
        private static double baseCritical = 0.10;

        private static int strength = 10;
        private static int agility = 10;
        private static int intelligence = 10;

        private static int maxHealth = baseHealth + Strenght * 10;
        private static int currentHealth = maxHealth;

        private static int maxMana = baseMana + Intelligence * 10;
        private static int currentMana = maxMana;

        private static int damage = agility * 2;
        private static double critical = baseCritical + intelligence / 10;
        private static int turnSpeed = agility / 2;

        private static int experience = 0;
        private static int level = 1;
        private static bool hasLevelUp = false;
        private static int statPoints = 0;

        #region Properties

        public static int Strenght
        {
            get { return strength; }
            set
            {
                strength = value;
                MaxHealth = baseHealth + strength * 10;
            }
        }

        public static int Agility
        {
            get { return agility; }
            set
            {
                agility = value;
                TurnSpeed = agility / 2;
                Damage = agility * 2;
            }
        }

        public static int Intelligence
        {
            get { return intelligence; }
            set
            {
                intelligence = value;
                MaxMana = baseMana + Intelligence * 10;
                Critical = baseCritical + intelligence / 10;
            }
        }

        public static int MaxHealth
        {
            get { return maxHealth; }
            private set
            {
                maxHealth = value;

                if (CurrentHealth > value)
                {
                    CurrentHealth = value;
                }
            }
        }

        public static int CurrentHealth
        {
            get { return currentHealth; }
            set
            {
                if (value <= MaxHealth && value >= 0)
                {
                    currentHealth = value;
                }
                else if (value < 0)
                {
                    currentHealth = 0;
                }
                else if (value > MaxHealth)
                {
                    currentHealth = MaxHealth;
                }
            }
        }

        public static int MaxMana
        {
            get { return maxMana; }
            private set
            {
                maxMana = value;

                if (CurrentMana > value)
                {
                    CurrentMana = value;
                }
            }
        }

        public static int CurrentMana
        {
            get { return currentMana; }
            set { currentMana = value; }
        }

        public static int Damage
        {
            get { return damage; }
            private set
            {
                if (value >= 0)
                {
                    damage = value;
                }
            }
        }

        public static double Critical { get; private set; }

        public static int TurnSpeed { get; private set; }

        public static int Experience
        {
            get { return experience; }
            set
            {
                if (value > 0)
                {
                    experience = value;
                    //TODO: Change this so that levels require increasingly more experience.
                    while (Experience / Level >= 100)
                    {
                        Level += 1;
                        HasLevelUp = true;
                        StatPoints += 5;
                    }
                }
            }
        }

        public static int Level
        {
            get { return level; }
            private set
            {
                if (value > 0)
                {
                    level = value;
                }
            }
        }

        public static bool HasLevelUp
        {
            get { return hasLevelUp; }
            private set { hasLevelUp = value; }
        }

        public static int StatPoints
        {
            get { return statPoints; }
            set
            {
                if (value >= 0)
                {
                    statPoints = value;
                }
            }
        }

        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperFantasyMagicProject
{
    public static class RogueStats
    {

        private static int strenght = 10;
        private static int agility = 10;
        private static int intelligence = 10;
        private static int experience = 0;

        private static int baseHealth = 100;
        public static int maxHealth { get; private set; } = baseHealth + Strenght * 10;
        public static int currentHealth;

        private static int baseMana = 100;
        public static int maxMana { get; private set; } = baseMana + Intelligence * 10;
        public static int currentMana = 0;

        public static int damage { get; private set; } = agility * 2;

        #region
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

        public static int Strenght
        {
            get { return strenght; }
            set
            {
                strenght = value;
                maxHealth = baseHealth + strenght * 10;
            }
        }

        public static int Agility
        {
            get { return agility; }
            set
            {
                agility = value;
                int baseAgility = 100;
                turnSpeed = baseAgility + agility / 10;

                damage = agility * 2;
            }
        }

        public static int Intelligence
        {
            get { return intelligence; }
            set
            {
                intelligence = value;
                double baseIntelligence = 0.10;
                critical = baseIntelligence + intelligence / 100;
            }
        }

        #endregion

        public static int Level { get; private set; } = 1;
        public static bool HasLevelUp { get; private set; } = false;
        public static int StatPoints { get; set; } = 0;

        public static int turnSpeed = 20;
        public static double critical = 0.05;
    }
}

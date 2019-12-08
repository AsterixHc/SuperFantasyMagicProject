using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperFantasyMagicProject
{
    public static class RogueStats
    {
        public static int maxHealth = 150;
        public static int currentHealth = 150;
        public static int mana = 100;
        public static int strenght = 10;
        public static int agility = 10;
        public static int intelligence = 10;
        public static int damage = 20;
        private static int experience = 0;

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

        public static int Level { get; private set; } = 1;
        public static bool HasLevelUp { get; private set; } = false;
        public static int StatPoints { get; set; } = 0;

        public static int turnSpeed = 10;
        public static double critical = 0.05;
    }
}

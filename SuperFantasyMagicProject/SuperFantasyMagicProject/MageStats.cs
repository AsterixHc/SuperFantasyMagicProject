using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperFantasyMagicProject
{
    public static class MageStats
    {
        public static int maxHealth = 120;
        public static int currentHealth = 120;
        public static int mana = 200;
        public static int strenght = 10;
        public static int agility = 10;
        public static int intelligence = 10;
        public static int damage = 20;

        public static int Experience
        {
            get { return Experience; }
            set
            {
                if (value > 0)
                {
                    Experience = value;
                    //TODO: Change this so that levels require increasingly more experience.
                    if (Experience / Level > 100)
                    {
                        HasLevelUp = true;
                    }
                }
            }
        }

        public static int Level { get; set; } = 1;
        public static bool HasLevelUp { get; private set; } = false;

        public static int turnSpeed = 10;
        public static double critical = 0.05;
    }
}

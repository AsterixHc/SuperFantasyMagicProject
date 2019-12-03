using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperFantasyMagicProject.Items
{
    class PhoenixDown : Item
    {
        public PhoenixDown(int currentHealth, int mana, bool isPlayerAlive)
        {
            if(isPlayerAlive==false)
            {
                currentHealth = 40;
                mana = 40;
            }
        }

        //If Player is dead
        //Revive Player at 40% Health and 40% mana
    }
}

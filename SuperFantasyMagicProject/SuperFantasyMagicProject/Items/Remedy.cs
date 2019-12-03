using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperFantasyMagicProject.Items
{
    class Remedy : Item
    {
        public Remedy(bool isPoisoned, bool isScratched, bool isGusted, bool isParalysed)
        {
            if (isPoisoned == true)
            {
                isPoisoned = false;
            }
            if(isScratched== true)
            {
                isScratched = false;
            }
            if(isGusted==true)
            {
                isGusted = false;
            }
            if(isParalysed==true)
            {
                isParalysed = false;
            }
        }

        //If Player is under any negative status effects
        //Clear negative status effects from Player
    }
}

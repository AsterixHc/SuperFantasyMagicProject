using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperFantasyMagicProject.Items
{
    class Antidote : Item
    {
        public Antidote(bool isPoisoned)
        {
            if(isPoisoned==true)
            {
                isPoisoned = false;
            }
        }

        //If Player is Poisened
        //Remove Poisen
    }
}
